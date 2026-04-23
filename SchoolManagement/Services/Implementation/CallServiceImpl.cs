using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Call;

namespace SchoolManagement.Services.Implementation
{
    public class CallServiceImpl : ICallService
    {
        private readonly SchoolManagementDb context;
        private readonly BlobServiceClient blobServiceClient;
        private readonly IConfiguration configuration;
        private readonly ILogger<CallServiceImpl> logger;

        public CallServiceImpl(
            SchoolManagementDb _context,
            BlobServiceClient _blobServiceClient,
            IConfiguration _configuration,
            ILogger<CallServiceImpl> _logger)
        {
            context = _context;
            blobServiceClient = _blobServiceClient;
            configuration = _configuration;
            logger = _logger;
        }

        public List<CallResponseVM> GetAllEmployeeLogs(int empId, int tenantId)
        {
            var result = (from c in context.Call
                                  .Include(e => e.Stage)
                                  .Include(c => c.Contact)
                                  .Include(c => c.Tenant)
                                  .Include(c => c.DirectionTypeName)
                                  .Include(c => c.CallStatusName)
                          join u in context.Users on c.CreatedBy equals u.UserId into uGroup
                          from user in uGroup.DefaultIfEmpty()
                          where c.ContactId == empId && c.TenantId == tenantId
                          select new CallResponseVM()
                          {
                              Id = c.Id,
                              ContactId = c.ContactId,
                              Contact = c.Contact != null ? c.Contact.Name : null,
                              BenificiaryName = c.Contact != null ? c.Contact.Beneficiary : null,
                              BeneficiaryRelationshipName = c.Contact != null ? c.Contact.BeneficiaryRelationshipName : null,
                              CallDuration = c.CallDuration,
                              StageId = c.StageId,
                              Stage = c.Stage != null ? c.Stage.Name : null,
                              AudioLink = c.AudioLink, // refreshed below
                              Remarks = c.Remarks,
                              TenantId = c.TenantId,
                              TenantName = c.Tenant != null ? c.Tenant.Name : null,
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                              CallStatusName = c.CallStatusName != null ? c.CallStatusName.Name : null,
                              DirectionTypeName = c.DirectionTypeName != null ? c.DirectionTypeName.Name : null,
                              CallStatusId = c.CallStatusId ?? 0,
                              DirectionTypeId = c.DirectionTypeId ?? 0,
                          }).ToList();

            if (result == null || result.Count == 0) return null;

            // Regenerate fresh SAS URLs so audio is always playable
            foreach (var r in result)
                r.AudioLink = GenerateFreshSasUrl(r.AudioLink);

            return result;
        }

        public List<CallResponseVM> GetAllLogs(int tenantId)
        {
            var result = (from c in context.Call
                                  .Include(e => e.Stage)
                                  .Include(c => c.Contact)
                                  .Include(c => c.Tenant)
                                  .Include(c => c.DirectionTypeName)
                                  .Include(c => c.CallStatusName)
                          join u in context.Users on c.CreatedBy equals u.UserId into uGroup
                          from user in uGroup.DefaultIfEmpty()
                          where c.TenantId == tenantId
                          select new CallResponseVM()
                          {
                              Id = c.Id,
                              ContactId = c.ContactId,
                              Contact = c.Contact != null ? c.Contact.Name : null,
                              BenificiaryName = c.Contact != null ? c.Contact.Beneficiary : null,
                              BeneficiaryRelationshipName = c.Contact != null ? c.Contact.BeneficiaryRelationshipName : null,
                              StageId = c.StageId,
                              Stage = c.Stage != null ? c.Stage.Name : null,
                              AudioLink = c.AudioLink, // refreshed below
                              Remarks = c.Remarks,
                              TenantId = c.TenantId,
                              TenantName = c.Tenant != null ? c.Tenant.Name : null,
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                              CallStatusName = c.CallStatusName != null ? c.CallStatusName.Name : null,
                              DirectionTypeName = c.DirectionTypeName != null ? c.DirectionTypeName.Name : null,
                              CallStatusId = c.CallStatusId ?? 0,
                              DirectionTypeId = c.DirectionTypeId ?? 0,
                          }).ToList();

            if (result == null || result.Count == 0) return null;

            // Regenerate fresh SAS URLs so audio is always playable
            foreach (var r in result)
                r.AudioLink = GenerateFreshSasUrl(r.AudioLink);

            return result;
        }

