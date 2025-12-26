namespace SchoolManagement.ViewModel.FeeTransactions
{
    public class BuildFeeTransactionsRequest
    {
       
        public int TenantId { get; set; }

       
        public int? CourseId { get; set; }

       
        public int? StudentId { get; set; }

       
        public int CreatedBy { get; set; }

        // Fee package and corporate discount selections
        public int? PackageMasterId { get; set; }  // Filter by specific package
        public int? CorporateId { get; set; }      // Apply corporate discount

        public List<int> IncludeOptionalFeeStructures { get; set; } = new();

    }
}
