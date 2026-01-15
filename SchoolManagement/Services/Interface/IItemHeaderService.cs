using SchoolManagement.ViewModel.ItemHeader;

namespace SchoolManagement.Services.Interface
{
    public interface IItemHeaderService
    {
        List<ItemHeaderResponseVM> GetAll();
        ItemHeaderVM GetById(int id);
        ItemHeaderResponseVM CreateItemHeader(ItemHeaderRequestVM request); // Changed return to ResponseVM
        ItemHeaderResponseVM UpdateItemHeader(int id, ItemHeaderRequestVM request); // Changed return to ResponseVM
        bool DeleteItemHeader(int id); // Changed return to bool
        List<ItemHeaderResponseVM> GetAllByTenantId(int tenantId); 
        ItemHeaderVM GetByIdAndTenantId(int id, int tenantId);
    }
}
