using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageRequestVM
    {
        [Required]
        public int FeeStructureId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int TenantId { get; set; }

        public int? TaxId { get; set; }

        [Required]
        public int PaymentPeriod { get; set; }   // 🔹 changed to int (matches DB + Entity)

        public int? PackageMasterId { get; set; }  // 🔹 newly added
    }
}
