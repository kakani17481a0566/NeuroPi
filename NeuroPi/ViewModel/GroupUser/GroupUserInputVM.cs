using System.ComponentModel.DataAnnotations;

namespace NeuroPi.UserManagment.ViewModel.GroupUser
{
    public class GroupUserInputVM
    {

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int CreatedBy { get; set; }

    }
}
