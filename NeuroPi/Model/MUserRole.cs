using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("user_roles")]
    public class MUserRole : MBaseModel
    {
        [Column("user_id")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Column("role_id")]
        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        // Navigation properties
        public virtual MUser User { get; set; }
        public virtual MRole Role { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}