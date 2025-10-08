using System.Collections.Generic;

namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageGroupVM
    {
        public int PackageMasterId { get; set; }
        public string PackageName { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public int TenantId { get; set; }
        public string TenantName { get; set; }

        // All fee structures under this package
        public List<FeePackageItemVM> Items { get; set; } = new();
    }

    public class FeePackageItemVM
    {
        public int Id { get; set; }

        public int FeeStructureId { get; set; }
        public string FeeStructureName { get; set; }

        public decimal Amount { get; set; }

        // Payment Period (e.g. Monthly, Annual, Term, Onetime)
        public int PaymentPeriodId { get; set; }
        public string PaymentPeriodName { get; set; }

        // 🔹 Include FeeType as well
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
    }
}
