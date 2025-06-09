using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("v_term_table")]
    public class MVTermTable
    {
        [Column("term_id")]
        public int? TermId { get; set; }

        [Column("term_name")]
        public string? TermName { get; set; }

        [Column("start_date")]
        public DateTime? StartDate { get; set; }

        [Column("end_date")]
        public DateTime? EndDate { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        [Column("date")]
        public DateTime? Date { get; set; }

        [Column("day_name")]
        public string? DayName { get; set; }

        [Column("time_table_id")]
        public int? TimeTableId { get; set; }

        [Column("holiday_id")]
        public int? HolidayId { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("course_id")]
        public int? CourseId { get; set; }

        [Column("course_name")]
        public string? CourseName { get; set; }

        [Column("time_table_detail_id")]
        public int? TimeTableDetailId { get; set; }

        [Column("period_id")]
        public int? PeriodId { get; set; }

        [Column("period_name")]
        public string? PeriodName { get; set; }

        [Column("subject_id")]
        public int? SubjectId { get; set; }

        [Column("subject_name")]
        public string? SubjectName { get; set; }

        [Column("subject_code")]
        public string? SubjectCode { get; set; }

        [Column("time_table_topic_id")]
        public int? TimeTableTopicId { get; set; }

        [Column("topic_id")]
        public int? TopicId { get; set; }

        [Column("topic_name")]
        public string? TopicName { get; set; }

        [Column("topic_description")]
        public string? TopicDescription { get; set; }

        [Column("topic_type_name")]
        public string? TopicTypeName { get; set; }

        [Column("topic_type_code")]
        public string? TopicTypeCode { get; set; }


        [Column("week_id")]
        public int? WeekId { get; set; }

        [Column("week_name")]
        public string? WeekName { get; set; }

        [Column("week_start")]
        public DateTime? WeekStart { get; set; }

        [Column("week_end")]
        public DateTime? WeekEnd { get; set; }
    }
}
