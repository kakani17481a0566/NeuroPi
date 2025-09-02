using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Supplier_Branch;

namespace SchoolManagement.Services.Implementation
{
    public class SupplierBranchImpl : ISupplierBranchService

    {
        public readonly SchoolManagementDb _dbcontext;
        public SupplierBranchImpl(SchoolManagementDb dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<SupplierBranchVM> GetAllSupplierBranches()
        {
            var supplierBranches = _dbcontext.SupplierBranches.Where(sb => !sb.is_delete).ToList();
            return supplierBranches.Select(SupplierBranchVM.FromModel).ToList();
        }

        public SupplierBranchVM GetSupplierBranchById(int id)
        {
            var supplierBranch = _dbcontext.SupplierBranches.FirstOrDefault(sb => sb.Id == id && !sb.is_delete);
            return SupplierBranchVM.FromModel(supplierBranch);

        }



        public SupplierBranchVM CreateSupplierBranch(SupplierBranchVM supplierBranch)
        {
            var model = new MSupplierBranch
            {
                Supplier_id = supplierBranch.Supplier_id,
                Branch_id = supplierBranch.Branch_id,
                Tenant_id = supplierBranch.Tenant_id,
                Created_by = supplierBranch.Created_by,
                Updated_by = supplierBranch.Updated_by,
                Created_On = DateTime.UtcNow,
                Updated_on = DateTime.UtcNow,
                is_delete = false
            };
            _dbcontext.SupplierBranches.Add(model);
            _dbcontext.SaveChanges();
            return SupplierBranchVM.FromModel(model);
        }
        public SupplierBranchVM UpdateSupplierBranch(int id, SupplierBranchVM supplierBranch)
        {
            var existingSupplierBranch = _dbcontext.SupplierBranches.FirstOrDefault(sb => sb.Id == id && !sb.is_delete);
            if (existingSupplierBranch == null)
            {
                return null;
            }
            existingSupplierBranch.Supplier_id = supplierBranch.Supplier_id;
            existingSupplierBranch.Branch_id = supplierBranch.Branch_id;
            existingSupplierBranch.Tenant_id = supplierBranch.Tenant_id;
            existingSupplierBranch.Updated_on = DateTime.UtcNow;
            existingSupplierBranch.Updated_by = supplierBranch.Updated_by;
            _dbcontext.SaveChanges();
            return SupplierBranchVM.FromModel(existingSupplierBranch);
        }
        public bool DeleteSupplierBranch(int id)
        {
            var existingSupplierBranch = _dbcontext.SupplierBranches.FirstOrDefault(sb => sb.Id == id && !sb.is_delete);
            if (existingSupplierBranch == null)
            {
                return false;
            }
            existingSupplierBranch.is_delete = true;
            _dbcontext.SaveChanges();
            return true;
        }

        
    }
}
