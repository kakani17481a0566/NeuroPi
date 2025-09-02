using System;

namespace SchoolManagement.ViewModel.ItemSupplier
{
    // Flat DTO for listings/grids
    public class ItemSupplierVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int BranchId { get; set; }
        public int? Adt { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
