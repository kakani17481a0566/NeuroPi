using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemSupplier
{
    public class ItemSupplierVM
    {
        public int Id { get; set; }
        public int item_id { get; set; }
        public int branch_id { get; set; }
        public int adt { get; set; }
        public int tenant_id { get; set; }
        public int Created_by { get; set; }

        public int Updated_by { get; set; }
        public DateTime Created_on { get; set; }
        public DateTime Updated_on { get; set; }
        public bool is_delete { get; set; }

    }
}
