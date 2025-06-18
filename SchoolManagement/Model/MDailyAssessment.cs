using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("daily_assessment")]
    public class MDailyAssessment : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("assessment_date")]
        public DateTime AssessmentDate { get; set; }

        [Required]
        [ForeignKey(nameof(TimeTable))]
        [Column("time_table_id")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [Required]
        [ForeignKey(nameof(Student))]
        [Column("student_id")]
        public int StudentId { get; set; }
        public virtual MStudent Student { get; set; }

        [Column("conducted_by")]
        public int ConductedById { get; set; }

        [ForeignKey(nameof(ConductedById))] // <-- THIS LINE IS MANDATORY
        public virtual MUser ConductedByUser { get; set; }



        [Required]
        [ForeignKey(nameof(Tenant))]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }

        [Column("grade_id")]
        public int? GradeId { get; set; }
        public virtual MGrade Grade { get; set; }

        [Column("branch_id")]
        [ForeignKey(nameof(Branch))]
        public int? BranchId { get; set; }
        public virtual MBranch Branch { get; set; }

        [Required]
        [ForeignKey(nameof(Assessment))]
        [Column("assessment_id")]
        public int AssessmentId { get; set; }
        public virtual MAssessment Assessment { get; set; }
    }
}
