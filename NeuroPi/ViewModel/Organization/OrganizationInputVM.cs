using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.ViewModel.Organization
{
    public class OrganizationInputVM
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int TenantId { get; set; }

        

        public int CreatedBy { get; set; }
    }

}
