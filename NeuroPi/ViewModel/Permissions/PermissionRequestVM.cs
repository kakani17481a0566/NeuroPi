using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Permissions
{
    public class PermissionRequestVM
    {

        public string Name { get; set; }

        public string? Description { get; set; }


        public int? TenantId { get; set; }

        public int CreatedBy { get; set; }

        //public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public static MPermission ToModel(PermissionRequestVM vm)
        {
            return new MPermission
            {
                Name = vm.Name,
                Description = vm.Description,
                TenantId = vm.TenantId,
               CreatedBy = vm.CreatedBy,
                //CreatedOn = vm.CreatedOn,
                IsDeleted = false
            };
        }






    }
}
