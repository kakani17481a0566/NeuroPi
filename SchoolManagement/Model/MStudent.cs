using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("students")]
    public class MStudent : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual MCourse Course { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; }

        public virtual ICollection<MStudentAttendance> StudentAttendances { get; set; } = new List<MStudentAttendance>();

        public virtual ICollection<MParentStudent> ParentStudents { get; set; }



        // Tenant navigation already present
    }
}
