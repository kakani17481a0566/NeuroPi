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

        public InventoryTransferServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<InventoryTransferResponseVM> CreateRequest(InventoryTransferRequestVM request)
        {
            try
            {
                // Validate Basic Input
                if (request.Quantity <= 0)
                    return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.BadRequest, null, "Quantity must be greater than 0.");

                // For Transfers, Source Branch is required
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
                    IsDeleted = false
                };

                _context.Set<MInventoryTransfer>().Add(transfer);
                _context.SaveChanges();

                return new ResponseResult<InventoryTransferResponseVM>(HttpStatusCode.Created, MapToVM(transfer), "Request created successfully.");
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
                var transfer = _context.Set<MInventoryTransfer>()
                    .Include(t=>t.FromBranch)
                    .Include(t=>t.ToBranch)
                    .FirstOrDefault(t => t.Id == approval.Id && !t.IsDeleted);

                if (transfer == null)
                    return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Transfer request not found.");

                if (transfer.Status != "PENDING")
                    return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, $"Transfer is already {transfer.Status}.");

                if (approval.Status == "REJECTED")
                {
                    transfer.Status = "REJECTED";
                    transfer.ApprovedBy = approval.ApprovedBy;
                    transfer.ApprovalDate = DateTime.UtcNow;
                    transfer.UpdatedBy = approval.ApprovedBy;
                    transfer.UpdatedOn = DateTime.UtcNow;
                    _context.SaveChanges();
                    transaction.Commit();
                    return new ResponseResult<bool>(HttpStatusCode.OK, true, "Request rejected.");
                }

                if (approval.Status == "APPROVED")
                {
                    // 1. Handle Source Stock (If Transfer)
                    if (transfer.TransferType == "TRANSFER" && transfer.FromBranchId.HasValue)
                    {
                        var sourceStock = _context.ItemBranch
                            .FirstOrDefault(ib => ib.BranchId == transfer.FromBranchId && ib.ItemId == transfer.ItemId && !ib.IsDeleted);
                        
                        // Check availability
                        if (sourceStock == null || sourceStock.ItemQuantity < transfer.Quantity)
                        {
                            return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Insufficient stock in source branch.");
                        }

                        // Deduct
                        sourceStock.ItemQuantity -= transfer.Quantity;
                        sourceStock.UpdatedOn = DateTime.UtcNow;
                        _context.ItemBranch.Update(sourceStock);
                    }

                    // 2. Handle Target Stock
                    var targetStock = _context.ItemBranch
                         .FirstOrDefault(ib => ib.BranchId == transfer.ToBranchId && ib.ItemId == transfer.ItemId && !ib.IsDeleted);

                    if (targetStock != null)
                    {
                        // Add to existing
                        targetStock.ItemQuantity += transfer.Quantity;
                        targetStock.UpdatedOn = DateTime.UtcNow;
                        _context.ItemBranch.Update(targetStock);
                    }
                    else
                    {
                        // Create new Record if not exists
                        // Note: We need some defaults for Price/Cost if creating new.
                        // Ideally we copy from Source or Item Master. Using 0 for now or fetch Master.
                        
                        var newItemBranch = new MItemBranch
                        {
                            BranchId = transfer.ToBranchId,
                            ItemId = transfer.ItemId,
                            ItemQuantity = transfer.Quantity,
                            TenantId = transfer.TenantId,
                            CreatedBy = approval.ApprovedBy,
                            CreatedOn = DateTime.UtcNow,
                            IsDeleted = false,
                            ItemPrice = 0, // Placeholder
                            ItemCost = 0, // Placeholder
                            ItemLocationId = 0 // Placeholder
                        };
                         _context.ItemBranch.Add(newItemBranch);
                    }

                    // 3. Update Status
                    transfer.Status = "APPROVED";
                    transfer.ApprovedBy = approval.ApprovedBy;
                    transfer.ApprovalDate = DateTime.UtcNow;
                    transfer.UpdatedBy = approval.ApprovedBy;
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
            var list = _context.Set<MInventoryTransfer>()
                .Include(t => t.Item)
                .Include(t=> t.FromBranch)
                .Include(t=> t.ToBranch)
                .Where(t => t.TenantId == tenantId && !t.IsDeleted && (t.ToBranchId == branchId || t.FromBranchId == branchId)) // Show if I am sender OR receiver
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => MapToVM(t))
                .ToList();

            return new ResponseResult<List<InventoryTransferResponseVM>>(HttpStatusCode.OK, list, "Requests fetched.");
        }

        private static InventoryTransferResponseVM MapToVM(MInventoryTransfer t)
        {
            return new InventoryTransferResponseVM
            {
                Id = t.Id,
                TransferType = t.TransferType,
                Quantity = t.Quantity,
                Status = t.Status,
                ItemId = t.ItemId,
                ItemName = t.Item?.Name ?? "Unknown Item",
                FromBranchId = t.FromBranchId,
                FromBranchName = t.FromBranch?.Name ?? "External/Vendor",
                ToBranchId = t.ToBranchId,
                ToBranchName = t.ToBranch?.Name ?? "Unknown",
                CreatedOn = t.CreatedOn,
                ApprovalDate = t.ApprovalDate
            };
        }
    }
}
