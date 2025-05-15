using NeuroPi.UserManagment.ViewModel.UserDepartment;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserDepartmentService
    {
        List<UserDepartmentResponseVM> GetAllUserDepartments();

        UserDepartmentResponseVM GetUserDepartmentById(int id);

        UserDepartmentResponseVM GetUserDepartmentByIdAndTenantId(int id, int tenantId);

        List<UserDepartmentResponseVM> GetUserDepartmentsByTenantId(int tenantId);


        UserDepartmentCreateVM CreateUserDepartment(UserDepartmentRequestVM input);

        UserDepartmentResponseVM UpdateUserDepartmentByUserDeptIdAndTenantId(int id, int tenantId, UserDepartmentUpdateVM input);

        bool DeleteUserDepartmentByUserDeptIdAndTenantId(int id, int tenantId);


    }
}
