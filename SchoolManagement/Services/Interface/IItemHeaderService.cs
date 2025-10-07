using SchoolManagement.ViewModel.ItemHeader;

namespace SchoolManagement.Services.Interface
{
    public interface IItemHeaderService
    {
        public List<ItemHeaderResponseVM> GetAll();
         ItemHeaderVM GetById(int id);
         ItemHeaderVM CreateItemHeader(ItemHeaderRequestVM request);
         ItemHeaderVM UpdateItemHeader(int id, ItemHeaderRequestVM request);
         
        ItemHeaderVM DeleteItemHeader(int id);
        List<ItemHeaderResponseVM> GetAllByTenantId(int tenantId); 
        ItemHeaderVM GetByIdAndTenantId(int id, int tenantId);


    }
}
