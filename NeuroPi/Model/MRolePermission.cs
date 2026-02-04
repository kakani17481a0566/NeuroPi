using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("role_permissions")]
    public class MRolePermission : MBaseModel
    {
        [Key]
        [Column("role_permission_id")]
        public int RolePermissionId { get; set; }

        [Column("role_id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Column("menu_id")]
        [ForeignKey("Menu")]
        public int MenuId { get; set; }

        [Column("permissions")]
        public string Permissions {get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

     

        // Navigation properties
        public virtual MRole Role { get; set; }
        public virtual MMenu Menu { get; set; }

        public virtual MTenant Tenant { get; set; }
    }
}
