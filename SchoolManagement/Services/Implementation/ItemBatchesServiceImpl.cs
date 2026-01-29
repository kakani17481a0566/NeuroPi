using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class ItemBatchesServiceImpl : IItemBatchesService
    {
        private readonly SchoolManagementDb _context;

        public ItemBatchesServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<ItemBatchResponseVM> CreateBatch(ItemBatchRequestVM request, int tenantId, int userId)
        {
            try
            {
                var batch = new MItemBatch
                {
                    ItemId = request.ItemId,
                    BranchId = request.BranchId,
                    BatchNumber = request.BatchNumber,
                    ExpiryDate = request.ExpiryDate,
                    ManufactureDate = request.ManufactureDate,
                    ReceivedDate = request.ReceivedDate ?? DateTime.UtcNow,
                    QuantityRemaining = request.QuantityRemaining,
                    QualityStatus = request.QualityStatus,
                    QualityNotes = request.QualityNotes,
                    SupplierId = request.SupplierId,
                    CertificateNumber = request.CertificateNumber,
                    TenantId = tenantId,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false
                };

                _context.Set<MItemBatch>().Add(batch); // Using Set<> to be safe
                _context.SaveChanges();

                // Map to Response
                var response = MapToResponse(batch);
                return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.Created, response, "Batch created successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.InternalServerError, null, $"Error creating batch: {ex.Message}");
            }
        }

        public ResponseResult<ItemBatchResponseVM> UpdateBatch(int id, ItemBatchRequestVM request, int tenantId, int userId)
        {
            var batch = _context.Set<MItemBatch>().FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            if (batch == null)
            {
                return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.NotFound, null, "Batch not found");
            }

            batch.BatchNumber = request.BatchNumber;
            batch.ExpiryDate = request.ExpiryDate;
            batch.ManufactureDate = request.ManufactureDate;
            batch.ReceivedDate = request.ReceivedDate;
            batch.QuantityRemaining = request.QuantityRemaining;
            batch.QualityStatus = request.QualityStatus;
            batch.QualityNotes = request.QualityNotes;
            batch.SupplierId = request.SupplierId;
            batch.CertificateNumber = request.CertificateNumber;
            batch.UpdatedBy = userId;
            batch.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            var response = MapToResponse(batch);
            return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.OK, response, "Batch updated successfully");
        }

        public ResponseResult<ItemBatchResponseVM> GetBatchById(int id, int tenantId)
        {
            var batch = _context.Set<MItemBatch>()
                .Include(b => b.Item)
                .Include(b => b.Branch)
                .Include(b => b.Supplier)
                .FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);

            if (batch == null)
            {
                return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.NotFound, null, "Batch not found");
            }

            return new ResponseResult<ItemBatchResponseVM>(HttpStatusCode.OK, MapToResponse(batch), "Batch retrieved successfully");
        }

        public ResponseResult<List<ItemBatchResponseVM>> GetBatchesByItem(int itemId, int tenantId)
        {
            var batches = _context.Set<MItemBatch>()
                .Include(b => b.Item)
                .Include(b => b.Branch)
                .Include(b => b.Supplier)
                .Where(b => b.ItemId == itemId && b.TenantId == tenantId && !b.IsDeleted)
                .ToList();

            var response = batches.Select(MapToResponse).ToList();
            return new ResponseResult<List<ItemBatchResponseVM>>(HttpStatusCode.OK, response, "Batches retrieved successfully");
        }

        public ResponseResult<List<ItemBatchResponseVM>> GetBatchesByBranch(int branchId, int tenantId)
        {
             var batches = _context.Set<MItemBatch>()
                .Include(b => b.Item)
                .Include(b => b.Branch)
                .Include(b => b.Supplier)
                .Where(b => b.BranchId == branchId && b.TenantId == tenantId && !b.IsDeleted)
                .ToList();

            var response = batches.Select(MapToResponse).ToList();
            return new ResponseResult<List<ItemBatchResponseVM>>(HttpStatusCode.OK, response, "Batches retrieved successfully");
        }

        public ResponseResult<bool> DeleteBatch(int id, int tenantId, int userId)
        {
            var batch = _context.Set<MItemBatch>().FirstOrDefault(b => b.Id == id && b.TenantId == tenantId && !b.IsDeleted);
            if (batch == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Batch not found");
            }

            batch.IsDeleted = true;
            batch.UpdatedBy = userId;
            batch.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Batch deleted successfully");
        }

        private ItemBatchResponseVM MapToResponse(MItemBatch batch)
        {
            return new ItemBatchResponseVM
            {
                Id = batch.Id,
                ItemId = batch.ItemId,
                ItemName = batch.Item?.Name ?? "",
                BranchId = batch.BranchId,
                BranchName = batch.Branch?.Name ?? "",
                BatchNumber = batch.BatchNumber,
                ExpiryDate = batch.ExpiryDate,
                ManufactureDate = batch.ManufactureDate,
                ReceivedDate = batch.ReceivedDate,
                QuantityRemaining = batch.QuantityRemaining,
                QualityStatus = batch.QualityStatus,
                QualityNotes = batch.QualityNotes,
                SupplierId = batch.SupplierId,
                SupplierName = batch.Supplier?.Name,
                CertificateNumber = batch.CertificateNumber,
                CreatedOn = batch.CreatedOn
            };
        }
    }
}
