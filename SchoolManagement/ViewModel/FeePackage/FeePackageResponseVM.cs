namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageResponseVM
    {
        public int Id { get; set; }
        public int FeeStructureId { get; set; }
        public string FeeStructureName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public int? TaxId { get; set; }
        public string PaymentPeriod { get; set; }
    }
}
