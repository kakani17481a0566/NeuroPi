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
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual MParent Parent { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual MStudent Student { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        // Navigation for CreatedBy
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser CreatedByUser { get; set; }

        // Navigation for UpdatedBy (nullable)
        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
