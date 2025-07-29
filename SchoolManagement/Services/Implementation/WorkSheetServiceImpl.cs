using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Worksheets;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class WorkSheetServiceImpl : IWorkSheetService
    {
        private readonly SchoolManagementDb _dbContext;
        private readonly Cloudinary _cloudinary;

        public WorkSheetServiceImpl(SchoolManagementDb dbContext, Cloudinary cloudinary)
        {
            _dbContext = dbContext;
            _cloudinary = cloudinary;
        }

        public List<WorksheetResponseVM> GetAll()
        {
            return _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .Where(w => !w.IsDeleted)
                .Select(WorksheetResponseVM.FromModel)
                .ToList();
        }

        public List<WorksheetResponseVM> GetAll(int tenantId)
        {
            return _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .Where(w => w.TenantId == tenantId && !w.IsDeleted)
                .Select(WorksheetResponseVM.FromModel)
                .ToList();
        }

        public WorksheetResponseVM GetById(int id)
        {
            var item = _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .FirstOrDefault(w => w.Id == id && !w.IsDeleted);

            return WorksheetResponseVM.FromModel(item);
        }

        public WorksheetResponseVM GetById(int id, int tenantId)
        {
            var item = _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            return WorksheetResponseVM.FromModel(item);
        }

        public WorksheetResponseVM Create(WorksheetRequestVM request)
        {
            string uploadedUrl = null;

            if (request.File != null && request.File.Length > 0)
            {
                var extension = Path.GetExtension(request.File.FileName).ToLower();

                // ✅ Only allow .pdf files
                if (extension != ".pdf")
                    throw new InvalidOperationException("Only PDF files are allowed.");

                using (var stream = request.File.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(request.File.FileName, stream),
                        Folder = "worksheets",
                        UseFilename = true,
                        UniqueFilename = false,
                        Overwrite = false,
                        Type = "upload" // Ensure it's public
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);
                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                        uploadedUrl = uploadResult.SecureUrl.AbsoluteUri;
                    else
                        throw new Exception("Cloudinary upload failed.");
                }
            }

            var model = new MWorksheet
            {
                Name = request.Name,
                Description = request.Description,
                Location = uploadedUrl,
                TenantId = request.TenantId,
                SubjectId = request.SubjectId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _dbContext.Worksheets.Add(model);
            _dbContext.SaveChanges();

            model = _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .FirstOrDefault(w => w.Id == model.Id);

            return WorksheetResponseVM.FromModel(model);
        }

        public WorksheetResponseVM Update(int id, int tenantId, WorksheetUpdateVM request)
        {
            var item = _dbContext.Worksheets
                .Include(w => w.Tenant)
                .Include(w => w.Subject)
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            if (item == null) return null;

            item.Name = request.Name;
            item.Description = request.Description;
            item.SubjectId = request.SubjectId;
            item.UpdatedOn = DateTime.UtcNow;
            item.UpdatedBy = request.UpdatedBy;

            if (request.File != null && request.File.Length > 0)
            {
                using (var stream = request.File.OpenReadStream())
                {
                    var uploadParams = new RawUploadParams
                    {
                        File = new FileDescription(request.File.FileName, stream),
                        Folder = "worksheets"
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);
                    if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                        item.Location = uploadResult.SecureUrl.AbsoluteUri;
                    else
                        throw new Exception("Cloudinary upload failed.");
                }
            }

            _dbContext.SaveChanges();

            return WorksheetResponseVM.FromModel(item);
        }

        public bool Delete(int id, int tenantId)
        {
            var item = _dbContext.Worksheets
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            if (item == null) return false;

            item.IsDeleted = true;
            item.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
