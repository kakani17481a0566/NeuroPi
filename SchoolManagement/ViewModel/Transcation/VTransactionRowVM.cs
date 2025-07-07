namespace SchoolManagement.ViewModel.Transcation
{
    public class VTransactionRowVM
    {
        public int TrxId { get; set; }
        public string RefTrnsId { get; set; }
        public string TrxDesc { get; set; }
        public DateTime? TrxDate { get; set; }
        public double TrxAmount { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string TrxTypeName { get; set; }
        public string TrxModeName { get; set; }
        public string TrxStatusName { get; set; }
        public string AccHeadName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

}
