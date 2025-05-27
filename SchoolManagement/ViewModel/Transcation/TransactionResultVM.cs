namespace SchoolManagement.ViewModel.Transcation
{
    public class TransactionResultVM
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public int DebitTrxId { get; set; }
        public int CreditTrxId { get; set; }
        public string RefTrnsId { get; set; }
    }
}
