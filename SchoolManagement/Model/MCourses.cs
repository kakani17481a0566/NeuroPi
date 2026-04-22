using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("courses")]
    public class MCourses : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("course_code")]
        public string CourseCode { get; set; }

        [Column("course_type_id")]
        public int? CourseTypeId { get; set; }

        [ForeignKey(nameof(CourseTypeId))]
        public virtual MMaster CourseType { get; set; }

        [Column("duration")]
        public string Duration { get; set; }

        [Column("apx_fee")]
        public decimal? ApxFee { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
