using NeuroPi.UserManagment.ViewModel.User;
using System.Collections.Generic;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserService
    {
        UserLogInSucessVM LogIn(string username, string password);

        List<UserResponseVM> GetAllUsers();

        List<UserResponseVM> GetAllUsersByTenantId(int tenantId);
        List<UserResponseVM> GetAllUsersByTenantIdWithRoles(int tenantId);

        UserResponseVM GetUser(int id, int tenantId);

        UserResponseVM AddUser(UserRequestVM request, out string message);

        UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate);
        UserResponseVM UpdateUserContact(int id, int tenantId, UserContactUpdateVM contactUpdate);

        UserResponseVM DeleteUser(int id, int tenantId);

        Task<string> UpdateUserImageAsync(int id, int tenantId, UserImageUploadVM request);

        bool UpdateUserPassword(int id, int tenantId, UserUpdatePasswordVM request, out string message);

        bool ResetUserPassword(int id, int tenantId, AdminResetPasswordVM request, out string message);

        bool ForgotPassword(ForgotPasswordRequestVM request, out string message);
        bool ValidateOtp(ValidateOtpRequestVM request, out string message);
        bool ResetPasswordWithOtp(ResetPasswordOtpRequestVM request, out string message);

        UsersProfileSummaryVM GetUserProfileSummary(int id, int tenantId);

        bool CheckUsernameExists(string username);
        bool CheckFirstTimeLogin(string username, int tenantId);

        string SendMessage(string email);

    }
}
