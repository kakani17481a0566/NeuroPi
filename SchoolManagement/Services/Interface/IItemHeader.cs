using SchoolManagement.ViewModel.ItemHeader;

namespace SchoolManagement.Services.Interface
{
    public interface IItemHeader
    {
        public List<ItemHeaderVM> GetAll();
         ItemHeaderVM GetById(int id);
         ItemHeaderVM CreateItemHeader(ItemHeaderRequestVM request);
         ItemHeaderVM UpdateItemHeader(int id, ItemHeaderRequestVM request);
         
        ItemHeaderVM DeleteItemHeader(int id);
        List<ItemHeaderVM> GetAllByTenantId(int tenantId); 
        ItemHeaderVM GetByIdAndTenantId(int id, int tenantId);


    }
}
