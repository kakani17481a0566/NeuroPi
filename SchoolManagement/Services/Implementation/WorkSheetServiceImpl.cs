using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Worksheets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class WorkSheetServiceImpl : IWorkSheetService
    {
        private readonly SchoolManagementDb _dbContext;

        public WorkSheetServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
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
            var model = new MWorksheet
            {
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
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

            // Update location only if a new URL is provided
            if (!string.IsNullOrWhiteSpace(request.Location))
            {
                item.Location = request.Location;
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
