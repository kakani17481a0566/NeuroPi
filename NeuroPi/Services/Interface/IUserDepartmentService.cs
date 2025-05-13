using NeuroPi.UserManagment.ViewModel.UserDepartment;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserDepartmentService
    {
        List<UserDepartmentResponseVM> GetAllUserDepartments();

        UserDepartmentResponseVM GetUserDepartmentById(int id);

        UserDepartmentCreateVM CreateUserDepartment(UserDepartmentRequestVM input);

        UserDepartmentResponseVM UpdateUserDepartment(int id, UserDepartmentUpdateVM input);

        bool DeleteUserDepartment(int id);


    }
}
