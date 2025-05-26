using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MMasterType : MBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
    }
}
