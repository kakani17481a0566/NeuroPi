using NeuroPi.UserManagment.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("parents")]
    public class MParent : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual MUser User { get; set; } = default!;

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        // ----------------------------
        // Audit Navigation (from MBaseModel)
        // ----------------------------
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        // ----------------------------
        // Relationships
        // ----------------------------
        public virtual ICollection<MParentStudent> ParentStudents { get; set; } = new List<MParentStudent>();
    }
}
