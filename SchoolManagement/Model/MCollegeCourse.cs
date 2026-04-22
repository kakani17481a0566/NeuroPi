using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("college_course")]
    public class MCollegeCourse : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("college_id")]
        public int CollegeId { get; set; }

        [ForeignKey(nameof(CollegeId))]
        public virtual MCollegeDetail College { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [ForeignKey(nameof(CourseId))]
        public virtual MCourses Course { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
