using SchoolManagement.ViewModel.Inventory;
using NeuroPi.UserManagment.Response;

namespace SchoolManagement.Services.Interface
{
    public interface IItemBatchesService
    {
        ResponseResult<ItemBatchResponseVM> CreateBatch(ItemBatchRequestVM request, int tenantId, int userId);
        ResponseResult<ItemBatchResponseVM> UpdateBatch(int id, ItemBatchRequestVM request, int tenantId, int userId);
        ResponseResult<ItemBatchResponseVM> GetBatchById(int id, int tenantId);
        ResponseResult<List<ItemBatchResponseVM>> GetBatchesByItem(int itemId, int tenantId);
        ResponseResult<List<ItemBatchResponseVM>> GetBatchesByBranch(int branchId, int tenantId);
        ResponseResult<bool> DeleteBatch(int id, int tenantId, int userId);
    }
}
