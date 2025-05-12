using NeuroPi.Models;
using NeuroPi.ViewModel.Permissions;

namespace NeuroPi.Services.Interface
{
    public interface IPermissionService
    {

        PermissionResponseVM AddPermission(PermissionRequestVM permissionRequestVM);

        List<PermissionResponseVM> GetPermissions();

        MPermission GetById(int id);

        MPermission DeletePermission(int id);

        PermissionResponseVM UpdatePermission(int id,PermissionRequestVM requestVM);
    }
}
