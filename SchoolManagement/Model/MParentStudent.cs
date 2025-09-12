using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("parent_student")]
    public class MParentStudent : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual MParent Parent { get; set; } = default!;

        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual MStudent Student { get; set; } = default!;

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        // ----------------------------
        // Audit Navigation (from MBaseModel)
        // ----------------------------
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }   // ✅ make nullable

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
