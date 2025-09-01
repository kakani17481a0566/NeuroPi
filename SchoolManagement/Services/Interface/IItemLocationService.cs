using SchoolManagement.ViewModel.ItemLocation;

namespace SchoolManagement.Services.Interface
{
    public interface IItemLocationService
    {
        List<ItemLocationResponseVM> GetItemLocations();

        List<ItemLocationResponseVM> ItemLocationByTenantId(int tenantId);

        ItemLocationResponseVM GetItemLocationById(int branchId);

        List<ItemLocationResponseVM> GetItemLocationById(int branchId, int tenantId);

        ItemLocationResponseVM CreateItemLocation(ItemLocationRequestVM itemLocationRequestVM);

        ItemLocationResponseVM UpdateItemLocation(int id, int tenantId, ItemLocationUpdateVM itemLocationUpdateVM);

        bool DeleteItemLocation(int id, int tenantId);





    }
}
