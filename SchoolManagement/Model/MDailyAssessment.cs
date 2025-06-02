using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MDailyAssessment : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime AssessmentDate { get; set; }

        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual MTimeTable TimeTable { get; set; }

        [ForeignKey("Worksheet")]
        public int WorksheetId { get; set; }
        public virtual MWorksheet Worksheet { get; set; }

        public string Grade { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public virtual MStudent Student { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual MCourse Course { get; set; }

        [ForeignKey("ConductedBy")]
        public int ConductedById { get; set; }
        public virtual MUser ConductedBy { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
