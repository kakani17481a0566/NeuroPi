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

        // 🔹 Small helper to convert FeeType int → friendly string
        private string GetFeeTypeName(int feeType) =>
            feeType switch
            {
                0 => "Tuition",
                1 => "Transport",
                2 => "Hostel",
                3 => "Miscellaneous",
                _ => "Other"
            };

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
                    PaymentPeriodId = f.PaymentPeriod,
                    PaymentPeriodName = f.PaymentPeriodMaster.Name,
                    PackageMasterId = f.PackageMasterId,
                    PackageMasterName = f.PackageMaster != null ? f.PackageMaster.Name : null,
                    FeeTypeId = f.FeeType,
                    FeeTypeName = GetFeeTypeName(f.FeeType)
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
                    PaymentPeriodId = f.PaymentPeriod,
                    PaymentPeriodName = f.PaymentPeriodMaster.Name,
                    PackageMasterId = f.PackageMasterId,
                    PackageMasterName = f.PackageMaster != null ? f.PackageMaster.Name : null,
                    FeeTypeId = f.FeeType,
                    FeeTypeName = GetFeeTypeName(f.FeeType)
                })
                .FirstOrDefault();
        }

        public int Create(FeePackageRequestVM vm, int currentUserId)
        {
            var existing = _db.FeePackages.FirstOrDefault(f =>
                f.FeeStructureId == vm.FeeStructureId &&
                f.BranchId == vm.BranchId &&
                f.CourseId == vm.CourseId &&   // ✅ fixed
                f.TenantId == vm.TenantId &&
                f.PaymentPeriod == vm.PaymentPeriod &&
                f.FeeType == vm.FeeType &&
                !f.IsDeleted);

            if (existing != null)
            {
                return existing.Id;
            }

            var entity = new MFeePackage
            {
                FeeStructureId = vm.FeeStructureId,
                BranchId = vm.BranchId,
                CourseId = vm.CourseId,
                TenantId = vm.TenantId,
                TaxId = vm.TaxId,
                PackageMasterId = vm.PackageMasterId,
                PaymentPeriod = vm.PaymentPeriod,
                FeeType = vm.FeeType,
                CreatedBy = currentUserId,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = currentUserId,
                UpdatedOn = DateTime.UtcNow
            };

            _db.FeePackages.Add(entity);
            _db.SaveChanges();
            return entity.Id;
        }

        public bool Update(int id, FeePackageRequestVM vm, int currentUserId)
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
                f.FeeType == vm.FeeType &&
                !f.IsDeleted);

            if (duplicate != null)
                throw new InvalidOperationException("Another fee package with same details already exists.");

            entity.FeeStructureId = vm.FeeStructureId;
            entity.CourseId = vm.CourseId;
            entity.TaxId = vm.TaxId;
            entity.PaymentPeriod = vm.PaymentPeriod;
            entity.PackageMasterId = vm.PackageMasterId;
            entity.FeeType = vm.FeeType;
            entity.UpdatedBy = currentUserId;
            entity.UpdatedOn = DateTime.UtcNow;

            _db.FeePackages.Update(entity);
            return _db.SaveChanges() > 0;
        }

        public bool Delete(int id, int tenantId, int branchId, int currentUserId)
        {
            var entity = _db.FeePackages.FirstOrDefault(f =>
                f.Id == id && f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted);

            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.UpdatedBy = currentUserId;
            entity.UpdatedOn = DateTime.UtcNow;

            return _db.SaveChanges() > 0;
        }

        public List<FeePackageListVM> GetPackageList(int tenantId, int branchId)
        {
            return _db.FeePackages
                .Where(f => f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted)
                .Select(f => new FeePackageListVM
                {
                    Id = f.Id,
                    CourseId = f.CourseId,
                    CourseName = f.Course.Name,
                    PackageMasterId = f.PackageMasterId ?? 0,
                    PackageName = f.PackageMaster != null ? f.PackageMaster.Name : null,
                    FeeStructureId = f.FeeStructureId,
                    FeeStructureName = f.FeeStructure.Name,
                    Amount = f.FeeStructure.Amount,
                    PaymentPeriodId = f.PaymentPeriod,
                    PaymentPeriodName = f.PaymentPeriodMaster.Name,
                    FeeTypeId = f.FeeType,
                    FeeTypeName = GetFeeTypeName(f.FeeType)
                })
                .ToList();
        }

        public List<FeePackageGroupVM> GetGroupedPackages(int tenantId, int branchId)
        {
            return _db.FeePackages
                .Where(f => f.TenantId == tenantId && f.BranchId == branchId && !f.IsDeleted)
                .GroupBy(f => new
                {
                    f.PackageMasterId,
                    PackageName = f.PackageMaster != null ? f.PackageMaster.Name : null,
                    f.CourseId,
                    CourseName = f.Course.Name
                })
                .Select(g => new FeePackageGroupVM
                {
                    PackageMasterId = g.Key.PackageMasterId ?? 0,
                    PackageName = g.Key.PackageName,
                    CourseId = g.Key.CourseId,
                    CourseName = g.Key.CourseName,
                    BranchId = g.First().BranchId,
                    BranchName = g.First().Branch.Name,
                    TenantId = g.First().TenantId,
                    TenantName = g.First().Tenant.Name,
                    Items = g.Select(f => new FeePackageItemVM
                    {
                        Id = f.Id,
                        FeeStructureId = f.FeeStructureId,
                        FeeStructureName = f.FeeStructure.Name,
                        Amount = f.FeeStructure.Amount,
                        PaymentPeriodId = f.PaymentPeriod,
                        PaymentPeriodName = f.PaymentPeriodMaster.Name,
                        FeeTypeId = f.FeeType,
                        FeeTypeName = GetFeeTypeName(f.FeeType)
                    }).ToList()
                })
                .ToList();
        }
    }
}
