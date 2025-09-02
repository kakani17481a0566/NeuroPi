using System;
using System.Collections.Generic;
using System.Linq;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PhoneLog;

namespace SchoolManagement.Services.Implementation
{
    public class PhoneLogServiceImpl : IPhoneLogService
    {
        private readonly SchoolManagementDb _dbContext;

        public PhoneLogServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<PhoneLogResponseVM> GetAll()
        {
            return _dbContext.PhoneLogs
                .Where(p => !p.IsDeleted)
                .Select(PhoneLogResponseVM.Map)
                .ToList();
        }

        public List<PhoneLogResponseVM> GetByTenantId(int tenantId)
        {
            return _dbContext.PhoneLogs
                .Where(p => !p.IsDeleted && p.tenant_id == tenantId)
                .Select(PhoneLogResponseVM.Map)
                .ToList();
        }

        public List<PhoneLogResponseVM> GetBybranch_id(int id)
        {
            return _dbContext.PhoneLogs
                .Where(p => !p.IsDeleted && p.branch_id == id)
                .Select(PhoneLogResponseVM.Map)
                .ToList();
        }

        public PhoneLogResponseVM create(PhoneLogVM model)
        {
            var entity = model.ToModel();
            _dbContext.PhoneLogs.Add(entity);
            _dbContext.SaveChanges();
            return PhoneLogResponseVM.FromModel(entity);
        }

        public PhoneLogResponseVM update(int id, int tenantId, PhoneLogVM model)
        {
            // If you have a primary key, filter by it; otherwise pick the row by tenant + whatever key you have
            var entity = _dbContext.PhoneLogs
                .FirstOrDefault(p => !p.IsDeleted && p.tenant_id == tenantId /* && p.Id == id */);

            if (entity == null) return null;

            // update allowed fields
            entity.branch_id = model.branch_id;
            entity.from_name = model.from_name;
            entity.to_name = model.to_name;
            entity.from_number = model.from_number;
            entity.to_number = model.to_number;
            entity.purpose = model.purpose;
            entity.call_duration = model.call_duration;
            // entity.UpdatedBy = <whoever is performing update>;
            entity.UpdatedOn = DateTime.UtcNow;

            _dbContext.SaveChanges();
            return PhoneLogResponseVM.FromModel(entity);
        }

        // Unimplemented overloads you had
        public PhoneLogResponseVM create(PhoneLogRequestVM model)
        {
            throw new NotImplementedException();
        }

        public List<PhoneLogResponseVM> update(int id, int tenantId, PhoneLogUpdateVM model)
        {
            throw new NotImplementedException();
        }
    }
}
