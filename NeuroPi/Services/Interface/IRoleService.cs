using NeuroPi.UserManagment.ViewModel.Role;

public interface IRoleService
{
    List<RoleResponseVM> GetAllRoles();
    RoleResponseVM GetRoleById(int id);
    RoleResponseVM AddRole(RoleRequestVM role);
    RoleResponseVM UpdateRoleByTenantIdAndId(int tenantId, int id, RoleUpdateRequestVM roleUpdateRequestVM);

    bool DeleteRoleByTenantIdAndId(int tenantId, int id); 
    List<RoleResponseVM> GetAllRolesByTenantId(int tenantId);
    RoleResponseVM GetRoleByTenantIdAndId(int tenantId, int id);
}
