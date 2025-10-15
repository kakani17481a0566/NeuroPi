using SchoolManagement.ViewModel.Items;

namespace SchoolManagement.Services.Interface
{
    public interface IItemsService
    {
        List<ItemsResponseVM> GetAllItems();

        //List<ItemsResponse> GetItemsByTenant(int tenantId);

        ItemsFilterResponse GetItemsByTenant(int tenantId);



        ItemsResponseVM GetItemsById(int id);

        ItemsResponseVM GetItemsByIdAndTenant(int id, int tenantId);


        ItemsResponseVM CreateItems(ItemsRequestVM itemsRequestVM);

        ItemsResponseVM UpdateItems(int id, int tenantId, ItemsUpdateVM itemsUpdateVM);

        bool DeleteItemsByIdAndTenant(int id, int tenantId);

        ItemsResponseVM CreateItemWithGroup(ItemInsertVM itemInsertVM);

        ItemWithGroupResponseVM GetItemWithGroup(int id, int tenantId);






    }
}
