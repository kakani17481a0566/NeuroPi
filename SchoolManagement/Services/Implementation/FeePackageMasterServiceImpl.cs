using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeePackage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class FeePackageMasterServiceImpl : IFeePackageMaster
    {
        private readonly SchoolManagementDb _db;

        public FeePackageMasterServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public List<FeePackageMasterResponseVM> GetAll(int tenantId, int branchId)
        {
            return _db.FeePackageMasters
                .Where(m => m.TenantId == tenantId && m.BranchId == branchId && !m.IsDeleted)
                .Select(m => new FeePackageMasterResponseVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    BranchId = m.BranchId,
                    BranchName = m.Branch.Name,
                    TenantId = m.TenantId,
                    TenantName = m.Tenant.Name,
                    CourseId = m.CourseId,
                    CourseName = m.Course.Name
                }).ToList();
        }

        public FeePackageMasterResponseVM GetById(int id, int tenantId, int branchId)
        {
            return _db.FeePackageMasters
                .Where(m => m.Id == id && m.TenantId == tenantId && m.BranchId == branchId && !m.IsDeleted)
                .Select(m => new FeePackageMasterResponseVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    BranchId = m.BranchId,
                    BranchName = m.Branch.Name,
                    TenantId = m.TenantId,
                    TenantName = m.Tenant.Name,
                    CourseId = m.CourseId,
                    CourseName = m.Course.Name
                }).FirstOrDefault();
        }

        public int Create(FeePackageMasterRequestVM vm, int currentUserId)
        {
            // Normalize the name to avoid case-sensitive duplicates
            var normalizedName = vm.Name.Trim().ToLower();

            var existing = _db.FeePackageMasters.FirstOrDefault(m =>
                m.TenantId == vm.TenantId &&
                m.BranchId == vm.BranchId &&
                m.CourseId == vm.CourseId &&
                m.Name.ToLower() == normalizedName &&   // ✅ case-insensitive check
                !m.IsDeleted);

            if (existing != null)
            {
                // Instead of silently returning Id, throw error (clearer for API consumers)
                throw new InvalidOperationException(
                    $"Fee Package Master '{vm.Name}' already exists for this Branch, Tenant, and Course.");
            }

            var entity = new MFeePackageMaster
            {
                Name = vm.Name.Trim(),
                BranchId = vm.BranchId,
                TenantId = vm.TenantId,
                CourseId = vm.CourseId,
                CreatedBy = currentUserId,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = currentUserId,
                UpdatedOn = DateTime.UtcNow
            };

            _db.FeePackageMasters.Add(entity);
            _db.SaveChanges();
            return entity.Id;
        }

        public bool Update(int id, FeePackageMasterRequestVM vm, int currentUserId)
        {
            var entity = _db.FeePackageMasters.FirstOrDefault(m =>
                m.Id == id && m.TenantId == vm.TenantId && m.BranchId == vm.BranchId && !m.IsDeleted);

            if (entity == null) return false;

            var duplicate = _db.FeePackageMasters.FirstOrDefault(m =>
                m.Id != id &&
                m.Name == vm.Name &&
                m.TenantId == vm.TenantId &&
                m.BranchId == vm.BranchId &&
                m.CourseId == vm.CourseId &&
                !m.IsDeleted);

            if (duplicate != null)
                throw new InvalidOperationException("Duplicate package master exists.");

            entity.Name = vm.Name;
            entity.CourseId = vm.CourseId;
            entity.UpdatedBy = currentUserId;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.FeePackageMasters.Update(entity);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id, int tenantId, int branchId, int currentUserId)
        {
            var entity = _db.FeePackageMasters.FirstOrDefault(m =>
                m.Id == id && m.TenantId == tenantId && m.BranchId == branchId && !m.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedBy = currentUserId;
            entity.UpdatedOn = DateTime.UtcNow;

            return _db.SaveChanges() > 0;
        }
    }
}
