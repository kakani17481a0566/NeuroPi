using Azure.Storage;
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
                              AudioLink = c.AudioLink,
                              Remarks = c.Remarks,
                              TenantId = c.TenantId,
                              TenantName = c.Tenant != null ? c.Tenant.Name : null,
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                              CallStatusName = c.CallStatusName != null ? c.CallStatusName.Name : null,
                              DirectionTypeName = c.DirectionTypeName != null ? c.DirectionTypeName.Name : null,
                              CallStatusId = c.CallStatusId ?? 0,
                              DirectionTypeId = c.DirectionTypeId ?? 0,
                              CreatedOn = c.CreatedOn,
                          }).ToList();

            if (result == null || result.Count == 0) return null;

            foreach (var item in result)
            {
                item.AudioLink = GenerateFreshSasUrl(item.AudioLink) ?? item.AudioLink;
            }

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
                              AudioLink = c.AudioLink,
                              Remarks = c.Remarks,
                              TenantId = c.TenantId,
                              TenantName = c.Tenant != null ? c.Tenant.Name : null,
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                              CallStatusName = c.CallStatusName != null ? c.CallStatusName.Name : null,
                              DirectionTypeName = c.DirectionTypeName != null ? c.DirectionTypeName.Name : null,
                              CallStatusId = c.CallStatusId ?? 0,
                              DirectionTypeId = c.DirectionTypeId ?? 0,
                              CreatedOn = c.CreatedOn,
                          }).ToList();

            if (result == null || result.Count == 0) return null;

            foreach (var item in result)
            {
                item.AudioLink = GenerateFreshSasUrl(item.AudioLink) ?? item.AudioLink;
            }

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
                CreatedOn = DateTime.UtcNow,
                CallDuration = request.CallDuration
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
                AudioLink = GenerateFreshSasUrl(savedCall.AudioLink) ?? savedCall.AudioLink,
                Remarks = savedCall.Remarks,
                TenantId = savedCall.TenantId,
                TenantName = savedCall.Tenant?.Name,
                CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                CreatedOn = savedCall.CreatedOn
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
                ContentDisposition = "inline"
            };

            await using (var stream = audioFile.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, new BlobUploadOptions
                {
                    HttpHeaders = headers
                });
            }

            return blobClient.Uri.ToString();
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
            foreach (var call in allCalls)
            {
                if (call.CallDuration.HasValue)
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

            var outcomesMap = new Dictionary<string, int>();
            foreach (var call in allCalls)
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
            foreach (var kvp in outcomesMap)
            {
                vm.Outcomes.Add(new CallOutcomeVM
                {
                    Label = kvp.Key,
                    Value = kvp.Value,
                    Color = colors[colorIndex % colors.Length]
                });
                colorIndex++;
            }

            var recentDays = allCalls.Where(c => c.CreatedOn > DateTime.UtcNow.AddDays(-7))
                                    .GroupBy(c => c.CreatedOn.Date)
                                    .OrderBy(g => g.Key);
            foreach (var group in recentDays)
            {
                vm.VolumeData.Add(new CallVolumeVM
                {
                    Hour = group.Key.ToString("dd MMM"),
                    Inbound = group.Count(),
                    Outbound = 0,
                    Missed = 0
                });
            }

            var recentLogs = allCalls.OrderByDescending(c => c.CreatedOn).Take(10).ToList();
            var userIds = recentLogs.Select(c => c.CreatedBy).Distinct().ToList();
            var users = await context.Users.Where(u => userIds.Contains(u.UserId)).ToDictionaryAsync(u => u.UserId);

            foreach (var call in recentLogs)
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
                    AudioLink = GenerateFreshSasUrl(call.AudioLink) ?? call.AudioLink,
                    Remarks = call.Remarks,
                    TenantId = call.TenantId,
                    TenantName = call.Tenant != null ? call.Tenant.Name : null,
                    CreatedByName = user != null ? user.FirstName + " " + user.LastName : null,
                    CallStatusName = call.CallStatusName != null ? call.CallStatusName.Name : null,
                    DirectionTypeName = call.DirectionTypeName != null ? call.DirectionTypeName.Name : null,
                    CallStatusId = call.CallStatusId ?? 0,
                    DirectionTypeId = call.DirectionTypeId ?? 0,
                    CreatedOn = call.CreatedOn,
                });
            }

            return vm;
        }

        private string GenerateFreshSasUrl(string storedUrl)
        {
            if (string.IsNullOrWhiteSpace(storedUrl))
            {
                logger.LogDebug("[Audio] storedUrl is null/empty, skipping SAS generation.");
                return null;
            }

            logger.LogDebug("[Audio] GenerateFreshSasUrl input: {Url}", storedUrl);

            try
            {
                var uri = new Uri(storedUrl);
                var blobUriBuilder = new BlobUriBuilder(uri);

                if (string.IsNullOrWhiteSpace(blobUriBuilder.BlobContainerName) || string.IsNullOrWhiteSpace(blobUriBuilder.BlobName))
                {
                    logger.LogWarning("[Audio] Cannot parse container/blob from stored URL '{Url}'.", storedUrl);
                    return null;
                }

                var credential = CreateSharedKeyCredential(blobUriBuilder.AccountName);
                if (credential == null)
                {
                    logger.LogWarning("[Audio] No matching storage credentials found for account '{AccountName}'. Returning stored URL.", blobUriBuilder.AccountName);
                    return storedUrl;
                }

                var blobClient = new BlobClient(uri, credential);

                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = blobUriBuilder.BlobContainerName,
                    BlobName = blobUriBuilder.BlobName,
                    Resource = "b",
                    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                    ExpiresOn = DateTimeOffset.UtcNow.AddYears(1),
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                var freshUrl = blobClient.GenerateSasUri(sasBuilder).ToString();
                logger.LogDebug("[Audio] SAS generated for host '{Host}'. Prefix: {Prefix}",
                    uri.Host,
                    freshUrl.Length > 100 ? freshUrl[..100] + "..." : freshUrl);

                return freshUrl;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[Audio] GenerateFreshSasUrl FAILED for '{Url}'.", storedUrl);
                return null;
            }
        }

        private StorageSharedKeyCredential CreateSharedKeyCredential(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return null;
            }

            var primaryConnectionString = configuration["AzureBlobStorage:ConnectionString"];
            var fallbackConnectionString = configuration.GetConnectionString("AzureBlobStorage");

            foreach (var connectionString in new[] { primaryConnectionString, fallbackConnectionString })
            {
                var credential = TryCreateSharedKeyCredential(connectionString, accountName);
                if (credential != null)
                {
                    return credential;
                }
            }

            return null;
        }

        private StorageSharedKeyCredential TryCreateSharedKeyCredential(string connectionString, string expectedAccountName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return null;
            }

            var segments = connectionString
                .Split(';', StringSplitOptions.RemoveEmptyEntries)
                .Select(segment => segment.Split('=', 2))
                .Where(parts => parts.Length == 2)
                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim(), StringComparer.OrdinalIgnoreCase);

            if (!segments.TryGetValue("AccountName", out var configuredAccountName) ||
                !segments.TryGetValue("AccountKey", out var accountKey))
            {
                return null;
            }

            if (!string.Equals(configuredAccountName, expectedAccountName, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return new StorageSharedKeyCredential(configuredAccountName, accountKey);
        }
    }
}
