using NeuroPi.ViewModel.RolePermission;

namespace NeuroPi.Services.Interface
{
    public interface IRolePermisisionService
    {
        List<RolePermissionResponseVM> GetAllRolePermissions();
        RolePermissionResponseVM GetRolePermissionById(int id);
        bool DeleteById(int id);
        RolePermissionResponseVM AddRolePermission(RolePermissionRequestVM rolePermission);

        RolePermissionResponseVM UpdateRolePermissionById(int id, RolePermissionVM rolePermission);
    }
}




