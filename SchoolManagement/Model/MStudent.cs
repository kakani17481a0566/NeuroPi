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

        // DB: first_name
        [Column("first_name")]
        public string Name { get; set; } = default!;

        // DB: last_name
        [Column("last_name")]
        public string? Surname { get; set; }

        // DB: middle_name
        [Column("middle_name")]
        public string? MiddleName { get; set; }

        // DB: dob (date)
        [Column("dob")]
        public DateOnly? DateOfBirth { get; set; }

        // DB: course_id
        [Column("course_id")]
        public int CourseId { get; set; }

        // DB: branch_id
        [Column("branch_id")]
        public int BranchId { get; set; }

        // DB: tenant_id
        [Column("tenant_id")]
        public int TenantId { get; set; }

        // DB: student_image_url
        [Column("student_image_url")]
        public string? StudentImageUrl { get; set; }

        // DB: bloodgroup
        [Column("bloodgroup")]
        public string? BloodGroup { get; set; }

        // DB: gender
        [Column("gender")]
        public string? Gender { get; set; }

        // DB: admission_grade
        [Column("admission_grade")]
        public string? AdmissionGrade { get; set; }

        // Navigation properties
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; } = default!;

        [ForeignKey(nameof(CourseId))]
        public virtual MCourse Course { get; set; } = default!;

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; } = default!;

        public virtual ICollection<MStudentAttendance> StudentAttendances { get; set; } = new List<MStudentAttendance>();
        public virtual ICollection<MParentStudent> ParentStudents { get; set; } = new List<MParentStudent>();
        public virtual ICollection<MDailyAssessment> DailyAssessments { get; set; } = new List<MDailyAssessment>();
    }
}
