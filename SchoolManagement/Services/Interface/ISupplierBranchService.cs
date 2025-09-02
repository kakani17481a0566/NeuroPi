using SchoolManagement.ViewModel.Supplier_Branch;

namespace SchoolManagement.Services.Interface
{
    public interface ISupplierBranchService
    {
        public List<SupplierBranchVM> GetAllSupplierBranches();
        public SupplierBranchVM GetSupplierBranchById(int id);
        public SupplierBranchVM CreateSupplierBranch(SupplierBranchVM supplierBranch);
        public SupplierBranchVM UpdateSupplierBranch(int id, SupplierBranchVM supplierBranch);
        public bool DeleteSupplierBranch(int id);
       
    }
}
