namespace SchoolManagement.ViewModel.Student
{
    public class StudentsData
    {
         public List<StudentDetails> students {  get; set; }

        public int totalStudents { get; set; }

        public int checkedIn { get; set; }

        public int checkedOut { get; set; }


    }
    public class StudentDetails
    {
        public string Name { get; set; }

        public DateOnly date { get; set; }

        public TimeSpan checkedIn { get; set; }

        public TimeSpan checkedOut { get; set; }

    }
}
