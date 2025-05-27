using System;

namespace SchoolManagement.ViewModel.Transaction
{
    public class TransactionResponseVM
    {
        public int TrxId { get; set; }
        public int AccId { get; set; }
        public double TrxAmount { get; set; }
        public string TrxDesc { get; set; }
        public string RefTrnsId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int TenantId { get; set; }
        public int TrxTypeId { get; set; }
        public int TrxModeId { get; set; }
        public int TrxStatus { get; set; }
        public int? AccHeadId { get; set; }
    }
}
