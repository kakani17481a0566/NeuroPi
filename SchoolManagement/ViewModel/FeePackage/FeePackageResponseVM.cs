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

        // PaymentPeriod: show both ID + friendly name
        public int PaymentPeriodId { get; set; }
        public string PaymentPeriodName { get; set; }

        // Optional: Package Master (if assigned)
        public int? PackageMasterId { get; set; }
        public string PackageMasterName { get; set; }

        // 🔹 Fee Type (new column)
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
    }
}
