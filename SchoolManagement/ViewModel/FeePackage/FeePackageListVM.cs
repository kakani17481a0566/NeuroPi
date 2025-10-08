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

        // Payment period (e.g. Monthly, Annual, Onetime)
        public int PaymentPeriodId { get; set; }
        public string PaymentPeriodName { get; set; }

        // 🔹 Include fee type for quick list filtering/grouping
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
    }
}
