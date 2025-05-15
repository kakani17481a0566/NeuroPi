using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.User;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserService
    {
        UserLogInSucessVM LogIn(string username, string password);

        List<UserResponseVM> GetAllUsers();

        UserResponseVM GetUser(int id);

        UserResponseVM GetUserByIdAndTenantId(int id, int tenantId);

        List<UserResponseVM> GetAllUsersByTenantId(int tenantId);

        UserResponseVM AddUser(UserRequestVM request);

        UserResponseVM UpdateUser(int id, int tenantId, UserUpdateRequestVM userUpdate);

        UserResponseVM DeleteUser(int id, int tenantId);
    }
}