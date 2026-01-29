 using SchoolManagement.ViewModel.Item;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Interface
    {
        public interface ILibraryBookCopyService
        {
            List<ItemVM> GetAll();
            List<ItemVM> GetAllByTenantId(int id);

            ItemVM GetById(int id);
            ItemVM GetByIdAndTenantId(int id, int TenantId);
            ItemVM UpdateByIdAndTenantId(int Id, int TenantId, UpdateItemVM request);
            ItemVM DeleteByIdAndTenantId(int Id, int TenantId);
            ItemVM CreateLibraryBookCopy(ItemRequestVM request);
       

       
        }
    }



