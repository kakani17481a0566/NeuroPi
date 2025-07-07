namespace SchoolManagement.ViewModel.Transcation
{
    public class VTransactionVM
    {
        public int TrxId { get; set; }
        public string TrxDesc { get; set; }
        public DateTime? TrxDate { get; set; }
        public float? TrxAmount { get; set; }
        public string RefTrnsId { get; set; }

        // Account Info
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
        public string IFSCCode { get; set; }

        // Master Info
        public string TrxTypeName { get; set; }
        public string TrxModeName { get; set; }
        public string TrxStatusName { get; set; }
        public string AccHeadName { get; set; }

        // Audit
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
