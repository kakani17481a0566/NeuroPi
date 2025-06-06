namespace SchoolManagement.ViewModel.VwComprehensiveTimeTables
{
    public class VwComprehensiveTimeTableVM
    {
        public DateTime ScheduleDate { get; set; }
        public string DayOfWeek { get; set; }
        public string PeriodName { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string? TopicName { get; set; }
        public string? TopicCode { get; set; }
        public string CourseName { get; set; }
        public string? AssignedWorksheetName { get; set; }
    }
}
