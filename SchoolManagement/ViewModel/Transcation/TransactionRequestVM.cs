namespace SchoolManagement.ViewModel.Transaction
{
    public class TransactionRequestVM
    {
        public int DebitAccId { get; set; }
        public int CreditAccId { get; set; }
        public double Amount { get; set; }
        public int TrxTypeId { get; set; }
        public int TrxModeId { get; set; }
        public int TrxStatus { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

    }

}
