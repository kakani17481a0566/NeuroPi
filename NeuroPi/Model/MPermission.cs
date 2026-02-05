using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("permissions")]
    public class MPermission : MBaseModel
    {
        [Key]
        [Column("permission_id")]
        public int PermissionId { get; set; }

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
        //public virtual ICollection<MRolePermission> RolePermissions { get; set; } = new List<MRolePermission>();
    }
}
