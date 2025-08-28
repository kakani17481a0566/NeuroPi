using System;
using System.Collections.Generic;
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

        [Column("surname")]
        public string Surname { get; set; }

        [Column("dob")]
        public DateOnly? DateOfBirth { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("student_image_url")]
        public string StudentImageUrl { get; set; }

        [Column("bloodgroup")]
        public string BloodGroup { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        // Navigation properties

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual MCourse Course { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; }

        public virtual ICollection<MStudentAttendance> StudentAttendances { get; set; } = new List<MStudentAttendance>();

        public virtual ICollection<MParentStudent> ParentStudents { get; set; }

        public virtual ICollection<MDailyAssessment> DailyAssessments { get; set; } = new List<MDailyAssessment>();
    }
}
