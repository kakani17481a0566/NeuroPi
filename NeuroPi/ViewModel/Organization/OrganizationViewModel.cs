using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.ViewModel.Organization
{
    public class OrganizationViewModel
    {
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public int TenantId { get; set; }
    }
}
