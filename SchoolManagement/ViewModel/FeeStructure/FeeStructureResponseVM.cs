namespace SchoolManagement.ViewModel.FeeStructure
{
    public class FeeStructureResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }

        // Tenant info
        public int TenantId { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
