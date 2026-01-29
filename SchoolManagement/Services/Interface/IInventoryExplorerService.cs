using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Interface
{
    public interface IInventoryExplorerService
    {
        ResponseResult<List<InventoryItemVM>> GetInventoryList(int tenantId, int? branchId = null, int? categoryId = null, string? searchTerm = null);
        ResponseResult<InventoryItemVM> GetInventoryDetail(int itemId, int branchId, int tenantId);
    }
}
