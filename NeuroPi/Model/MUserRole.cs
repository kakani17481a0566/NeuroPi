using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.Model;

namespace NeuroPi.Models
{
    [Table("user_roles")]
    public class MUserRole : MBaseModel
    {
        // Primary key for the user_role record
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremented primary key
        [Column("user_role_id")] // Maps to 'user_role_id' column in the database
        public int UserRoleId { get; set; }

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
