using SchoolManagement.ViewModel.ItemSupplier;

namespace SchoolManagement.Services.Interface
{
    public interface IItemSupplierService
    {
        List<ItemSupplierResponseVM> GetAll();
        List<ItemSupplierResponseVM> GetAllByTenantId(int tenantId);
        ItemSupplierResponseVM GetById(int id);
        ItemSupplierResponseVM? GetByIdAndTenantId(int id, int tenantId);

        ItemSupplierResponseVM CreateItemSupplier(ItemSupplierRequestVM request);
        ItemSupplierResponseVM? UpdateItemSupplier(int id, int tenantId, ItemSupplierRequestVM request);
        ItemSupplierResponseVM? DeleteByIdAndTenantId(int id, int tenantId);
    }
}
