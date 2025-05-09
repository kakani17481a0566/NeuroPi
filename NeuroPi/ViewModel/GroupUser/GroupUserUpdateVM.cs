using System.ComponentModel.DataAnnotations;

namespace NeuroPi.ViewModel.GroupUser
{
    public class GroupUserUpdateVM
    {
        public int GroupId { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        [Required]
        public DateTime UpdatedOn { get; set; }

        [Required]
        public int UpdatedBy { get; set; }


    }
}
