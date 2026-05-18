using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("employee_study_plan")]
    public class MEmployeeStudyPlan : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("employee_details_id")]
        public int EmployeeDetailsId { get; set; }

        [Column("study_plan_id")]
        public int StudyPlanId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(EmployeeDetailsId))]
        public virtual MEmployeeDetail EmployeeDetail { get; set; } = default!;

        [ForeignKey(nameof(StudyPlanId))]
        public virtual MStudyPlan StudyPlan { get; set; } = default!;

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
