using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.User
{
    public class UserResponseVM
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int TenantId { get; set; } // Keep as int to match MUser
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; } // Keep as int? to match MBaseModel

        public int? UpdatedBy { get; set; } // Keep as int? to match MBaseModel

        public static UserResponseVM ToViewModel(MUser user)
        {
            return new UserResponseVM
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                TenantId = user.TenantId,
                CreatedOn = user.CreatedOn,
                CreatedBy = user.CreatedBy,
               
                UpdatedBy = user.UpdatedBy
            };
        }

        public static List<UserResponseVM> ToViewModelList(List<MUser> users)
        {
            return users.Select(user => ToViewModel(user)).ToList();
        }
    }
}