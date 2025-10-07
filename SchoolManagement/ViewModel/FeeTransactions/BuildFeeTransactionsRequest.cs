namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class BuildFeeTransactionsRequest
    {
       
        public int TenantId { get; set; }

       
        public int? CourseId { get; set; }

       
        public int? StudentId { get; set; }

       
        public int CreatedBy { get; set; }
    }
}