        public async Task<CallResponseVM> AddCallAsync(CallCreateVM request)
        {
            var audioLink = await UploadAudioToAzureBlobAsync(request.AudioFile);

            var call = new MCall
            {
                ContactId = request.ContactId,
                StageId = request.StageId,
                AudioLink = audioLink,
                Remarks = request.Remarks,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            context.Call.Add(call);
            await context.SaveChangesAsync();

            var savedCall = await context.Call
                .Where(c => c.Id == call.Id)
                .Include(c => c.Stage)
                .Include(c => c.Contact)
                .Include(c => c.Tenant)
                .FirstAsync();

            var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == savedCall.CreatedBy);

            return new CallResponseVM
            {
                Id = savedCall.Id,
                ContactId = savedCall.ContactId,
                Contact = savedCall.Contact?.Name,
                BenificiaryName = savedCall.Contact?.Beneficiary,
                BeneficiaryRelationshipName = savedCall.Contact?.BeneficiaryRelationshipName,
                CallDuration = savedCall.CallDuration,
                StageId = savedCall.StageId,
                Stage = savedCall.Stage?.Name,
                AudioLink = savedCall.AudioLink,
                Remarks = savedCall.Remarks,
                TenantId = savedCall.TenantId,
                TenantName = savedCall.Tenant?.Name,
                CreatedByName = user != null ? user.FirstName + " " + user.LastName : null
            };
        }

        private async Task<string> UploadAudioToAzureBlobAsync(IFormFile audioFile)
        {
            if (audioFile == null || audioFile.Length == 0)
            {
                throw new InvalidOperationException("Audio file is required.");
            }

            var containerName = configuration["AzureBlobStorage:CallAudioContainerName"];
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new InvalidOperationException("Azure blob container name is missing in configuration. Set AzureBlobStorage:CallAudioContainerName.");
            }

            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);

            var extension = Path.GetExtension(audioFile.FileName);
            var blobName = $"call-audio/{DateTime.UtcNow:yyyy/MM/dd}/{Guid.NewGuid()}{extension}";
            var blobClient = containerClient.GetBlobClient(blobName);

            var contentType = string.IsNullOrWhiteSpace(audioFile.ContentType) ? "audio/mpeg" : audioFile.ContentType;
            var headers = new BlobHttpHeaders
            {
                ContentType = contentType,
                ContentDisposition = "inline" // Encourages playback rather than attachment/download
            };

