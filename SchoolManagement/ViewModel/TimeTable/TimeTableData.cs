namespace SchoolManagement.ViewModel.TimeTable
{
    public class TimeTableData
    {
        public Dictionary<string, string> Headers { get; set; } = new();
        public List<TDataTimeTable> TimeTableDataList { get; set; } = new();
        public FilterOptions Filters { get; set; } = new();
    }

    public class FilterOptions
    {
        public Dictionary<int, string> Weeks { get; set; } = new();
        public Dictionary<int, string> Courses { get; set; } = new();
        public Dictionary<int, string> AssessmentStatuses { get; set; } = new();
    }

    public class TDataTimeTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int? WeekId { get; set; }
        public string? WeekName { get; set; }

        public int? CourseId { get; set; }
        public string? CourseName { get; set; }

        public int? AssessmentStatusCode { get; set; }
        public string? AssessmentStatusName { get; set; }

        public int TenantId { get; set; }
        public string? TenantName { get; set; }
    }
}
