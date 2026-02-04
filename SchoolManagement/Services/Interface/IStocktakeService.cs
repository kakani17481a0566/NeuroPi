using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IStocktakeService
    {
        // CRUD Header
        ResponseResult<StocktakeHeaderResponseVM> CreateStocktake(StocktakeHeaderRequestVM request, int tenantId, int userId);
        ResponseResult<StocktakeHeaderResponseVM> GetStocktakeById(int id, int tenantId);
        ResponseResult<List<StocktakeHeaderResponseVM>> GetAllStocktakes(int tenantId, int? branchId = null, string? status = null);
        ResponseResult<StocktakeHeaderResponseVM> UpdateStocktake(int id, StocktakeHeaderRequestVM request, int tenantId, int userId);
        ResponseResult<bool> DeleteStocktake(int id, int tenantId, int userId);

        // Lines
        ResponseResult<StocktakeLineResponseVM> AddStocktakeLine(StocktakeLineRequestVM request, int tenantId, int userId);
        ResponseResult<List<StocktakeLineResponseVM>> GetStocktakeLines(int headerId, int tenantId);

        // Workflow
        ResponseResult<bool> CompleteStocktake(int id, int tenantId, int userId);
        ResponseResult<bool> ApproveStocktake(ApproveStocktakeRequestVM request, int tenantId);
        ResponseResult<bool> PostStocktakeToInventory(int id, int tenantId, int userId);

        // Reporting
        ResponseResult<List<StocktakeLineResponseVM>> GetVarianceReport(int headerId, int tenantId);
        ResponseResult<StocktakeSummaryVM> GetStocktakeSummary(int headerId, int tenantId);
    }
}
