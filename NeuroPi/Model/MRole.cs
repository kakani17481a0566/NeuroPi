using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NeuroPi.Models
{
    [Table("roles")]
    public class MRole : MBaseModel
    {
        [Key]
        [Column("role_id")]
        public int RoleId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MTenant Tenant { get; set; }
        public virtual ICollection<MUserRole> UserRoles { get; set; } = new List<MUserRole>();
        public virtual ICollection<MRolePermission> RolePermissions { get; set; } = new List<MRolePermission>();
    }
}
