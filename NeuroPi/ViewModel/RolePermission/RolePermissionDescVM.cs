using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.RolePermission
{
    public class RolePermissionDescVM
    {
        public int RolePermissionId { get; set; }

        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public string PermissionName { get; set; }

        public string? PermissionDescription { get; set; }
        public int TenantId { get; set; }

        public static RolePermissionDescVM ToViewModel(MRolePermission rolePermission)
        {
            return new RolePermissionDescVM 
            {
                RolePermissionId = rolePermission.RolePermissionId,
                RoleId = rolePermission.RoleId,
                PermissionId = rolePermission.PermissionId,
                PermissionName = rolePermission.Permission.Name,
                PermissionDescription = rolePermission.Permission.Description,
                TenantId = rolePermission.TenantId

            };

        }

        public static List<RolePermissionDescVM> ToViewModelList(List<MRolePermission> rolePermissions)
        {
            List<RolePermissionDescVM> result = new List<RolePermissionDescVM>();
            foreach(MRolePermission rolePermission in rolePermissions)
            {
                result.Add(ToViewModel(rolePermission));
            }
            return result;
        }
    }
}
