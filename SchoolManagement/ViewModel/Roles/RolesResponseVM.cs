using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Roles
{
    public class RolesResponseVM
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public static RolesResponseVM ToViewModel(MRoles role) =>
            new RolesResponseVM
            {
                RoleId = role.RoleId,
                Name = role.Name,
                TenantId = role.TenantId,
                CreatedBy = role.CreatedBy,
                CreatedOn = role.CreatedOn
            };

        public static List<RolesResponseVM> ToViewModelList(List<MRoles> roles)
        {
            return roles.Select(ToViewModel).ToList();
        }
    }
}
