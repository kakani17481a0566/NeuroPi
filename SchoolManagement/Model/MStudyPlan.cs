using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("study_plan")]
    public class MStudyPlan : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        [MaxLength(150)]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }

        public virtual ICollection<MStudyPlanSteps> StudyPlanSteps { get; set; } = new List<MStudyPlanSteps>();
        public virtual ICollection<MStudyCoursesMap> StudyCoursesMaps { get; set; } = new List<MStudyCoursesMap>();
        public virtual ICollection<MEmployeeStudyPlan> EmployeeStudyPlans { get; set; } = new List<MEmployeeStudyPlan>();
    }
}
