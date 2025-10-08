namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class FeeReportTransactionVM
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int FeeStructureId { get; set; }
        public string FeeStructureName { get; set; } = string.Empty; // joined from fee_structure
        public int StudentId { get; set; }
        public DateTime TrxDate { get; set; }
        public string TrxMonth { get; set; } = string.Empty; // e.g. "Sep"
        public string TrxYear { get; set; } = string.Empty;  // e.g. "2025"
        public string TrxType { get; set; } = string.Empty;  // "debit" or "credit"
        public string TrxName { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string TrxStatus { get; set; } = string.Empty; // Pending, Completed, etc.
        public string PaymentType { get; set; } = string.Empty; // e.g. "Monthly", "Annual"
    }

    public class FeeReportSummaryVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public decimal TotalFee { get; set; }       // Sum of Debit
        public decimal TotalPaid { get; set; }      // Sum of Credit
        public decimal PendingFee { get; set; }     // TotalFee - TotalPaid
        public List<FeeReportTransactionVM> Transactions { get; set; } = new();
    }
}
