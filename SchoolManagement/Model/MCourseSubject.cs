using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("course_subject")]
    public class MCourseSubject : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("course_id")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        //public virtual MCourse Course { get; set; }

        [Required]
        [Column("subject_id")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        //public virtual MSubject Subject { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
