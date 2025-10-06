using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeePackage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.Services.Implementation
{
    public class FeePackageServiceImpl : IFeePackage
    {
        private readonly SchoolManagementDb _db;

        public FeePackageServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        public List<FeePackageResponseVM> GetAll(int tenantId, int branchId)
        {
            return _db.FeePackages
                .Where(f => f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted)
                .Select(f => new FeePackageResponseVM
                {
                    Id = f.Id,
                    FeeStructureId = f.FeeStructureId,
                    FeeStructureName = f.FeeStructure.Name,
                    BranchId = f.BranchId,
                    BranchName = f.Branch.Name,
                    CourseId = f.CourseId,
                    CourseName = f.Course.Name,
                    TenantId = f.TenantId,
                    TenantName = f.Tenant.Name,
                    TaxId = f.TaxId,
                    PaymentPeriod = f.PaymentPeriod
                })
                .ToList();
        }

        public FeePackageResponseVM GetById(int id, int tenantId, int branchId)
        {
            return _db.FeePackages
                .Where(f => f.Id == id && f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted)
                .Select(f => new FeePackageResponseVM
                {
                    Id = f.Id,
                    FeeStructureId = f.FeeStructureId,
                    FeeStructureName = f.FeeStructure.Name,
                    BranchId = f.BranchId,
                    BranchName = f.Branch.Name,
                    CourseId = f.CourseId,
                    CourseName = f.Course.Name,
                    TenantId = f.TenantId,
                    TenantName = f.Tenant.Name,
                    TaxId = f.TaxId,
                    PaymentPeriod = f.PaymentPeriod
                })
                .FirstOrDefault();
        }

        public int Create(FeePackageRequestVM vm)
        {
            var existing = _db.FeePackages.FirstOrDefault(f =>
                f.FeeStructureId == vm.FeeStructureId &&
                f.BranchId == vm.BranchId &&
                f.CourseId == vm.CourseId &&
                f.TenantId == vm.TenantId &&
                f.PaymentPeriod == vm.PaymentPeriod &&
                !f.IsDeleted);

            if (existing != null)
            {
                return existing.Id;
            }

            if (!vm.CreatedBy.HasValue)
                throw new InvalidOperationException("CreatedBy is required");

            var entity = new MFeePackage
            {
                FeeStructureId = vm.FeeStructureId,
                BranchId = vm.BranchId,
                CourseId = vm.CourseId,
                TenantId = vm.TenantId,
                TaxId = vm.TaxId,
                PaymentPeriod = vm.PaymentPeriod,
                CreatedBy = vm.CreatedBy.Value,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = vm.UpdatedBy ?? vm.CreatedBy.Value,
                UpdatedOn = DateTime.UtcNow
            };

            _db.FeePackages.Add(entity);
            _db.SaveChanges();
            return entity.Id;
        }

        public bool Update(int id, FeePackageRequestVM vm)
        {
            var entity = _db.FeePackages.FirstOrDefault(f =>
                f.Id == id && f.TenantId == vm.TenantId && f.BranchId == vm.BranchId && !f.IsDeleted);

            if (entity == null) return false;

            var duplicate = _db.FeePackages.FirstOrDefault(f =>
                f.Id != id &&
                f.FeeStructureId == vm.FeeStructureId &&
                f.BranchId == vm.BranchId &&
                f.CourseId == vm.CourseId &&
                f.TenantId == vm.TenantId &&
                f.PaymentPeriod == vm.PaymentPeriod &&
                !f.IsDeleted);

            if (duplicate != null)
                throw new InvalidOperationException("Another fee package with same details already exists.");

            entity.FeeStructureId = vm.FeeStructureId;
            entity.CourseId = vm.CourseId;
            entity.TaxId = vm.TaxId;
            entity.PaymentPeriod = vm.PaymentPeriod;

            if (!vm.UpdatedBy.HasValue)
                throw new InvalidOperationException("UpdatedBy is required");

            entity.UpdatedBy = vm.UpdatedBy.Value;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.FeePackages.Update(entity);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id, int tenantId, int branchId)
        {
            var entity = _db.FeePackages.FirstOrDefault(f =>
                f.Id == id && f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;

            if (!entity.UpdatedBy.HasValue)
                throw new InvalidOperationException("UpdatedBy must be set before deletion.");

            entity.UpdatedOn = DateTime.UtcNow;

            return _db.SaveChanges() > 0;
        }
    }
}
