using NeuroPi.UserManagment.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("time_table")]
    public class MTimeTable : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("date")]
        public DateTime Date { get; set; }

        [ForeignKey(nameof(Week))]
        [Column("week_id")]
        public int? WeekId { get; set; }
        public virtual MWeek Week { get; set; }

        [ForeignKey(nameof(PublicHoliday))]
        [Column("holiday_id")]
        public int? HolidayId { get; set; }
        public virtual MPublicHoliday PublicHoliday { get; set; }

        [Column("status")]
        public string Status { get; set; } //"working" or "holiday"

        [ForeignKey(nameof(Course))]
        [Column("course_id")]
        public int? CourseId { get; set; }
        public virtual MCourse Course { get; set; }

        [ForeignKey(nameof(AssessmentStatus))]
        [Column("assessment_status_code")]
        public int? AssessmentStatusCode { get; set; }
        public virtual MMaster AssessmentStatus { get; set; }

        [Required]
        [ForeignKey(nameof(Tenant))]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        public virtual MTenant Tenant { get; set; }
    }



}
