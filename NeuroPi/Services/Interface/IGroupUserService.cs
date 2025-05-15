using NeuroPi.UserManagment.ViewModel.GroupUser;
using System.Collections.Generic;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IGroupUserService
    {
        List<GroupUserVM> getAllGroupUsers();

        GroupUserVM createGroupUser(GroupUserInputVM input);

        GroupUserUpdateVM updateGroupUserByIdAndTenantId(int groupUserId, int tenantId, GroupUserUpdateVM input);

        GroupUserVM getGroupUserByGroupUserId(int groupUserId);

        GroupUserVM getGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId);

        List<GroupUserVM> getGroupUsersByTenantId(int tenantId);

        bool deleteGroupUserByGroupUserIdAndTenantId(int groupUserId, int tenantId);

    }
}
