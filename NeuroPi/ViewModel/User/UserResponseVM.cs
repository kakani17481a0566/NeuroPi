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

        public string? MobileNumber { get; set; }

        public string? AlternateNumber { get; set; }

        public string? Address { get; set; }


        public string? MiddleName { get; set; }
        public int TenantId { get; set; } // Keep as int to match MUser
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; } // Keep as int? to match MBaseModel

        public int? UpdatedBy { get; set; } // Keep as int? to match MBaseModel
        public string? UserImageUrl { get; set; }





        public static UserResponseVM ToViewModel(MUser user)
        {
            return new UserResponseVM
            {
                UserId = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                MobileNumber = user.MobileNumber,
                AlternateNumber = user.AlternateNumber,
                Address = user.Address,
                UserImageUrl  =user.UserImageUrl,
                TenantId = user.TenantId,
                CreatedOn = user.CreatedOn,
                CreatedBy = user.CreatedBy,
               
                UpdatedBy = user.UpdatedBy
            };
        }

        public static List<UserResponseVM> ToViewModelList(List<MUser> users)
        {
            if (users == null)
                return new List<UserResponseVM>();

            return users
                .Where(user => user != null)
                .Select(user => ToViewModel(user)!)
                .Where(vm => vm != null)
                .ToList();
        }

    }
}