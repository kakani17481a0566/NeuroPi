using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public GeneralController(IConfiguration configuration, IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            string connectionString = _configuration.GetSection("AzureBlobStorage:ConnectionString").Value;

            // Check if using development storage (emulator)
            bool useDevelopmentStorage = connectionString == "UseDevelopmentStorage=true";

            if (useDevelopmentStorage)
            {
                // Use local file storage as fallback
                try
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath ?? _environment.ContentRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Return URL that can be accessed via static files
                    string fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                    return Ok(new { Url = fileUrl });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Local file storage error: {ex.Message}. Internal: {ex.InnerException?.Message}");
                }
            }

            // Use Azure Blob Storage for production
            string containerName = "student-docs";

            if (string.IsNullOrEmpty(connectionString))
                return StatusCode(500, "Azure Blob Storage connection string is missing in configuration.");

            try
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync();

                string blobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true);
                }

                // Generate SAS Token
                var sasBuilder = new Azure.Storage.Sas.BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = blobName,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddYears(1) // Reduced from 100 for safety, but still very long
                };
                sasBuilder.SetPermissions(Azure.Storage.Sas.BlobSasPermissions.Read);

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);

                return Ok(new { Url = sasUri.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error during Azure upload: {ex.Message}. Details: {ex.InnerException?.Message}");
            }
        }
    }
}
