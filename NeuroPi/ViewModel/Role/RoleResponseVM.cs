using NeuroPi.Models;

namespace NeuroPi.ViewModel.Role
{
    public class RoleResponseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TenantId { get; set; }


        public static RoleResponseVM ToViewModel(MRole role)
        {
            return new RoleResponseVM
            {
                Id = role.RoleId,
                Name = role.Name,
                TenantId = role.TenantId,
            };
        }
        public static List<RoleResponseVM> ToViewModelList(List<MRole> role)
        {
            List<RoleResponseVM> roleResponseVMs = new List<RoleResponseVM>();
            foreach (MRole roleItem in role)
            {
                roleResponseVMs.Add(ToViewModel(roleItem));
            }
            return roleResponseVMs;

        }

    }
}
