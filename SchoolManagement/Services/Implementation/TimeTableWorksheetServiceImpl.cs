using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableWorksheet;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableWorksheetServiceImpl : ITimeTableWorksheetService
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableWorksheetServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        // Get all worksheets (Admin/global - no tenant filter)
        public List<TimeTableWorksheetResponseVM> GetAll()
        {
            return _dbContext.TimeTableWorksheets
                .Where(ttw => !ttw.IsDeleted)
                .Select(TimeTableWorksheetResponseVM.FromModel)
                .ToList();
        }

        // Get all worksheets for a specific tenant
        public List<TimeTableWorksheetResponseVM> GetAll(int tenantId)
        {
            return _dbContext.TimeTableWorksheets
                .Where(ttw => ttw.TenantId == tenantId && !ttw.IsDeleted)
                .Select(TimeTableWorksheetResponseVM.FromModel)
                .ToList();
        }

        // Get worksheet by ID (Admin/global)
        public TimeTableWorksheetResponseVM GetById(int id)
        {
            var item = _dbContext.TimeTableWorksheets
                .FirstOrDefault(ttw => ttw.Id == id && !ttw.IsDeleted);

            return TimeTableWorksheetResponseVM.FromModel(item);
        }

        // Get worksheet by ID and tenant
        public TimeTableWorksheetResponseVM GetById(int id, int tenantId)
        {
            var item = _dbContext.TimeTableWorksheets
                .FirstOrDefault(ttw => ttw.Id == id && ttw.TenantId == tenantId && !ttw.IsDeleted);

            return TimeTableWorksheetResponseVM.FromModel(item);
        }

        // Create new worksheet
        public TimeTableWorksheetResponseVM Create(TimeTableWorksheetRequestVM request)
        {
            var model = request.ToModel();
            model.CreatedOn = DateTime.UtcNow;
            _dbContext.TimeTableWorksheets.Add(model);
            _dbContext.SaveChanges();

            return TimeTableWorksheetResponseVM.FromModel(model);
        }

        // Update worksheet by id and tenantId
        public TimeTableWorksheetResponseVM Update(int id, int tenantId, TimeTableWorksheetUpdateVM request)
        {
            var item = _dbContext.TimeTableWorksheets
                .FirstOrDefault(ttw => ttw.Id == id && ttw.TenantId == tenantId && !ttw.IsDeleted);

            if (item == null)
                return null;

            item.TimeTableId = request.TimeTableId;
            item.WorksheetId = request.WorksheetId;
            item.UpdatedOn = DateTime.UtcNow;
            item.UpdatedBy = request.UpdatedBy;

            _dbContext.SaveChanges();

            return TimeTableWorksheetResponseVM.FromModel(item);
        }

        // Soft delete worksheet by id and tenantId
        public bool Delete(int id, int tenantId)
        {
            var item = _dbContext.TimeTableWorksheets
                .FirstOrDefault(ttw => ttw.Id == id && ttw.TenantId == tenantId && !ttw.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;
            item.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
