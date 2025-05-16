
namespace NeuroPi.UserManagment.ViewModel.RolePermission
{
    public class RolePermissionVM
    {
        //public int RolePermissionId { get; set; }
        public int RoleId { get; set; }

        public int PermissionId { get; set; }

        //public int TenantId { get; set; }

        public int CanCreate { get; set; }

        public int CanRead { get; set; }

        public int CanUpdate { get; set; }

        public int CanDelete { get; set; }
        public int UpdatedBy { get;  set; }

    }
}
