namespace SchoolManagement.ViewModel.ItemLocation
{
    public class ItemLocationUpdateVM
    {
        public string Name { get; set; }

        public int BranchId { get; set; }

        public int TenantId { get; set; }

        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
