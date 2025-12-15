namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class AddPaymentRequest
    {
        public int TenantId { get; set; }
        public int StudentId { get; set; }
        public int FeeStructureId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TrxDate { get; set; }
        public string PaymentMode { get; set; } = string.Empty; // Cash, UPI, Card, Cheque, Bank Transfer
        public string Remarks { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
    }
}
