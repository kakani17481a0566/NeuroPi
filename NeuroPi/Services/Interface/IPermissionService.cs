using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.Permissions;
using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IPermissionService
    {

        PermissionResponseVM AddPermission(PermissionRequestVM permissionRequestVM);

        List<PermissionResponseVM> GetPermissions();

        PermissionResponseVM GetById(int id);

        MPermission DeletePermission(int id,int tenantId);

        PermissionResponseVM UpdatePermission(int id,int tenantId,PermissionUpdateRequestVM requestVM);

        List<PermissionResponseVM> GetAllPermissionsByTenantId(int tenantId);

        PermissionResponseVM GetByIdAndTenantId(int id,int tenantId);
        List<PermissionDescriptionVM> GetByPermissionId(int id);
    }
}
