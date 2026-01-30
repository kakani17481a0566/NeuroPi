using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class StockTransactionLogServiceImpl : IStockTransactionLogService
    {
        private readonly SchoolManagementDb _context;

        public StockTransactionLogServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<StockTransactionLogResponseVM> LogTransaction(StockTransactionLogRequestVM request, int tenantId, int userId)
        {
            // Transaction is managed by the caller (e.g. InventoryTransferService) logic if needed.
            try
            {
                // 1. Get current stock for audit
                var branchItem = _context.ItemBranch
                    .FirstOrDefault(ib => ib.ItemId == request.ItemId && ib.BranchId == request.BranchId && ib.TenantId == tenantId);

                int qtyBefore = branchItem?.ItemQuantity ?? 0;

                // 2. Create Log Entry
                var log = new MStockTransactionLog
                {
                    ItemId = request.ItemId,
                    BranchId = request.BranchId,
                    TransactionDate = DateTime.UtcNow,
                    TransactionType = request.TransactionType.ToUpper(),
                    QuantityChange = request.QuantityChange,
                    QuantityBefore = qtyBefore,
                    QuantityAfter = qtyBefore + request.QuantityChange,
                    ReferenceType = request.ReferenceType,
                    ReferenceId = request.ReferenceId,
                    AdjustmentReasonId = request.AdjustmentReasonId,
                    UnitCost = request.UnitCost,
                    BatchId = request.BatchId,
                    UomCode = request.UomCode ?? "EA",
                    SerialNumberId = request.SerialNumberId,
                    TenantId = tenantId,
                    CreatedBy = userId == 0 ? null : userId
                };

                _context.StockTransactionLogs.Add(log);

                // 3. Update Branch Stock Cache
                if (branchItem != null)
                {
                    branchItem.ItemQuantity = log.QuantityAfter ?? 0;
                    branchItem.LastMovementDate = log.TransactionDate;
                    branchItem.UpdatedBy = userId == 0 ? null : userId;
                    branchItem.UpdatedOn = DateTime.UtcNow;
                    branchItem.IsDeleted = false; // Reactivate if it was deleted
                }
                else
                {
                    // Create branch item if it doesn't exist
                    _context.ItemBranch.Add(new MItemBranch
                    {
                        ItemId = request.ItemId,
                        BranchId = request.BranchId,
                        ItemQuantity = log.QuantityAfter ?? 0,
                        LastMovementDate = log.TransactionDate,
                        TenantId = tenantId,
                        CreatedBy = userId, // MItemBranch uses MBaseModel.CreatedBy (int), cannot set null.
                        CreatedOn = DateTime.UtcNow,
                        ItemPrice = 0,
                        ItemCost = 0,
                        ItemLocationId = null
                    });
                }

                _context.SaveChanges();
                
                var data = new StockTransactionLogResponseVM
                {
                    Id = log.Id,
                    ItemId = log.ItemId,
                    ItemName = _context.Items.Find(log.ItemId)?.Name,
                    BranchId = log.BranchId,
                    BranchName = _context.Branches.Find(log.BranchId)?.Name,
                    TransactionDate = log.TransactionDate,
                    TransactionType = log.TransactionType,
                    QuantityChange = log.QuantityChange,
                    QuantityBefore = log.QuantityBefore,
                    QuantityAfter = log.QuantityAfter,
                    UnitCost = log.UnitCost,
                    ReferenceType = log.ReferenceType,
                    ReferenceId = log.ReferenceId,
                    UomCode = log.UomCode
                };

                return new ResponseResult<StockTransactionLogResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                // Let the caller handle rollback if necessary, or just return error
                return new ResponseResult<StockTransactionLogResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StockTransactionLogResponseVM>> GetTransactionHistory(int itemId, int? branchId, int tenantId, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                var query = _context.StockTransactionLogs
                    .Include(l => l.Item)
                    .Include(l => l.Branch)
                    .Where(l => l.TenantId == tenantId);

                if (itemId > 0)
                    query = query.Where(l => l.ItemId == itemId);

                if (branchId.HasValue)
                    query = query.Where(l => l.BranchId == branchId);

                if (fromDate.HasValue)
                    query = query.Where(l => l.TransactionDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(l => l.TransactionDate <= toDate.Value);

                var data = query.OrderByDescending(l => l.TransactionDate)
                    .Select(l => new StockTransactionLogResponseVM
                    {
                        Id = l.Id,
                        ItemId = l.ItemId,
                        ItemName = l.Item.Name,
                        BranchId = l.BranchId,
                        BranchName = l.Branch.Name,
                        TransactionDate = l.TransactionDate,
                        TransactionType = l.TransactionType,
                        QuantityChange = l.QuantityChange,
                        QuantityBefore = l.QuantityBefore,
                        QuantityAfter = l.QuantityAfter,
                        UnitCost = l.UnitCost,
                        ReferenceType = l.ReferenceType,
                        ReferenceId = l.ReferenceId,
                        UomCode = l.UomCode
                    }).ToList();

                return new ResponseResult<List<StockTransactionLogResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<StockTransactionLogResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<StockTransactionLogResponseVM>> GetTransactionsByType(string transactionType, int tenantId, int? branchId = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                var query = _context.StockTransactionLogs
                    .Include(l => l.Item)
                    .Include(l => l.Branch)
                    .Where(l => l.TransactionType == transactionType.ToUpper() && l.TenantId == tenantId);

                if (branchId.HasValue)
                    query = query.Where(l => l.BranchId == branchId);

                if (fromDate.HasValue)
                    query = query.Where(l => l.TransactionDate >= fromDate.Value);

                if (toDate.HasValue)
                    query = query.Where(l => l.TransactionDate <= toDate.Value);

                var data = query.OrderByDescending(l => l.TransactionDate)
                    .Select(l => new StockTransactionLogResponseVM
                    {
                        Id = l.Id,
                        ItemId = l.ItemId,
                        ItemName = l.Item.Name,
                        BranchId = l.BranchId,
                        BranchName = l.Branch.Name,
                        TransactionDate = l.TransactionDate,
                        TransactionType = l.TransactionType,
                        QuantityChange = l.QuantityChange,
                        QuantityBefore = l.QuantityBefore,
                        QuantityAfter = l.QuantityAfter,
                        UnitCost = l.UnitCost,
                        ReferenceType = l.ReferenceType,
                        ReferenceId = l.ReferenceId,
                        UomCode = l.UomCode
                    }).ToList();

                return new ResponseResult<List<StockTransactionLogResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<StockTransactionLogResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<int> GetCurrentStock(int itemId, int branchId, int tenantId)
        {
            try
            {
                var stock = _context.ItemBranch
                    .Where(ib => ib.ItemId == itemId && ib.BranchId == branchId && ib.TenantId == tenantId)
                    .Select(ib => ib.ItemQuantity)
                    .FirstOrDefault();

                return new ResponseResult<int>(HttpStatusCode.OK, stock);
            }
            catch (Exception ex)
            {
                return new ResponseResult<int>(HttpStatusCode.InternalServerError, 0, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<StockMovementSummaryVM> GetStockMovementSummary(int itemId, int branchId, int tenantId, DateTime fromDate, DateTime toDate)
        {
            try
            {
                var logs = _context.StockTransactionLogs
                    .Where(l => l.ItemId == itemId && l.BranchId == branchId && l.TenantId == tenantId && 
                                l.TransactionDate >= fromDate && l.TransactionDate <= toDate)
                    .ToList();

                var item = _context.Items.Find(itemId);

                var summary = new StockMovementSummaryVM
                {
                    ItemId = itemId,
                    ItemName = item?.Name,
                    BranchId = branchId,
                    PeriodFrom = fromDate,
                    PeriodTo = toDate,
                    OpeningStock = _context.StockTransactionLogs
                        .Where(l => l.ItemId == itemId && l.BranchId == branchId && l.TenantId == tenantId && l.TransactionDate < fromDate)
                        .OrderByDescending(l => l.TransactionDate)
                        .Select(l => l.QuantityAfter)
                        .FirstOrDefault() ?? 0,
                    TotalIn = logs.Where(l => l.QuantityChange > 0).Sum(l => l.QuantityChange),
                    TotalOut = Math.Abs(logs.Where(l => l.QuantityChange < 0).Sum(l => l.QuantityChange)),
                    Adjustments = logs.Where(l => l.TransactionType == "ADJUSTMENT").Sum(l => l.QuantityChange),
                    ClosingStock = _context.ItemBranch
                        .Where(ib => ib.ItemId == itemId && ib.BranchId == branchId && ib.TenantId == tenantId)
                        .Select(ib => ib.ItemQuantity)
                        .FirstOrDefault()
                };

                return new ResponseResult<StockMovementSummaryVM>(HttpStatusCode.OK, summary);
            }
            catch (Exception ex)
            {
                return new ResponseResult<StockMovementSummaryVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
