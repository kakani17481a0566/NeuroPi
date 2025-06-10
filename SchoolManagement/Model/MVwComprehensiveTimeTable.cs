using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("vw_comprehensive_time_table")]
    public class MVwComprehensiveTimeTable
    {
        [Column("schedule_date")]
        [Required]
        public DateTime ScheduleDate { get; set; }

        [Column("day_of_week")]
        [Required]
        public string DayOfWeek { get; set; } = string.Empty;

        [Column("period_name")]
        [Required]
        public string PeriodName { get; set; } = string.Empty;

        [Column("start_time")]
        [Required]
        public TimeSpan StartTime { get; set; }

        [Column("end_time")]
        [Required]
        public TimeSpan EndTime { get; set; }

        [Column("subject_name")]
        [Required]
        public string SubjectName { get; set; } = string.Empty;

        [Column("subject_code")]
        [Required]
        public string SubjectCode { get; set; } = string.Empty;

        [Column("topic_name")]
        public string? TopicName { get; set; }

        [Column("topic_code")]
        public string? TopicCode { get; set; }

        [Column("teacher_username")]
        public string? TeacherUsername { get; set; }

        [Column("teacher_user_id")]
        [Required]
        public int TeacherUserId { get; set; }

        [Column("course_name")]
        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Column("tenant_id")]
        [Required]
        public int TenantId { get; set; }

        [Column("team_name")]
        public string? TeamName { get; set; }

        [Column("team_id")]
        public int? TeamId { get; set; }

        [Column("assigned_worksheet_name")]
        public string? AssignedWorksheetName { get; set; }

        [Column("assigned_worksheet_description")]
        public string? AssignedWorksheetDescription { get; set; }

        [Column("location_name")]
        public string? LocationName { get; set; }

        [Column("week_id")]
        public int? WeekId { get; set; }

        [Column("term_id")]
        public int? TermId { get; set; }

        [Column("week_long_name")]
        public string? WeekLongName { get; set; }

        [Column("week_start_date")]
        public DateTime? WeekStartDate { get; set; }

        [Column("week_end_date")]
        public DateTime? WeekEndDate { get; set; }

        [Column("week_created_on")]
        public DateTimeOffset? WeekCreatedOn { get; set; }

        [Column("week_created_by")]
        public int? WeekCreatedBy { get; set; }

        [Column("week_updated_on")]
        public DateTimeOffset? WeekUpdatedOn { get; set; }

        [Column("week_updated_by")]
        public int? WeekUpdatedBy { get; set; }

        [Column("week_is_deleted")]
        public bool? WeekIsDeleted { get; set; }
    }
}
