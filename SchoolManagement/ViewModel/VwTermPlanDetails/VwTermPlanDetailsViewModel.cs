using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.VwTermPlanDetails
{
    public class VwTermPlanDetailsViewModel
    {
        public string TermName { get; set; } = string.Empty;
        public DateTime TermStartDate { get; set; }
        public DateTime TermEndDate { get; set; }
        public string WeekPeriod { get; set; } = string.Empty;
        public int WeekId { get; set; }
        public string WeekLongName { get; set; } = string.Empty;
        public DateTime WeekStartDate { get; set; }
        public DateTime WeekEndDate { get; set; }
        public DateTime? ScheduleDate { get; set; }
        public string? DayOfWeek { get; set; }
        public string? PeriodName { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectCode { get; set; }
        public string? TopicName { get; set; }
        public string? TopicCode { get; set; }
        public string? TopicTypeName { get; set; }
        public string? AssignedWorksheetName { get; set; }
        public string? AssignedWorksheetDescription { get; set; }
        public string? AssignedWorksheetLocation { get; set; }
        public string? TeacherUsername { get; set; }
        public int? TeacherUserId { get; set; }
        public string? TeacherTeamName { get; set; }
        public int? TeacherTeamId { get; set; }
        public string? CourseName { get; set; }
        public int? CourseId { get; set; }
        public int TenantId { get; set; }

        public static VwTermPlanDetailsViewModel FromModel(MVwTermPlanDetails x)
        {
            return new VwTermPlanDetailsViewModel
            {
                TermName = x.TermName,
                TermStartDate = x.TermStartDate,
                TermEndDate = x.TermEndDate,
                WeekPeriod = x.WeekPeriod,
                WeekId = x.WeekId,
                WeekLongName = x.WeekLongName,
                WeekStartDate = x.WeekStartDate,
                WeekEndDate = x.WeekEndDate,
                ScheduleDate = x.ScheduleDate,
                DayOfWeek = x.DayOfWeek,
                PeriodName = x.PeriodName,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                SubjectName = x.SubjectName,
                SubjectCode = x.SubjectCode,
                TopicName = x.TopicName,
                TopicCode = x.TopicCode,
                TopicTypeName = x.TopicTypeName,
                AssignedWorksheetName = x.AssignedWorksheetName,
                AssignedWorksheetDescription = x.AssignedWorksheetDescription,
                AssignedWorksheetLocation = x.AssignedWorksheetLocation,
                TeacherUsername = x.TeacherUsername,
                TeacherUserId = x.TeacherUserId,
                TeacherTeamName = x.TeacherTeamName,
                TeacherTeamId = x.TeacherTeamId,
                CourseName = x.CourseName,
                CourseId = x.CourseId,
                TenantId = x.TenantId
            };
        }
    }
}
