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

        [Required]
        [Column("course_id")]
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }
        public virtual MCourse Course { get; set; }

        [Required]
        [Column("tenant_id")]
        [ForeignKey(nameof(Tenant))]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }
}
