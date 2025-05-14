using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.User;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserService
    {
        UserLogInSucessVM LogIn(string username,string password);
    }
}
