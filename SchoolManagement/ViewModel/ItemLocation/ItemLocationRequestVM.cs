namespace SchoolManagement.ViewModel.ItemLocation
{
    public class ItemLocationRequestVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BranchId { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public int? updatedBy { get; set; }
    }
}
