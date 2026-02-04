using SchoolManagement.ViewModel.ItemBranch;

namespace SchoolManagement.Services.Interface
{
    public interface IItemBranchService
    {
        List<ItemBranchResponseVM> GetAllItemBranches();

        List<ItemBranchResponseVM> GetItemBranchesByTenant(int tenantId);

        ItemBranchResponseVM GetItemBranchById(int id);

        ItemBranchResponseVM GetItemBranchByIdAndTenant(int id, int tenantId);

        ItemBranchResponseVM CreateItemBranch(ItemBranchRequestVM itemBranchRequestVM);

        ItemBranchResponseVM UpdateItemBranch(int id, int tenantId, ItemBranchUpdateVM itemBranchUpdateVM);

        bool DeleteItemBranchByIdAndTenant(int id, int tenantId);
        List<ItemBranchResponseVM> GetItemsByBranchId(int branchId);
        List<ItemBranchResponseVM> GetBranchStockForItem(int itemId, int tenantId);
    }
}
