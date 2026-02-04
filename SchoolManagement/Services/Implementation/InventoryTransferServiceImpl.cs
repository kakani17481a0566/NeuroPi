using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class InventoryTransferServiceImpl : IInventoryTransferService
    {
        private readonly SchoolManagementDb _context;
        private readonly IStockTransactionLogService _stockLogService;
        private readonly ISupplierPerformanceService _supplierPerformanceService;

        public InventoryTransferServiceImpl(
            SchoolManagementDb context, 
            IStockTransactionLogService stockLogService,
            ISupplierPerformanceService supplierPerformanceService)
        {
            _context = context;
            _stockLogService = stockLogService;
            _supplierPerformanceService = supplierPerformanceService;
        }

        public ResponseResult<InventoryTransferResponseVM> CreateRequest(InventoryTransferRequestVM request)
        {
            try
            {
                if (request.Quantity <= 0)
                    return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.BadRequest, null, "Quantity must be greater than 0.");

                if (request.TransferType == "TRANSFER" && request.FromBranchId == null)
                    return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.BadRequest, null, "From Branch is required for Transfers.");

                var transfer = new MInventoryTransfer
                {
                    TransferType = request.TransferType,
                    ItemId = request.ItemId,
                    Quantity = request.Quantity,
                    FromBranchId = request.FromBranchId,
                    ToBranchId = request.ToBranchId,
                    TenantId = request.TenantId,
                    Status = "PENDING",
                    CreatedBy = request.CreatedBy,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted = false,
                    SupplierId = (request.SupplierId == 0) ? null : request.SupplierId,
                    Size = request.Size
                };

                _context.InventoryTransfers.Add(transfer);
                _context.SaveChanges();

                // Reload the entity to get navigation properties for the response
                var loadedTransfer = _context.InventoryTransfers
                    .Include(t => t.Item)
                    .Include(t => t.FromBranch)
                    .Include(t => t.ToBranch)
                    .Include(t => t.Supplier)
                    .FirstOrDefault(t => t.Id == transfer.Id);

                return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.Created, MapToVM(loadedTransfer ?? transfer), "Request created successfully.");
            }
            catch (Exception ex)
            {
                return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<bool> ProcessApproval(InventoryTransferApprovalVM approval)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var transfer = _context.InventoryTransfers
                    .Include(t => t.FromBranch)
                    .Include(t => t.ToBranch)
                    .FirstOrDefault(t => t.Id == approval.Id && !t.IsDeleted);

                if (transfer == null)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Transfer request not found.");

                if (transfer.Status != "PENDING")
                    return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, $"Transfer is already {transfer.Status}.");

                if (approval.Status == "REJECTED")
                {
                    transfer.Status = "REJECTED";
                    transfer.ApprovedBy = approval.ApprovedBy == 0 ? null : approval.ApprovedBy;
                    transfer.ApprovalDate = DateTime.UtcNow;
                    transfer.UpdatedBy = approval.ApprovedBy == 0 ? null : approval.ApprovedBy;
                    transfer.UpdatedOn = DateTime.UtcNow;
                    _context.SaveChanges();
                    transaction.Commit();
                    return new ResponseResult<bool>(HttpStatusCode.OK, true, "Request rejected.");
                }

                if (approval.Status == "APPROVED")
                {
                    if (transfer.TransferType == "TRANSFER" && transfer.FromBranchId.HasValue)
                    {
                        var sourceStock = _context.ItemBranch
                            .FirstOrDefault(ib => ib.BranchId == transfer.FromBranchId && ib.ItemId == transfer.ItemId && !ib.IsDeleted);

                        if (sourceStock == null || sourceStock.ItemQuantity < transfer.Quantity)
                        {
                            return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Insufficient stock in source branch.");
                        }

                        _stockLogService.LogTransaction(new StockTransactionLogRequestVM
                        {
                            BranchId = transfer.FromBranchId.Value,
                            ItemId = transfer.ItemId,
                            TransactionType = "TRANSFER_OUT",
                            QuantityChange = -transfer.Quantity,
                            ReferenceType = "TRANSFER",
                            ReferenceId = transfer.Id
                        }, transfer.TenantId, approval.ApprovedBy);
                    }

                    _stockLogService.LogTransaction(new StockTransactionLogRequestVM
                    {
                        BranchId = transfer.ToBranchId,
                        ItemId = transfer.ItemId,
                        TransactionType = transfer.TransferType == "TRANSFER" ? "TRANSFER_IN" : "REFILL",
                        QuantityChange = transfer.Quantity,
                        ReferenceType = "TRANSFER",
                        ReferenceId = transfer.Id
                    }, transfer.TenantId, approval.ApprovedBy);

                    // RECORD SUPPLIER PERFORMANCE (If Refill)
                    if (transfer.TransferType == "REFILL" && transfer.SupplierId.HasValue)
                    {
                        _supplierPerformanceService.RecordPerformance(new SupplierPerformanceRequestVM
                        {
                            SupplierId = transfer.SupplierId.Value,
                            PoId = null, // transfer.Id cannot be used as PO ID because it FK references purchase_order_header
                            DeliveryDate = DateTime.UtcNow,
                            ExpectedDate = DateTime.UtcNow, // Simplified for now
                            OnTimeDelivery = true,
                            QualityRating = 5, // Defaulting to 5/5 for auto-approvals
                            QuantityAccuracyPct = 100,
                            Notes = "Auto-recorded from Refill Approval"
                        }, transfer.TenantId, approval.ApprovedBy );
                    }

                    transfer.Status = "APPROVED";
                    transfer.ApprovedBy = approval.ApprovedBy == 0 ? null : approval.ApprovedBy;
                    transfer.ApprovalDate = DateTime.UtcNow;
                    transfer.UpdatedBy = approval.ApprovedBy == 0 ? null : approval.ApprovedBy;
                    transfer.UpdatedOn = DateTime.UtcNow;

                    _context.SaveChanges();
                    transaction.Commit();

                    return new ResponseResult<bool>(HttpStatusCode.OK, true, "Stock transfer processing complete.");
                }

                return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Invalid Status.");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Processing failed: {ex.Message}");
            }
        }

        public ResponseResult<List<InventoryTransferResponseVM>> GetRequestsByBranch(int branchId, int tenantId)
        {
            try
            {
                var list = _context.InventoryTransfers
                    .Include(t => t.Item)
                    .Include(t => t.FromBranch)
                    .Include(t => t.ToBranch)
                    .Include(t => t.Supplier)
                    .Where(t => t.TenantId == tenantId && !t.IsDeleted && (t.ToBranchId == branchId || t.FromBranchId == branchId))
                    .OrderByDescending(t => t.CreatedOn)
                    .ToList();

                var vms = list.Select(t => MapToVM(t)).ToList();

                return new ResponseResult<List<InventoryTransferResponseVM>>(HttpStatusCode.OK, vms, "Requests fetched.");
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<InventoryTransferResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        private static InventoryTransferResponseVM MapToVM(MInventoryTransfer t)
        {
            return new InventoryTransferResponseVM
            {
                Id = t.Id,
                TransferType = t.TransferType,
                Quantity = t.Quantity,
                Size = t.Size,
                Status = t.Status,
                ItemId = t.ItemId,
                ItemName = t.Item?.Name ?? "Unknown Item",
                FromBranchId = t.FromBranchId,
                FromBranchName = t.FromBranch?.Name ?? "External/Vendor",
                ToBranchId = t.ToBranchId,
                ToBranchName = t.ToBranch?.Name ?? "Unknown",
                CreatedOn = t.CreatedOn,
                ApprovalDate = t.ApprovalDate,
                SupplierId = t.SupplierId,
                SupplierName = t.Supplier?.Name
            };
        }
    }
}
