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

        public CallServiceImpl(
            SchoolManagementDb _context,
            BlobServiceClient _blobServiceClient,
            IConfiguration _configuration)
        {
            context = _context;
            blobServiceClient = _blobServiceClient;
            configuration = _configuration;
        }

        public List<CallResponseVM> GetAllEmployeeLogs(int empId, int tenantId)
        {
            var result = (from c in context.Call.Include(e => e.Stage).Include(c => c.Contact).Include(c => c.Tenant)
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
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null
                          }).ToList();
            if (result != null && result.Count() > 0)
            {
                return result;
            }

            return null;
        }

        public List<CallResponseVM> GetAllLogs(int tenantId)
        {
            var result = (from c in context.Call.Include(e => e.Stage).Include(c => c.Contact).Include(c => c.Tenant)
                          join u in context.Users on c.CreatedBy equals u.UserId into uGroup
                          from user in uGroup.DefaultIfEmpty()
                          where c.TenantId == tenantId
                          select new CallResponseVM()
                          {
                              Id = c.Id,
                              ContactId = c.ContactId,
                              Contact = c.Contact != null ? c.Contact.Name : null,
                              StageId = c.StageId,
                              Stage = c.Stage != null ? c.Stage.Name : null,
                              AudioLink = c.AudioLink,
                              Remarks = c.Remarks,
                              TenantId = c.TenantId,
                              TenantName = c.Tenant != null ? c.Tenant.Name : null,
                              CreatedByName = user != null ? user.FirstName + " " + user.LastName : null
                          }).ToList();
            if (result != null && result.Count() > 0)
            {
                return result;
            }

            return null;
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
    }
}
