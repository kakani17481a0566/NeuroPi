namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class StudentPendingFeeVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
        public decimal TotalFee { get; set; }
        public decimal PendingAmount { get; set; }
        public double PendingPercentage { get; set; }
    }
}
