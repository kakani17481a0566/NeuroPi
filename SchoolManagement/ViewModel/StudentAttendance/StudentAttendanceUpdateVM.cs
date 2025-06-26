namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceUpdateVM
    {
        public int StudentId { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
        public int? BranchId { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedOn { get; set; }

    }
}
