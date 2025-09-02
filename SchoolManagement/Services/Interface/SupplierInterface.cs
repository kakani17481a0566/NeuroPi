using SchoolManagement.ViewModel.Supplier;

namespace SchoolManagement.Services.Interface
{
    public interface SupplierInterface
    {
        List<SupplierVM> GetAllSuppliers();
        SupplierVM GetSupplierById(int id);
        SupplierVM CreateSupplier(SupplierVM supplier);
        SupplierVM UpdateSupplier(int id, SupplierVM supplier);
        bool DeleteSupplier(int id);
    }
}
