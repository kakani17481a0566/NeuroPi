namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceSummaryVm
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }

        public int CourseId { get; set; } // ✅ New

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
}
