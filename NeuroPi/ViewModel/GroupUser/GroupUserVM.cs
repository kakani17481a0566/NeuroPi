using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.GroupUser
{
    public class GroupUserVM
    {

        public int GroupId { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        //[Required]
        //public int? CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; }

        //public DateTime UpdatedOn { get; set; }

        public static GroupUserVM ToViewModel(MGroupUser user)
        {
            return new GroupUserVM
            {
                GroupId = user.GroupId,
                UserId = user.UserId,
                TenantId = user.TenantId,
                //CreatedBy = user.CreatedBy,
                //CreatedOn = user.CreatedOn

            };
        }
        public static List<GroupUserVM> ToViewModelList(List<MGroupUser> groupUsers)
        {
            List<GroupUserVM> groupUserVMs = new List<GroupUserVM>();
            foreach (MGroupUser user in groupUsers)
            {
                groupUserVMs.Add(ToViewModel(user));
            }
            return groupUserVMs;
        }



    }
}
