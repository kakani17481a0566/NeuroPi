using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel
{
    public class RolesUpdateVM
    {
        [Required]
        public string Name { get; set; }

        public int UpdatedBy { get; set; }
    }
}
