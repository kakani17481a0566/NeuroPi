using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.ViewModel.GroupUser;

namespace NeuroPi.Services.Interface
{
    public interface IGroupUserService
    {
        List<GroupUserVM> getAllGroupUsers();

        GroupUserUpdateVM updateGroupUserById(int GroupUserId,GroupUserUpdateVM input);
        GroupUserVM getGroupUserById(int Groupid);

        GroupUserVM createGroupUser(GroupUserInputVM input);

        

        bool deleteGroupUserById(int GroupId);

    }
}
