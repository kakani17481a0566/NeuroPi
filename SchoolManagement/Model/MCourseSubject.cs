using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    public class MCourseSubject : MBaseModel
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual MCourse Course { get; set; }

        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual MSubject Subject { get; set; }

        [ForeignKey("Tenant")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
