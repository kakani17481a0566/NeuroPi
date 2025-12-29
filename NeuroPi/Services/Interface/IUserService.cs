using NeuroPi.UserManagment.ViewModel.User;
using System.Collections.Generic;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserService
    {
        UserLogInSucessVM LogIn(string username, string password);

        List<UserResponseVM> GetAllUsers();

        List<UserResponseVM> GetAllUsersByTenantId(int tenantId);

        UserResponseVM GetUser(int id, int tenantId);

        UserResponseVM AddUser(UserRequestVM request);

        UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate);

        UserResponseVM DeleteUser(int id, int tenantId);

        Task<string> UpdateUserImageAsync(int id, int tenantId, UserImageUploadVM request);

        bool UpdateUserPassword(int id, int tenantId, UserUpdatePasswordVM request, out string message);


        UsersProfileSummaryVM GetUserProfileSummary(int id, int tenantId);

        List<UserWithRoleVM> GetAllUsersByTenantIdWithRoles(int tenantId);


    }
}
