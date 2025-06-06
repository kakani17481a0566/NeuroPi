using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    /// <summary>
    /// Represents the data structure for the vw_comprehensive_time_table SQL View.
    /// This model combines information about the time table, periods, subjects,
    /// topics, teachers, teams, assigned worksheets, and week details,
    /// with annotations for mapping to specific view columns and their nullability.
    /// </summary>
    [Table("vw_comprehensive_time_table")] // Maps the C# class to the SQL view
    public class MVwComprehensiveTimeTable
    {
        /// <summary>
        /// The date of the time table entry. (SQL: schedule_date, NOT NULL from tt.date)
        /// </summary>
        [Column("schedule_date")]
        [Required]
        public DateTime ScheduleDate { get; set; }

        /// <summary>
        /// The day of the week (e.g., 'Monday', 'Tuesday'). (SQL: day_of_week, derived, NOT NULL)
        /// </summary>
        [Column("day_of_week")]
        [Required]
        public string DayOfWeek { get; set; } = string.Empty;

        /// <summary>
        /// The name of the period (e.g., 'Period 1'). (SQL: period_name, NOT NULL from p.name)
        /// </summary>
        [Column("period_name")]
        [Required]
        public string PeriodName { get; set; } = string.Empty;

        /// <summary>
        /// The start time of the period. (SQL: start_time, NOT NULL from p.start_time)
        /// </summary>
        [Column("start_time")]
        [Required]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// The end time of the period. (SQL: end_time, NOT NULL from p.end_time)
        /// </summary>
        [Column("end_time")]
        [Required]
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// The name of the subject for this period. (SQL: subject_name, NOT NULL from s.name)
        /// </summary>
        [Column("subject_name")]
        [Required]
        public string SubjectName { get; set; } = string.Empty;

        /// <summary>
        /// The code of the subject. (SQL: subject_code, NOT NULL from s.code)
        /// </summary>
        [Column("subject_code")]
        [Required]
        public string SubjectCode { get; set; } = string.Empty;

        /// <summary>
        /// The name of the topic covered (if any). (SQL: topic_name, NULLABLE from t.name via LEFT JOIN)
        /// </summary>
        [Column("topic_name")]
        public string? TopicName { get; set; }

        /// <summary>
        /// The code of the topic (if any). (SQL: topic_code, NULLABLE from t.code via LEFT JOIN)
        /// </summary>
        [Column("topic_code")]
        public string? TopicCode { get; set; }

        /// <summary>
        /// The username of the teacher assigned to this period. (SQL: teacher_username, NULLABLE from u.username via LEFT JOIN)
        /// </summary>
        [Column("teacher_username")]
        public string? TeacherUsername { get; set; }

        /// <summary>
        /// The user ID of the teacher assigned to this period. (SQL: teacher_user_id, NOT NULL from ttd.teacher_id)
        /// </summary>
        [Column("teacher_user_id")]
        [Required]
        public int TeacherUserId { get; set; }

        /// <summary>
        /// The name of the course. (SQL: course_name, NOT NULL from c.name)
        /// </summary>
        [Column("course_name")]
        [Required]
        public string CourseName { get; set; } = string.Empty;

        /// <summary>
        /// The tenant ID for the time table entry. (SQL: tenant_id, NOT NULL from tt.tenant_id)
        /// </summary>
        [Column("tenant_id")]
        [Required]
        public int TenantId { get; set; }

        /// <summary>
        /// The name of the team the teacher belongs to (if any). (SQL: team_name, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("team_name")]
        public string? TeamName { get; set; }

        /// <summary>
        /// The ID of the team the teacher belongs to (if any). (SQL: team_id, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("team_id")]
        public int? TeamId { get; set; }

        /// <summary>
        /// The name of the worksheet assigned for this time slot (if any). (SQL: assigned_worksheet_name, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("assigned_worksheet_name")]
        public string? AssignedWorksheetName { get; set; }

        /// <summary>
        /// The description of the assigned worksheet (if any). (SQL: assigned_worksheet_description, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("assigned_worksheet_description")]
        public string? AssignedWorksheetDescription { get; set; }

        /// <summary>
        /// The location associated with the worksheet. (SQL: location_name, NULLABLE from w.location)
        /// </summary>
        [Column("location_name")]
        public string? LocationName { get; set; }

        /// <summary>
        /// ID of the week. (SQL: week_id, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_id")]
        public int? WeekId { get; set; }

        /// <summary>
        /// Term ID the week belongs to. (SQL: term_id, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("term_id")]
        public int? TermId { get; set; }

        /// <summary>
        /// Full name of the week (e.g., 'Week 1'). (SQL: week_long_name, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_long_name")]
        public string? WeekLongName { get; set; }

        /// <summary>
        /// Start date of the week. (SQL: week_start_date, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_start_date")]
        public DateTime? WeekStartDate { get; set; }

        /// <summary>
        /// End date of the week. (SQL: week_end_date, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_end_date")]
        public DateTime? WeekEndDate { get; set; }

        /// <summary>
        /// Week creation timestamp. (SQL: week_created_on, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_created_on")]
        public DateTimeOffset? WeekCreatedOn { get; set; }

        /// <summary>
        /// User who created the week. (SQL: week_created_by, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_created_by")]
        public int? WeekCreatedBy { get; set; }

        /// <summary>
        /// Week update timestamp. (SQL: week_updated_on, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_updated_on")]
        public DateTimeOffset? WeekUpdatedOn { get; set; }

        /// <summary>
        /// User who last updated the week. (SQL: week_updated_by, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_updated_by")]
        public int? WeekUpdatedBy { get; set; }

        /// <summary>
        /// Week soft-delete status. (SQL: week_is_deleted, NULLABLE via LEFT JOIN)
        /// </summary>
        [Column("week_is_deleted")]
        public bool? WeekIsDeleted { get; set; }
    }
}
