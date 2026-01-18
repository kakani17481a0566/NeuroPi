using SchoolManagement.Model;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.Roles
{
    public class RolesRequestVM
    {
        [Required]
        public string Name { get; set; }

        public int TenantId { get; set; }
        
        public int CreatedBy { get; set; }

        public static MRoles ToModel(RolesRequestVM request) =>
            new MRoles
            {
                Name = request.Name,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
    }
}
