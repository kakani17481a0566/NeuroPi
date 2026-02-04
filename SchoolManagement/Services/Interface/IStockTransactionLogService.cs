using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IStockTransactionLogService
    {
        ResponseResult<StockTransactionLogResponseVM> LogTransaction(StockTransactionLogRequestVM request, int tenantId, int userId);
        ResponseResult<List<StockTransactionLogResponseVM>> GetTransactionHistory(int itemId, int? branchId, int tenantId, DateTime? fromDate = null, DateTime? toDate = null);
        ResponseResult<List<StockTransactionLogResponseVM>> GetTransactionsByType(string transactionType, int tenantId, int? branchId = null, DateTime? fromDate = null, DateTime? toDate = null);
        ResponseResult<int> GetCurrentStock(int itemId, int branchId, int tenantId);
        ResponseResult<StockMovementSummaryVM> GetStockMovementSummary(int itemId, int branchId, int tenantId, DateTime fromDate, DateTime toDate);
    }
}
