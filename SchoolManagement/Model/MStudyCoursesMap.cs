using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("study_courses_map")]
    public class MStudyCoursesMap : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("study_plan_id")]
        public int StudyPlanId { get; set; }

        [Column("study_courses_id")]
        public int StudyCoursesId { get; set; }

        [Column("seq_ord")]
        public int? SeqOrd { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(StudyPlanId))]
        public virtual MStudyPlan StudyPlan { get; set; } = default!;

        [ForeignKey(nameof(StudyCoursesId))]
        public virtual MStudyCourses StudyCourse { get; set; } = default!;

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
