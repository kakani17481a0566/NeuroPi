using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTable;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableServiceImpl : ITimeTableServices
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TimeTableResponseVM> GetAll()
        {
            return _dbContext.TimeTables
                .Where(x => !x.IsDeleted)
                .Select(TimeTableResponseVM.FromModel)
                .ToList();
        }

        public List<TimeTableResponseVM> GetAll(int tenantId)
        {
            return _dbContext.TimeTables
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(TimeTableResponseVM.FromModel)
                .ToList();
        }

        public TimeTableResponseVM GetById(int id)
        {
            var model = _dbContext.TimeTables.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return TimeTableResponseVM.FromModel(model);
        }

        public TimeTableResponseVM GetById(int id, int tenantId)
        {
            var model = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return TimeTableResponseVM.FromModel(model);
        }

        public TimeTableResponseVM Create(TimeTableRequestVM vm)
        {
            var entity = vm.ToModel();
            _dbContext.TimeTables.Add(entity);
            _dbContext.SaveChanges();

            return TimeTableResponseVM.FromModel(entity);
        }

        public TimeTableResponseVM Update(int id, int tenantId, TimeTableUpdateVM vm)
        {
            var entity = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return null;

            entity.Name = vm.Name;
            entity.Date = vm.Date;
            entity.WeekId = vm.WeekId;
            entity.HolidayId = vm.HolidayId;
            entity.Status = vm.Status;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = vm.UpdatedBy;

            _dbContext.SaveChanges();

            return TimeTableResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.TimeTables
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
