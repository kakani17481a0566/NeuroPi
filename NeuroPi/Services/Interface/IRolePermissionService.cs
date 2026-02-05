using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IRolePermissionService

    {
        List<RolePermissionResponseVM> GetAllRolePermissions();

        RolePermissionResponseVM GetRolePermissionById(int id);

        RolePermissionResponseVM GetRolePermissionByIdAndTenantId(int id, int tenantId);

        List<RolePermissionResponseVM> GetRolePermissionByTenantId(int tenantId);

        RolePermissionResponseVM AddRolePermission(RolePermissionRequestVM rolePermission);

        RolePermissionResponseVM UpdateRolePermissionByIdAndTenantId(int id, int tenantId, RolePermissionVM rolePermission);

        List<RolePermissionDescVM> GetRolePermissionByRoleIdAndTenantId(int roleId, int tenantId);

        bool DeleteByIdAndTenantId(int id, int tenantId);

        string AddMenus(List<RolePermissionRequestVM> menulists);

        
    }
}
 