            await using (var stream = audioFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = headers
                });
            }

            if (!blobClient.CanGenerateSasUri)
            {
                return blobClient.Uri.ToString();
            }

            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                BlobName = blobName,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddHours(2), // Reduced expiration for better security
                ContentType = contentType,
                ContentDisposition = "inline"
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read); // Strict Read-only permission

            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }

        public async Task<CallDashboardOverviewVM> GetDashboardOverviewAsync(int tenantId)
        {
            var vm = new CallDashboardOverviewVM();
            
            var allCallsQuery = context.Call
                .Include(c => c.Stage)
                .Include(c => c.Contact)
                .Include(c => c.Tenant)
                .Include(c => c.DirectionTypeName)
                .Include(c => c.CallStatusName)
                .Where(c => c.TenantId == tenantId);

            var allCalls = await allCallsQuery.ToListAsync();

            vm.TotalCalls = allCalls.Count;
            
            TimeSpan totalDuration = TimeSpan.Zero;
            foreach(var call in allCalls)
            {
                if(call.CallDuration.HasValue)
                {
                    totalDuration += call.CallDuration.Value;
                }
            }

            vm.TotalCallingDuration = $"{(int)totalDuration.TotalHours}h {totalDuration.Minutes}m {totalDuration.Seconds}s";
            
            if (vm.TotalCalls > 0)
            {
                var avgTicks = totalDuration.Ticks / vm.TotalCalls;
                var avgTime = new TimeSpan(avgTicks);
                vm.AvgHandleTime = $"{avgTime.Minutes}m {avgTime.Seconds}s";
            }
            else
            {
                vm.AvgHandleTime = "0m 0s";
            }

            // Outcomes
            var outcomesMap = new Dictionary<string, int>();
            foreach(var call in allCalls)
            {
                string stage = call.Stage != null ? call.Stage.Name : "Unknown";
                if (!outcomesMap.ContainsKey(stage))
                {
                    outcomesMap[stage] = 0;
                }
                outcomesMap[stage]++;
            }

            var colors = new string[] { "bg-emerald-500", "bg-primary-500", "bg-amber-400", "bg-violet-500", "bg-red-400", "bg-sky-500", "bg-pink-500" };
            int colorIndex = 0;
            foreach(var kvp in outcomesMap)
            {
                vm.Outcomes.Add(new CallOutcomeVM
                {
                    Label = kvp.Key,
                    Value = kvp.Value,
                    Color = colors[colorIndex % colors.Length]
                });
                colorIndex++;
            }

            // Volume Data (Last 7 days, mock for now using CreatedOn date part)
            var recentDays = allCalls.Where(c => c.CreatedOn > DateTime.UtcNow.AddDays(-7))
                                    .GroupBy(c => c.CreatedOn.Date)
                                    .OrderBy(g => g.Key);
            foreach(var group in recentDays)
            {
                vm.VolumeData.Add(new CallVolumeVM
                {
                    Hour = group.Key.ToString("dd MMM"), // Abusing 'Hour' property for Date
                    Inbound = group.Count(),
                    Outbound = 0, // Mock outbound
                    Missed = 0    // Mock missed
                });
            }

            // Recent Calls
            var recentLogs = allCalls.OrderByDescending(c => c.CreatedOn).Take(10).ToList();
            var userIds = recentLogs.Select(c => c.CreatedBy).Distinct().ToList();
            var users = await context.Users.Where(u => userIds.Contains(u.UserId)).ToDictionaryAsync(u => u.UserId);

            foreach(var call in recentLogs)
            {
                var user = users.ContainsKey(call.CreatedBy) ? users[call.CreatedBy] : null;

                vm.RecentCalls.Add(new CallResponseVM
                {
                    Id = call.Id,
                    ContactId = call.ContactId,
                    Contact = call.Contact != null ? call.Contact.Name : null,
                    BenificiaryName = call.Contact != null ? call.Contact.Beneficiary : null,
                    BeneficiaryRelationshipName = call.Contact != null ? call.Contact.BeneficiaryRelationshipName : null,
                    CallDuration = call.CallDuration,
                    StageId = call.StageId,
                    Stage = call.Stage != null ? call.Stage.Name : null,
                    // Regenerate a fresh 2-hour SAS URL so audio is always playable on the dashboard
                    AudioLink = GenerateFreshSasUrl(call.AudioLink),
                    Remarks = call.Remarks,
                    TenantId = call.TenantId,
                    TenantName = call.Tenant != null ? call.Tenant.Name : null,
                    CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                    CallStatusName = call.CallStatusName != null ? call.CallStatusName.Name : null,
                    DirectionTypeName = call.DirectionTypeName != null ? call.DirectionTypeName.Name : null,
                    CallStatusId = call.CallStatusId ?? 0,
                    DirectionTypeId = call.DirectionTypeId ?? 0,
                });
            }

            return vm;
        }

        /// <summary>
        /// Generates a fresh 2-hour SAS URL for playback.
        ///
        /// KEY DESIGN DECISION: We always use the injected BlobServiceClient (configured
        /// for neuropiblob4244 — confirmed to be where files actually live) and only
        /// extract the container name + blob path from the stored URL.
        /// We intentionally IGNORE the account hostname in the stored URL because legacy
        /// DB records may contain an old account name (e.g. neuropistorageacct7) even
        /// though the physical file was uploaded to neuropiblob4244.
        /// </summary>
        private string GenerateFreshSasUrl(string storedUrl)
        {
            if (string.IsNullOrWhiteSpace(storedUrl))
            {
                logger.LogDebug("[Audio] storedUrl is null/empty — skipping SAS generation.");
                return null;
            }

            logger.LogDebug("[Audio] GenerateFreshSasUrl input: {Url}", storedUrl);

            try
            {
                var uri = new Uri(storedUrl);

                // Parse ONLY the path — ignore the hostname/account name in the stored URL.
                // AbsolutePath format: /<container>/<blobName>  (SAS query string is NOT in AbsolutePath)
                var path = uri.AbsolutePath.TrimStart('/');
                var slash = path.IndexOf('/');

                if (slash < 0)
                {
                    logger.LogWarning("[Audio] Cannot parse container/blob from path '{Path}' — returning null.", path);
                    return null;
                }

                var containerName = path[..slash];
                var blobName = path[(slash + 1)..];

                logger.LogDebug("[Audio] Parsed → container='{Container}', blob='{Blob}'", containerName, blobName);

                // Use the injected blobServiceClient (neuropiblob4244 — where the file actually lives).
                var blobClient = blobServiceClient
                    .GetBlobContainerClient(containerName)
                    .GetBlobClient(blobName);

                logger.LogDebug("[Audio] Target BlobUri={BlobUri}, CanGenerateSasUri={CanSas}",
                    blobClient.Uri, blobClient.CanGenerateSasUri);

                if (!blobClient.CanGenerateSasUri)
                {
                    logger.LogWarning("[Audio] CanGenerateSasUri=false — BlobServiceClient must use " +
                        "StorageSharedKeyCredential (connection string with AccountKey), not Managed Identity.");
                    return null;
                }

                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = blobName,
                    Resource = "b",
                    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),  // ±5 min buffer for clock skew
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(2),
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                var freshUrl = blobClient.GenerateSasUri(sasBuilder).ToString();
                logger.LogDebug("[Audio] ✅ SAS generated. ExpiresIn=2h. Starts: {Prefix}",
                    freshUrl.Length > 100 ? freshUrl[..100] + "..." : freshUrl);

                return freshUrl;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[Audio] GenerateFreshSasUrl FAILED for '{Url}'.", storedUrl);
                return null;
            }
        }

    }
}
