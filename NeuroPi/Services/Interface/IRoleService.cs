using NeuroPi.UserManagment.ViewModel.Role;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IRoleService
    {
        List<RoleResponseVM> GetAllRoles();

        RoleResponseVM GetRoleById(int id);

        RoleResponseVM AddRole(RoleRequestVM role);

        RoleResponseVM UpdateRole(int id, RoleRequestVM role);

        bool DeleteRoleById(int id);


    }
}
