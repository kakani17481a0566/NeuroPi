namespace SchoolManagement.ViewModel.Transcation
{
    public class TransactionDetailVM
    {
        public int TrxId { get; set; }
        public string TrxDesc { get; set; }
        public string RefTrnsId { get; set; }
        public int? TrxTypeId { get; set; }
        public string TrxTypeName { get; set; }
        public int? TrxModeId { get; set; }
        public string TrxModeName { get; set; }
        public int? TrxStatus { get; set; }
        public string TrxStatusName { get; set; }
        public int? AccHeadId { get; set; }
        public string AccHeadName { get; set; }
        public decimal? TrxAmount { get; set; }
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public int? TenantId { get; set; }
        public string TenantName { get; set; }
    }

}
