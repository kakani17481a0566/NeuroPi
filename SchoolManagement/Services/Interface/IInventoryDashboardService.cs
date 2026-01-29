using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IInventoryDashboardService
    {
        ResponseResult<InventoryDashboardVM> GetBranchDashboard(int branchId, int tenantId);
    }
}
