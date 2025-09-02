using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Supplier
{
    public class SupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Contact_id { get; set; }
        public int Tenant_id { get; set; }
        public DateTime Created_On { get; set; }
        public int Created_by { get; set; }
        public DateTime Updated_on { get; set; }
        public int Updated_by { get; set; }
        public bool is_delete { get; set; }

        public static SupplierVM FromModel(MSupplier s)
        {
            if (s == null) return null;
            return new SupplierVM
            {
                Id = s.Id,
                Name = s.Name,
                Contact_id = s.Contact_id,
                Tenant_id = s.Tenant_id,
                Created_On = s.Created_On,
                Created_by = s.Created_by,
                Updated_on = s.Updated_on,
                Updated_by = s.Updated_by,
                is_delete = s.is_delete
            };
        }


    }
}