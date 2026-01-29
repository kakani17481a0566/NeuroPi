using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface ISupplierPerformanceService
    {
        ResponseResult<SupplierPerformanceResponseVM> RecordPerformance(SupplierPerformanceRequestVM request, int tenantId, int userId);
        ResponseResult<List<SupplierPerformanceResponseVM>> GetSupplierPerformanceHistory(int supplierId, int tenantId);
        ResponseResult<SupplierPerformanceSummaryVM> GetSupplierPerformanceSummary(int supplierId, int tenantId);
        ResponseResult<List<SupplierPerformanceSummaryVM>> GetAllSuppliersPerformanceSummary(int tenantId);
    }
}
