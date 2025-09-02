using Microsoft.VisualBasic;
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Supplier_Branch
{
    public class SupplierBranchVM
    {
        public int Id { get; set; }
        public int Supplier_id { get; set; }
        public int Branch_id { get; set; }
        public int Tenant_id { get; set; }
        public int Created_by { get; set; }
        public int Updated_by { get; set; }
        public DateTime Created_on { get; set; }
        public DateTime Updated_on { get; set; }

        public bool is_delete { get; set; }

        public static SupplierBranchVM FromModel(MSupplierBranch sb)
        {
            if (sb == null) return null;
            return new SupplierBranchVM
            {
                Id = sb.Id,
                Supplier_id = sb.Supplier_id,
                Branch_id = sb.Branch_id,
                Tenant_id = sb.Tenant_id,
                Created_by = sb.Created_by,
                Updated_by = sb.Updated_by,
                Created_on = sb.Created_On,
                Updated_on = sb.Updated_on,
                is_delete = sb.is_delete
            };
        }

    }
}
