using SchoolManagement.Model;
using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagement.ViewModel
{
    public class RolesResponseVM
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public static RolesResponseVM ToViewModel(MRole role)
        {
            return new RolesResponseVM
            {
                RoleId = role.RoleId,
                Name = role.Name,
                TenantId = role.TenantId
            };
        }

        public static List<RolesResponseVM> ToViewModelList(List<MRole> roles)
        {
            if (roles == null) return new List<RolesResponseVM>();
            return roles.Select(x => ToViewModel(x)).ToList();
        }
    }
}
