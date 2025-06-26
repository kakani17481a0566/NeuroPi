namespace SchoolManagement.ViewModel.Student
{
    public class StudentVM
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }  
        public int TenantId { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }

        public List<Student> students { get; set; }
    }
    public class Student
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
