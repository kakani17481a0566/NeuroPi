using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.FeeStructure
{
    public class FeeStructureRequestVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int TenantId { get; set; }

        // Audit fields
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
