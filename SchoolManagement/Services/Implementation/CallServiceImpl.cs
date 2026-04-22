using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Call;
using DriveFile = Google.Apis.Drive.v3.Data.File;

namespace SchoolManagement.Services.Implementation
{
    public class CallServiceImpl : ICallService
    {
        private static readonly string[] DriveScopes = [DriveService.Scope.Drive];

        private readonly SchoolManagementDb context;
        private readonly string googleCredentialPath;
        private readonly string googleDriveFolderId;

        public CallServiceImpl(SchoolManagementDb _context, IConfiguration configuration)
        {
            context = _context;
            googleCredentialPath = configuration["GoogleDrive:CredentialsPath"];
            googleDriveFolderId = configuration["GoogleDrive:FolderId"];
        }

        public List<CallResponseVM> GetAllEmployeeLogs(int empId, int tenantId)
        {
            var result = context.Call.Where(e => e.ContactId == empId && e.TenantId == tenantId).Include(e => e.Stage).Include(c => c.Contact).ToList();
            if (result != null && result.Count() > 0)
            {
                return result.Select(c => new CallResponseVM()
                {
                    Id = c.Id,
                    ContactId = c.ContactId,
                    Contact = c.Contact?.Name,
                    StageId = c.StageId,
                    Stage = c.Stage?.Name,
                    AudioLink = c.AudioLink,
                    Remarks = c.Remarks,
                    TenantId = c.TenantId,

                }).ToList();
            }
            return null;
        }

        public List<CallResponseVM> GetAllLogs(int tenantId)
        {
            var result = context.Call.Where(e => e.TenantId == tenantId).Include(e => e.Stage).Include(c => c.Contact).ToList();
            if (result != null && result.Count() > 0)
            {
                return result.Select(c => new CallResponseVM()
                {
                    Id = c.Id,
                    ContactId = c.ContactId,
                    Contact = c.Contact?.Name,
                    StageId = c.StageId,
                    Stage = c.Stage?.Name,
                    AudioLink = c.AudioLink,
                    Remarks = c.Remarks,
                    TenantId = c.TenantId,

                }).ToList();
            }
            return null;
        }

        public async Task<CallResponseVM> AddCallAsync(CallCreateVM request)
        {
            var audioLink = await UploadAudioToGoogleDriveAsync(request.AudioFile);

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
                .FirstAsync();

            return new CallResponseVM
            {
                Id = savedCall.Id,
                ContactId = savedCall.ContactId,
                Contact = savedCall.Contact?.Name,
                StageId = savedCall.StageId,
                Stage = savedCall.Stage?.Name,
                AudioLink = savedCall.AudioLink,
                Remarks = savedCall.Remarks,
                TenantId = savedCall.TenantId
            };
        }

        private async Task<string> UploadAudioToGoogleDriveAsync(IFormFile audioFile)
        {
            if (string.IsNullOrWhiteSpace(googleCredentialPath))
            {
                throw new InvalidOperationException("Google Drive credential path is missing in configuration.");
            }

            if (!System.IO.File.Exists(googleCredentialPath))
            {
                throw new FileNotFoundException("Google Drive credential file was not found.", googleCredentialPath);
            }

            if (string.IsNullOrWhiteSpace(googleDriveFolderId))
            {
                throw new InvalidOperationException("Google Drive folder id is missing in configuration. Set GoogleDrive:FolderId to a shared drive folder that is accessible by the service account.");
            }

            GoogleCredential credential;
            await using (var stream = new FileStream(googleCredentialPath, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(DriveScopes);
            }

            var driveService = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "SchoolManagement"
            });

            var fileExtension = Path.GetExtension(audioFile.FileName);
            var driveFile = new DriveFile
            {
                Name = $"call-audio-{DateTime.UtcNow:yyyyMMddHHmmssfff}{fileExtension}",
                Parents = [googleDriveFolderId]
            };

            await using var uploadStream = audioFile.OpenReadStream();
            var uploadRequest = driveService.Files.Create(
                driveFile,
                uploadStream,
                string.IsNullOrWhiteSpace(audioFile.ContentType) ? "application/octet-stream" : audioFile.ContentType);

            uploadRequest.Fields = "id";
            uploadRequest.SupportsAllDrives = true;
            var uploadProgress = await uploadRequest.UploadAsync();
            if (uploadProgress.Status == UploadStatus.Failed)
            {
                throw new InvalidOperationException($"Google Drive upload failed: {uploadProgress.Exception?.Message}", uploadProgress.Exception);
            }

            if (uploadProgress.Status != UploadStatus.Completed)
            {
                throw new InvalidOperationException($"Google Drive upload did not complete. Status: {uploadProgress.Status}");
            }

            var uploadedFileId = uploadRequest.ResponseBody?.Id;
            if (string.IsNullOrWhiteSpace(uploadedFileId))
            {
                uploadedFileId = await FindUploadedFileIdByNameAsync(driveService, driveFile.Name);
            }

            if (string.IsNullOrWhiteSpace(uploadedFileId))
            {
                throw new InvalidOperationException("Google Drive upload completed, but the uploaded file id could not be resolved.");
            }

            var permission = new Permission
            {
                Type = "anyone",
                Role = "reader"
            };

            var permissionRequest = driveService.Permissions.Create(permission, uploadedFileId);
            permissionRequest.SupportsAllDrives = true;
            await permissionRequest.ExecuteAsync();

            return $"https://drive.google.com/uc?export=download&id={uploadedFileId}";
        }

        private async Task<string?> FindUploadedFileIdByNameAsync(DriveService driveService, string fileName)
        {
            var escapedFileName = fileName.Replace("'", "\\'");
            var listRequest = driveService.Files.List();
            listRequest.Q = $"name = '{escapedFileName}' and '{googleDriveFolderId}' in parents and trashed = false";
            listRequest.OrderBy = "createdTime desc";
            listRequest.PageSize = 1;
            listRequest.Fields = "files(id,name)";
            listRequest.IncludeItemsFromAllDrives = true;
            listRequest.SupportsAllDrives = true;

            var files = await listRequest.ExecuteAsync();
            return files.Files?.FirstOrDefault()?.Id;
        }
    }
}
