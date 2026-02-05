using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.RolePermission
{
    public class RolePermissionRequestVM

    {
       
        public int RoleId { get; set; }

        public int MenuId { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public string Permissions {  get; set; }


        public static MRolePermission ToModel(RolePermissionRequestVM rolePermission)
        {
            return new MRolePermission
            {
                RoleId = rolePermission.RoleId,
                MenuId = rolePermission.MenuId,
                TenantId = rolePermission.TenantId,
                CreatedBy=rolePermission.createdBy,
                Permissions = rolePermission.Permissions
            };
        }
    }
}
