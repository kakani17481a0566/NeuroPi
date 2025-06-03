using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Week;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class WeekServiceImpl : IWeekService
    {
        private readonly SchoolManagementDb _dbContext;

        public WeekServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<WeekVm> GetAllWeeks()
        {
            return _dbContext.Weeks
                .Where(w => !w.IsDeleted)
                .Select(WeekVm.FromModel)
                .ToList();
        }

        // Get all weeks for a tenant
        public List<WeekVm> GetWeeksByTenantId(int tenantId)
        {
            return _dbContext.Weeks
                .Where(w => w.TenantId == tenantId && !w.IsDeleted)
                .Select(WeekVm.FromModel)
                .ToList();
        }

        // Get week by id and tenant
        public WeekVm GetWeekByIdAndTenantId(int id, int tenantId)
        {
            var week = _dbContext.Weeks
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            return WeekVm.FromModel(week);
        }

        // Get week by id only
        public WeekVm GetWeekById(int id)
        {
            var week = _dbContext.Weeks
                .FirstOrDefault(w => w.Id == id && !w.IsDeleted);

            return WeekVm.FromModel(week);
        }

        // Create a new week
        public WeekVm CreateWeek(WeekCreateVm request)
        {
            var model = request.ToModel();
            model.CreatedOn = DateTime.UtcNow;
            model.CreatedBy = request.CreatedBy;

            _dbContext.Weeks.Add(model);
            _dbContext.SaveChanges();

            return WeekVm.FromModel(model);
        }

        // Update existing week
        public WeekVm UpdateWeek(int id, int tenantId, WeekUpdateVm request)
        {
            var week = _dbContext.Weeks
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            if (week == null) return null;

            week.Name = request.Name;
            week.StartDate = request.StartDate;
            week.EndDate = request.EndDate;
            week.UpdatedOn = DateTime.UtcNow;
            week.UpdatedBy = request.UpdatedBy;

            _dbContext.SaveChanges();

            return WeekVm.FromModel(week);
        }

        // Soft delete week
        public bool DeleteWeek(int id, int tenantId)
        {
            var week = _dbContext.Weeks
                .FirstOrDefault(w => w.Id == id && w.TenantId == tenantId && !w.IsDeleted);

            if (week == null) return false;

            week.IsDeleted = true;
            week.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
