using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.Tenent
{
    public class TenantInputVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        public int CreatedBy { get; set; }
    }
}
