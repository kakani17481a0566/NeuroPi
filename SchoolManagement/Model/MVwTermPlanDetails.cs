using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("vw_term_plan_details")] // Maps the C# class to the SQL view
    public class MVwTermPlanDetails
    {
        [Column("term_name")]
        [Required]
        public string TermName { get; set; } = string.Empty;

        [Column("term_start_date")]
        [Required]
        public DateTime TermStartDate { get; set; }

        [Column("term_end_date")]
        [Required]
        public DateTime TermEndDate { get; set; }

        [Column("week_period")]
        [Required]
        public string WeekPeriod { get; set; } = string.Empty;

        [Column("week_id")]
        [Required]
        public int WeekId { get; set; }

        [Column("week_long_name")]
        [Required]
        public string WeekLongName { get; set; } = string.Empty;

        [Column("week_start_date")]
        [Required]
        public DateTime WeekStartDate { get; set; }

        [Column("week_end_date")]
        [Required]
        public DateTime WeekEndDate { get; set; }

        [Column("schedule_date")]
        public DateTime? ScheduleDate { get; set; }

        [Column("day_of_week")]
        public string? DayOfWeek { get; set; }

        [Column("period_name")]
        public string? PeriodName { get; set; }

        [Column("start_time")]
        public TimeSpan? StartTime { get; set; }

        [Column("end_time")]
        public TimeSpan? EndTime { get; set; }

        [Column("subject_name")]
        public string? SubjectName { get; set; }

        [Column("subject_code")]
        public string? SubjectCode { get; set; }

        [Column("topic_name")]
        public string? TopicName { get; set; }

        [Column("topic_code")]
        public string? TopicCode { get; set; }

        [Column("topic_type_name")]
        public string? TopicTypeName { get; set; }

        [Column("assigned_worksheet_name")]
        public string? AssignedWorksheetName { get; set; }

        [Column("assigned_worksheet_description")]
        public string? AssignedWorksheetDescription { get; set; }

        [Column("assigned_worksheet_location")]
        public string? AssignedWorksheetLocation { get; set; }

        [Column("teacher_username")]
        public string? TeacherUsername { get; set; }

        [Column("teacher_user_id")]
        public int? TeacherUserId { get; set; }

        [Column("teacher_team_name")]
        public string? TeacherTeamName { get; set; }

        [Column("teacher_team_id")]
        public int? TeacherTeamId { get; set; }

        [Column("course_name")]
        public string? CourseName { get; set; }

        [Column("course_id")]
        public int? CourseId { get; set; }

        [Column("tenant_id")]
        [Required]
        public int TenantId { get; set; }
    }
}
