namespace SchoolManagement.ViewModel.StudentAttendance
{
    public class StudentAttendanceGraphVM
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public string CourseName { get; set; }
        public string Date { get; set; } 
        public string InTime { get; set; } 
        public string OutTime { get; set; } 

        public string? TotalTime { get; set; }

        
    }

}
