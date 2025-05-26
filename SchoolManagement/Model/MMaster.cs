using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MMaster : MBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("MasterType")]
        public int MastersType { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
    }
}
