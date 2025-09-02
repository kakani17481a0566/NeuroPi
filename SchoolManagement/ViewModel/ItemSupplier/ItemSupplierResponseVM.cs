// SchoolManagement.ViewModel.ItemSupplier/ItemSupplierResponseVM.cs
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemSupplier
{
    public class ItemSupplierResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }
        public string Adt { get; set; }

        // STATIC mapper: Entity -> Response
        public static ItemSupplierResponseVM FromModel(MItemSupplier m)
        {
            if (m == null) return null;

            return new ItemSupplierResponseVM
            {
                Id = m.Id,
                ItemId = m.ItemId,
                BranchId = m.BranchId,
                TenantId = m.TenantId,
                Adt = m.Adt
            };
        }
    }
}
