using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.FeePackage
{
    public class FeePackageMasterRequestVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public int TenantId { get; set; }

        [Required]
        public int CourseId { get; set; }
    }
}
