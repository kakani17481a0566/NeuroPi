using SchoolManagement.Model;
using System;

namespace SchoolManagement.ViewModel.VwComprehensiveTimeTables
{
    public class VwComprehensiveTimeTableVM
    {
        public DateTime ScheduleDate { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public string PeriodName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string SubjectCode { get; set; } = string.Empty;
        public string? TopicName { get; set; }
        public string? TopicCode { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string? AssignedWorksheetName { get; set; }

        public static VwComprehensiveTimeTableVM FromModel(MVwComprehensiveTimeTable model)
        {
            return new VwComprehensiveTimeTableVM
            {
                ScheduleDate = model.ScheduleDate,
                DayOfWeek = model.DayOfWeek,
                PeriodName = model.PeriodName,
                SubjectName = model.SubjectName,
                SubjectCode = model.SubjectCode,
                TopicName = model.TopicName,
                TopicCode = model.TopicCode,
                CourseName = model.CourseName,
                AssignedWorksheetName = model.AssignedWorksheetName
            };
        }
    }
}
