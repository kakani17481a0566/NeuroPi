using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface ICostHistoryService
    {
        ResponseResult<CostHistoryResponseVM> RecordCost(CostHistoryRequestVM request, int tenantId, int userId);
        ResponseResult<decimal> GetCurrentCost(int itemId, int? branchId, string costType, int tenantId);
        ResponseResult<List<CostHistoryResponseVM>> GetCostHistory(int itemId, int? branchId, int tenantId, DateTime? fromDate = null, DateTime? toDate = null);
        ResponseResult<List<CostTrendVM>> GetCostTrends(int tenantId, int? branchId = null, DateTime? fromDate = null, DateTime? toDate = null);
    }
}
