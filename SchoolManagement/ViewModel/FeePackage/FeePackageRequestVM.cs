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
        [MaxLength(50)]
        public string PaymentPeriod { get; set; }

        // 🔹 Add these if you want to pass them explicitly from client
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
