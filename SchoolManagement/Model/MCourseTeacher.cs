using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("course_teacher")]
    public class MCourseTeacher : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("course_id")]
        public int CourseId { get; set; }


        [ForeignKey("CourseId")]
        public MCourse Course {  get; set; }

        [Required]
        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        // Navigation properties
        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; } = null!;

        [ForeignKey("BranchId")]
        public MBranch Branch { get; set; }



        // Add other navigation properties like Course, Teacher, Branch if models exist
    }
}
