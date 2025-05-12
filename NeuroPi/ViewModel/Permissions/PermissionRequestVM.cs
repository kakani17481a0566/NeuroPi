using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Permissions
{
    public class PermissionRequestVM
    {

        public string Name { get; set; }

        public string? Description { get; set; }


        public int? TenantId { get; set; }

        public static MPermission ToModel(PermissionRequestVM vm)
        {
            return new MPermission
            {
                Name = vm.Name,
                Description = vm.Description,
                TenantId = vm.TenantId,
            };
        }






    }
}
