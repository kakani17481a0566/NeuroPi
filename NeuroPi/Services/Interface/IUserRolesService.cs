using NeuroPi.UserManagment.ViewModel.UserRoles;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IUserRolesService
    {
        List<UserRoleVM> GetAll();
        UserRoleVM GetUserRoleById(int id);
        UserRoleVM Create(UserRoleCreateVM userRole);
        UserRoleVM Update(int id, UserRoleUpdateVM userRole);
        bool DeleteByTenantIdAndId(int tenantId, int id);
        List<UserRoleVM> GetUserRolesByTenantId(int tenantId);
        UserRoleVM GetUserRoleByTenantIdAndId(int tenantId, int id);
    }
}
