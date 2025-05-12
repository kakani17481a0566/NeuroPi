using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Role
{
    public class RoleRequestVM
    {

        public string Name { get; set; }

        public int TenantId { get; set; }


        public static MRole ToModel(RoleRequestVM roleRequestVM)
        {
            return new MRole
            {
                Name = roleRequestVM.Name,
                TenantId = roleRequestVM.TenantId,
            };
        }
    }
}
