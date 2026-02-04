using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.RolePermission
{
    public class RolePermissionResponseVM
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public int TenantId { get; set; }

        public int CanCreate { get; set; }

        public int CanRead { get; set; }

        public int CanUpdate { get; set; }

        public int CanDelete { get; set; }

        public int? CreatedBy { get; set; }


        public static RolePermissionResponseVM ToViewModel(MRolePermission rolePermission)
        {
            return new RolePermissionResponseVM
            {
                RolePermissionId = rolePermission.RolePermissionId,
                RoleId = rolePermission.RoleId,
                PermissionId = rolePermission.MenuId,
                TenantId = rolePermission.TenantId,
                CreatedBy = rolePermission.CreatedBy
            };
        }
        public static List<RolePermissionResponseVM> ToViewModelList(List<MRolePermission> rolePermissions)
        {
            List<RolePermissionResponseVM> result = new List<RolePermissionResponseVM>();
            foreach (MRolePermission rolePermission in rolePermissions)
            {
                result.Add(ToViewModel(rolePermission));
            }
            return result;
        }
    }
}
