using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeeStructure;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagement.Services.Implementation
{
    public class FeeStructureServiceImpl : IFeeStructure
    {
        private readonly SchoolManagementDb _db;

        public FeeStructureServiceImpl(SchoolManagementDb db)
        {
            _db = db;
        }

        // 🔹 Create
        public ResponseResult<int> CreateFeeStructure(FeeStructureRequestVM vm)
        {
            var entity = new MFeeStructure
            {
                Name = vm.Name,
                Amount = vm.Amount,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _db.FeeStructures.Add(entity);
            _db.SaveChanges();

            return new ResponseResult<int>(HttpStatusCode.Created, entity.Id, "Fee structure created successfully");
        }

        // 🔹 Get By Id
        public ResponseResult<FeeStructureResponseVM> GetFeeStructureById(int id, int tenantId)
        {
            var fee = _db.FeeStructures
                .AsNoTracking()
                .Where(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted)
                .Select(f => new FeeStructureResponseVM
                {
                    Id = f.Id,
                    Name = f.Name,
                    Amount = f.Amount,
                    TenantId = f.TenantId,
                    CreatedOn = f.CreatedOn,
                    CreatedBy = f.CreatedBy,
                    UpdatedOn = f.UpdatedOn,
                    UpdatedBy = f.UpdatedBy,
                    IsDeleted = f.IsDeleted
                }).FirstOrDefault();

            if (fee == null)
                return new ResponseResult<FeeStructureResponseVM>(HttpStatusCode.NotFound, null, "Fee structure not found");

            return new ResponseResult<FeeStructureResponseVM>(HttpStatusCode.OK, fee, "Fee structure retrieved successfully");
        }

        // 🔹 Get All
        public ResponseResult<List<FeeStructureResponseVM>> GetAllFeeStructures(int tenantId)
        {
            var list = _db.FeeStructures
                .AsNoTracking()
                .Where(f => f.TenantId == tenantId && !f.IsDeleted)
                .Select(f => new FeeStructureResponseVM
                {
                    Id = f.Id,
                    Name = f.Name,
                    Amount = f.Amount,
                    TenantId = f.TenantId,
                    CreatedOn = f.CreatedOn,
                    CreatedBy = f.CreatedBy,
                    UpdatedOn = f.UpdatedOn,
                    UpdatedBy = f.UpdatedBy,
                    IsDeleted = f.IsDeleted
                }).ToList();

            return new ResponseResult<List<FeeStructureResponseVM>>(HttpStatusCode.OK, list, "Fee structures retrieved successfully");
        }

        // 🔹 Update
        public ResponseResult<bool> UpdateFeeStructure(int id, FeeStructureRequestVM vm)
        {
            var fee = _db.FeeStructures.FirstOrDefault(f => f.Id == id && f.TenantId == vm.TenantId && !f.IsDeleted);
            if (fee == null)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Fee structure not found");

            fee.Name = vm.Name;
            fee.Amount = vm.Amount;
            fee.UpdatedBy = vm.UpdatedBy;
            fee.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();

            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Fee structure updated successfully");
        }

        // 🔹 Soft Delete
        public ResponseResult<bool> DeleteFeeStructure(int id, int tenantId)
        {
            var fee = _db.FeeStructures.FirstOrDefault(f => f.Id == id && f.TenantId == tenantId && !f.IsDeleted);
            if (fee == null)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Fee structure not found");

            fee.IsDeleted = true;
            fee.UpdatedOn = DateTime.UtcNow;

            _db.SaveChanges();

            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Fee structure deleted successfully");
        }
    }
}
