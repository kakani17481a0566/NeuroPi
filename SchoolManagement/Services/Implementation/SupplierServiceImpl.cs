using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Supplier;
using SchoolManagement.ViewModel.Supplier_Branch;


namespace SchoolManagement.Services.Implementation
{
    public class SupplierServiceImpl : SupplierInterface


    {
        public readonly SchoolManagementDb _dbcontext;
        public SupplierServiceImpl(SchoolManagementDb dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public List<SupplierVM> GetAllSuppliers()
        {
            var suppliers = _dbcontext.Supplier.Where(s => !s.is_delete).ToList();
            return suppliers.Select(SupplierVM.FromModel).ToList();
        }
        public SupplierVM GetSupplierById(int id)
        {
            var suppliers = _dbcontext.Supplier.FirstOrDefault(s => s.Id == id && !s.is_delete);
            return SupplierVM.FromModel(suppliers);
        }
        public SupplierVM CreateSupplier(SupplierVM supplier)
        {
            //var model = new MSupplierBranch
            //{
            //    Id = supplier.Id,
            //    Supplier_id = supplier.Supplier_id,
            //    Branch_id = supplier.Branch_id,
            //    Tenant_id = supplier.Tenant_id,
            //    Created_On = DateTime.UtcNow,
            //    Created_by = supplier.Created_by,
            //    Updated_on = DateTime.UtcNow,
            //    Updated_by = supplier.Updated_by,
            //    is_delete = false
            //};
            //_dbcontext.Supplier.Add(model);
            //_dbcontext.SaveChanges();
            //return SupplierVM.FromModel(model);
            return null;
        }
        public SupplierVM UpdateSupplier(int id, SupplierVM supplier)
        {
            var existingSupplier = _dbcontext.Supplier.FirstOrDefault(s => s.Id == id && !s.is_delete);
            if (existingSupplier == null)
            {
                return null;
            }
            existingSupplier.Name = supplier.Name;
            existingSupplier.Contact_id = supplier.Contact_id;
            existingSupplier.Tenant_id = supplier.Tenant_id;
            existingSupplier.Updated_on = DateTime.UtcNow;
            existingSupplier.Updated_by = supplier.Updated_by;
            _dbcontext.SaveChanges();
            return SupplierVM.FromModel(existingSupplier);
        }
        public bool DeleteSupplier(int id)
        {
            var existingSupplier = _dbcontext.Supplier.FirstOrDefault(s => s.Id == id && !s.is_delete);
            if (existingSupplier == null)
            {
                return false;
            }
            existingSupplier.is_delete = true;
            _dbcontext.SaveChanges();
            return true;
        }


    }
}
