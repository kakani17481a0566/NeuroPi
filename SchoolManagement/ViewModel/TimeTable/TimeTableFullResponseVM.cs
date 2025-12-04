namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableFullResponseVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int? WeekId { get; set; }
        public string WeekName { get; set; }

        public int? HolidayId { get; set; }
        public string HolidayName { get; set; }

        public string Status { get; set; }

        public int? CourseId { get; set; }
        public string CourseName { get; set; }

        public int? AssessmentStatusCode { get; set; }
        public string AssessmentStatusName { get; set; }

        public int TenantId { get; set; }
    }
}
