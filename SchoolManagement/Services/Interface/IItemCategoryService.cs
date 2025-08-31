using SchoolManagement.ViewModel.ItemCategory;

namespace SchoolManagement.Services.Interface
{
    public interface IItemCategoryService
    {
        List<ItemCategoryResponseVM> GetAllItemsCategory();

        ItemCategoryResponseVM GetItemCategoryById(int id);

        List<ItemCategoryResponseVM> GetItemCategoryByTenantId(int tenantId);

        ItemCategoryResponseVM GetItemCategoryByTenantIdAndId(int tenantId, int id);

        ItemCategoryResponseVM CreateItemCategory(ItemCategoryRequestVM itemCategoryRequestVM);

        ItemCategoryResponseVM UpdateItemCategory(int id, int tenantId,ItemCategoryUpdateVM itemCategoryUpdateVM);

        ItemCategoryResponseVM DeleteItemCategory(int id, int tenantId);



    }
}
