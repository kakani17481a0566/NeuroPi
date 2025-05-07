using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Models
{
    [Table("role_permissions")]
    public class MRolePermission : MBaseModel
    {
        [Column("role_id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Column("permission_id")]
        [ForeignKey("Permission")]
        public int PermissionId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        [Column("can_create")]
        public int CanCreate { get; set; } = 0;

        [Column("can_read")]
        public int CanRead { get; set; } = 0;

        [Column("can_update")]
        public int CanUpdate { get; set; } = 0;

        [Column("can_delete")]
        public int CanDelete { get; set; } = 0;

        // Navigation properties
        public virtual MRole Role { get; set; }
        public virtual MPermission Permission { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}