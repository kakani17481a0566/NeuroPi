using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableDetail;

namespace SchoolManagement.Services.Implementation
{
    public class TimeTableDetailServiceImpl : ITimeTableDetailService
    {
        private readonly SchoolManagementDb _dbContext;

        public TimeTableDetailServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TimeTableDetailResponseVM> GetAll()
        {
            return _dbContext.TimeTableDetails
                .Where(x => !x.IsDeleted)
                .Select(TimeTableDetailResponseVM.FromModel)
                .ToList();
        }

        public List<TimeTableDetailResponseVM> GetAll(int tenantId)
        {
            return _dbContext.TimeTableDetails
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(TimeTableDetailResponseVM.FromModel)
                .ToList();
        }

        public TimeTableDetailResponseVM GetById(int id)
        {
            var item = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            return TimeTableDetailResponseVM.FromModel(item);
        }

        public TimeTableDetailResponseVM GetById(int id, int tenantId)
        {
            var item = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            return TimeTableDetailResponseVM.FromModel(item);
        }

        public TimeTableDetailResponseVM Create(TimeTableDetailRequestVM vm)
        {
            var entity = vm.ToModel();
            _dbContext.TimeTableDetails.Add(entity);
            _dbContext.SaveChanges();

            return TimeTableDetailResponseVM.FromModel(entity);
        }

        public TimeTableDetailResponseVM Update(int id, int tenantId, TimeTableDetailUpdateVM vm)
        {
            var entity = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return null;

            entity.PeriodId = vm.PeriodId;
            entity.SubjectId = vm.SubjectId;
            entity.TimeTableId = vm.TimeTableId;
            entity.TeacherId = vm.TeacherId;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = vm.UpdatedBy;

            _dbContext.SaveChanges();

            return TimeTableDetailResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.TimeTableDetails
                .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);

            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return true;
        }
    }
}
