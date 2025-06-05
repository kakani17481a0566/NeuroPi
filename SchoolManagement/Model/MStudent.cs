using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("students")]
    public class MStudent : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [ForeignKey("Course")]
        [Column("course_id")]
        public int CourseId { get; set; }
        public virtual MCourse Course { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
