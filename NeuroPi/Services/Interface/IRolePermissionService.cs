using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IRolePermissionService
    {
        RolePermissionResponseVM AddRolePermission(RolePermissionRequestVM rolePermission);
        RolePermissionResponseVM UpdateRolePermissionById(int id, RolePermissionVM rolePermission);
        bool DeleteById(int id);
        List<RolePermissionResponseVM> GetAllRolePermissions();
        RolePermissionResponseVM GetRolePermissionById(int id);
    }
}
