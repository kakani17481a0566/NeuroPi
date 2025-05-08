using System.ComponentModel.DataAnnotations;

namespace NeuroPi.ViewModel.Tenent
{
    public class TenantInputModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        public int CreatedBy { get; set; }  // Set during creation
        public int? UpdatedBy { get; set; } // Optional, used during updates
    }
}
