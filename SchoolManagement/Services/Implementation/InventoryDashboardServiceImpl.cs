using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;
using System.Net;

namespace SchoolManagement.Services.Implementation
{
    public class InventoryDashboardServiceImpl : IInventoryDashboardService
    {
        private readonly SchoolManagementDb _context;
        private readonly IStockTransactionLogService _transactionLogService;

        public InventoryDashboardServiceImpl(SchoolManagementDb context, IStockTransactionLogService transactionLogService)
        {
            _context = context;
            _transactionLogService = transactionLogService;
        }

        public ResponseResult<InventoryDashboardVM> GetBranchDashboard(int branchId, int tenantId)
        {
            try
            {
                var branchItems = _context.ItemBranch
                    .Where(ib => ib.BranchId == branchId && ib.TenantId == tenantId && !ib.IsDeleted)
                    .ToList();

                var dashboard = new InventoryDashboardVM
                {
                    TotalStockValue = branchItems.Sum(ib => ib.ItemQuantity * (ib.AverageCost ?? ib.ItemCost)),
                    LowStockItemsCount = branchItems.Count(ib => ib.ItemQuantity > 0 && ib.ItemQuantity <= ib.ItemReOrderLevel),
                    OutOfStockItemsCount = branchItems.Count(ib => ib.ItemQuantity <= 0),
                    PendingTransfersCount = _context.InventoryTransfers
                        .Count(t => t.ToBranchId == branchId && t.Status == "PENDING" && t.TenantId == tenantId && !t.IsDeleted)
                };

                // Fetch recent transactions using the existing service
                var recentLogs = _transactionLogService.GetTransactionHistory(0, branchId, tenantId);
                if (recentLogs.StatusCode == HttpStatusCode.OK && recentLogs.Data != null)
                {
                    dashboard.RecentTransactions = recentLogs.Data.Take(10).ToList();
                }
                else
                {
                    dashboard.RecentTransactions = new List<StockTransactionLogResponseVM>();
                }

                return new ResponseResult<InventoryDashboardVM>(HttpStatusCode.OK, dashboard);
            }
            catch (Exception ex)
            {
                return new ResponseResult<InventoryDashboardVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
