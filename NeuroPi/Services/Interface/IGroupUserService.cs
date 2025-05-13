using NeuroPi.UserManagment.ViewModel.GroupUser;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IGroupUserService
    {
        List<GroupUserVM> getAllGroupUsers();

        GroupUserUpdateVM updateGroupUserById(int GroupUserId, GroupUserUpdateVM input);
        GroupUserVM getGroupUserById(int Groupid);

        GroupUserVM createGroupUser(GroupUserInputVM input);

        List<GroupUserVM> getGroupUsersByTenantId(int tenantId);



        bool deleteGroupUserById(int GroupId);

    }
}
