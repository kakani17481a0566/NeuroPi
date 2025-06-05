using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Period;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class PeriodServiceImpl : IPeriodService
    {
        private readonly SchoolManagementDb _dbContext;

        public PeriodServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PeriodResponseVM> GetAll()
        {
            return _dbContext.Periods
                .Where(p => !p.IsDeleted)
                .Select(PeriodResponseVM.FromModel)
                .ToList();
        }

        public List<PeriodResponseVM> GetByTenantId(int tenantId)
        {
            return _dbContext.Periods
                .Where(p => !p.IsDeleted && p.TenantId == tenantId)
                .Select(PeriodResponseVM.FromModel)
                .ToList();
        }

        public PeriodResponseVM GetById(int id)
        {
            var entity = _dbContext.Periods.FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            return PeriodResponseVM.FromModel(entity);
        }

        public PeriodResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _dbContext.Periods.FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            return PeriodResponseVM.FromModel(entity);
        }

        public PeriodResponseVM Create(PeriodRequestVM model)
        {
            var entity = model.ToModel();
            _dbContext.Periods.Add(entity);
            _dbContext.SaveChanges();
            return PeriodResponseVM.FromModel(entity);
        }

        public PeriodResponseVM Update(int id, int tenantId, PeriodUpdateVM model)
        {
            var entity = _dbContext.Periods.FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (entity == null)
                return null;

            entity.Name = model.Name;
            entity.CourseId = model.CourseId;
            entity.StartTime = model.StartTime;
            entity.EndTime = model.EndTime;
            entity.UpdatedOn = DateTime.UtcNow;
            entity.UpdatedBy = model.UpdatedBy;

            _dbContext.SaveChanges();
            return PeriodResponseVM.FromModel(entity);
        }

        public bool Delete(int id, int tenantId)
        {
            var entity = _dbContext.Periods.FirstOrDefault(p => p.Id == id && p.TenantId == tenantId && !p.IsDeleted);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
