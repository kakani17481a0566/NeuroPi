using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.UserManagment.Model
{
    [Table("roles")]
    public class MRole : MBaseModel
    {
        [Key]
        [Column("role_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }

        [MaxLength(50)]
        [Column("name")]
        public string? Name { get; set; }   // nullable to match DB

        [Column("tenant_id")]
        public int TenantId { get; set; }

        // ----------------------------
        // Navigation Properties
        // ----------------------------
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        public virtual ICollection<MUserRole> UserRoles { get; set; } = new List<MUserRole>();
        public virtual ICollection<MRolePermission> RolePermissions { get; set; } = new List<MRolePermission>();
    }
}
