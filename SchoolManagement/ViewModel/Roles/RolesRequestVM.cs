using SchoolManagement.Model;
using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel
{
    public class RolesRequestVM
    {
        [Required]
        public string Name { get; set; }

        public int TenantId { get; set; }
        
        public int CreatedBy { get; set; }

        public static MRole ToModel(RolesRequestVM vm)
        {
            return new MRole
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}
