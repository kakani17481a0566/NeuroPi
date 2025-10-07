namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class FeeTransactionResultVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public DateTime TrxDate { get; set; }
        public string TrxMonth => TrxDate.ToString("MMM");
        public string TrxYear => TrxDate.Year.ToString();
        public string TrxName { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string PaymentType { get; set; } = string.Empty;  // Annual, Monthly, Term, Onetime
    }
}
