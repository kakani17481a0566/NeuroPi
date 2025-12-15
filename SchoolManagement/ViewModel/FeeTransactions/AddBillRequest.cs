namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class AddBillRequest
    {
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int? FeeStructureId { get; set; }
        public string FeeStructureName { get; set; } = string.Empty; // Tuition Fee, Exam Fee, etc.
        public decimal Amount { get; set; }
        public DateTime TrxDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
    }
}
