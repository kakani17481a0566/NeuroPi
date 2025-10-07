namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageListVM
    {
        public int Id { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int PackageMasterId { get; set; }
        public string PackageName { get; set; }

        public int FeeStructureId { get; set; }
        public string FeeStructureName { get; set; }

        public decimal Amount { get; set; }

        public string PaymentPeriodName { get; set; }
    }
}
