using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IRolePermissionService
    {
        Task<RolePermissionResponseVM> AddRolePermissionAsync(RolePermissionRequestVM rolePermission);
        Task<RolePermissionResponseVM> UpdateRolePermissionByIdAsync(int id, RolePermissionVM rolePermission);
        Task<bool> DeleteByIdAsync(int id);
        Task<List<RolePermissionResponseVM>> GetAllRolePermissionsAsync();
        Task<RolePermissionResponseVM> GetRolePermissionByIdAsync(int id);
    }
}