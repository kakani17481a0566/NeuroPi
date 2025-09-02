using SchoolManagement.Model;

// SchoolManagement.ViewModel.ItemSupplier/ItemSupplierRequestVM.cs
using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.ItemSupplier
{
    public class ItemSupplierRequestVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int TenantId { get; set; }
        public string Adt { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }


        public static MItemSupplier ToModel(ItemSupplierRequestVM request)
        {
            return new MItemSupplier
            {
                Id = request.Id,
                ItemId = request.ItemId,
                BranchId = request.BranchId,
                TenantId = request.TenantId,
                Adt = request.Adt,
                CreatedBy = request.CreatedBy
            };
        }
    }
}
