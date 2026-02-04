using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IStockAdjustmentReasonService
    {
        ResponseResult<StockAdjustmentReasonResponseVM> CreateReason(StockAdjustmentReasonRequestVM request, int tenantId, int userId);
        ResponseResult<StockAdjustmentReasonResponseVM> GetReasonById(int id, int tenantId);
        ResponseResult<List<StockAdjustmentReasonResponseVM>> GetAllReasons(int tenantId, string? adjustmentType = null);
        ResponseResult<StockAdjustmentReasonResponseVM> UpdateReason(int id, StockAdjustmentReasonRequestVM request, int tenantId, int userId);
        ResponseResult<bool> DeleteReason(int id, int tenantId, int userId);
    }
}
