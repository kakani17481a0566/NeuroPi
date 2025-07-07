namespace SchoolManagement.ViewModel.Transaction
{
    public class TransactionRequestVM
    {
        public int DebitAccId { get; set; }
        public int CreditAccId { get; set; }
        public double Amount { get; set; }
        public int TrxModeId { get; set; }
        public int TrxStatus { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public string TrxDesc { get; set; } = string.Empty;
        public int? DebitAccHeadId { get; set; } = null;
        public int? CreditAccHeadId { get; set; } = null;

        // ✅ Optional transaction date (date only, no time)
        public DateTime? TrxDate { get; set; }
    }
}
