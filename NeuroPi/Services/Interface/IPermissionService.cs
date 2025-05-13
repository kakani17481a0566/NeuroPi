using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.Permissions;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IPermissionService
    {

        PermissionResponseVM AddPermission(PermissionRequestVM permissionRequestVM);

        List<PermissionResponseVM> GetPermissions();

        MPermission GetById(int id);

        MPermission DeletePermission(int id);

        PermissionResponseVM UpdatePermission(int id,PermissionRequestVM requestVM);

        List<PermissionResponseVM> GetAllPermissionsByTenantId(int tenantId);
    }
}
