using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Models
{
    [Table("permissions")]
    public class MPermission : MBaseModel
    {
        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MRolePermission> RolePermissions { get; set; }
    }
}