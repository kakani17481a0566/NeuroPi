using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Permissions
{
    public class PermissionResponseVM
    {

        public int PermissionId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }


        public int? TenantId { get; set; }

        public static PermissionResponseVM ToViewModel(MPermission permission)
        {
            return new PermissionResponseVM
            {
                PermissionId = permission.PermissionId,
                Name = permission.Name,
                Description = permission.Description,
                TenantId = permission.TenantId,
            };
        }

        public static List<PermissionResponseVM> ToViewModelList(List<MPermission> permissionList)
        {
            List<PermissionResponseVM> response = new List<PermissionResponseVM>();
            foreach (MPermission permission in permissionList)
            {
                response.Add(ToViewModel(permission));
            }
            return response;

        }

    }
}
