using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.Tenent
{
    public class TenantUpdateInputVM
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "UpdatedBy is required")]
        public int UpdatedBy { get; set; }
    }
}
