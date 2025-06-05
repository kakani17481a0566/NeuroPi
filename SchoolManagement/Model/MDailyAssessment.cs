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

        [ForeignKey("TimeTable")]
        [Column("time_table_id")]
        public int TimeTableId { get; set; }

        [ForeignKey("Worksheet")]
        [Column("worksheet_id")]
        public int WorksheetId { get; set; }

        [ForeignKey("Student")]
        [Column("student_id")]
        public int StudentId { get; set; }

        //[ForeignKey("Course")]
        //[Column("course_id")]
        //public int CourseId { get; set; } 

        [ForeignKey("ConductedBy")]
        [Column("conducted_by")]
        public int ConductedById { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("grade_id")]
        public int? GradeId { get; set; } 

        public virtual MTenant Tenant { get; set; }
    }
}