using NeuroPi.ViewModel.Role;

namespace NeuroPi.Services.Interface
{
    public interface IRoleService
    {
        List<RoleResponseVM> GetAllRoles();

        RoleResponseVM GetRoleById(int id);

        RoleResponseVM AddRole(RoleRequestVM role);

        RoleResponseVM UpdateRole( int id,RoleRequestVM role);

        void DeleteRoleById(int id);
    }
}
