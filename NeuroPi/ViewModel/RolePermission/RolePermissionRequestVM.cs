using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.RolePermission
{
    public class RolePermissionRequestVM

    {
       
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        public int TenantId { get; set; }

        public int CanCreate { get; set; }

        public int CanRead { get; set; }

        public int CanUpdate { get; set; }

        public int CanDelete { get; set; }

        public int CreatedBy { get; set; }


        public static MRolePermission ToModel(RolePermissionRequestVM rolePermission)
        {
            return new MRolePermission
            {
                RoleId = rolePermission.RoleId,
                PermissionId = rolePermission.PermissionId,
                TenantId = rolePermission.TenantId,
                CanCreate = rolePermission.CanCreate,
                CanRead = rolePermission.CanRead,
                CanUpdate = rolePermission.CanUpdate,
                CanDelete = rolePermission.CanDelete,
                
            };
        }
    }
}
