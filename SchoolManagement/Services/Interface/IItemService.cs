 using SchoolManagement.ViewModel.Item;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Interface
    {
        public interface IItemService
        {
            List<ItemVM> GetAll();
            List<ItemVM> GetAllByTenantId(int id);

            ItemVM GetById(int id);
            ItemVM GetByIdAndTenantId(int id, int TenantId);
            ItemVM UpdateByIdAndTenantId(int Id, int TenantId, UpdateItemVM request);
            ItemVM DeleteByIdAndTenantId(int Id, int TenantId);
        ItemVM CreateItem(ItemRequestVM request);
       

       
        }
    }



