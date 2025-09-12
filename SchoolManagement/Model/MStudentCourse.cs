using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("student_course")]
    public class MStudentCourse : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("is_current_year")]
        public bool IsCurrentYear { get; set; } = true;

        // ----------------------------
        // Navigation Properties
        // ----------------------------
        [ForeignKey(nameof(StudentId))]
        public virtual MStudent Student { get; set; } = default!;

        [ForeignKey(nameof(CourseId))]
        public virtual MCourse Course { get; set; } = default!;

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; } = default!;

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
