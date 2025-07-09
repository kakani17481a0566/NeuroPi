namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceSummaryVm
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }

        public int CourseId { get; set; }

        public int? AttendanceId { get; set; }
        public string AttendanceDate { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public string MarkedBy { get; set; }
        public string MarkedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string AttendanceStatus { get; set; }
    }

    public class CourseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class StudentAttendanceStructuredSummaryVm
    {
        public List<CourseVm> Courses { get; set; }
        public List<StudentAttendanceSummaryVm> Records { get; set; }
    }
}
