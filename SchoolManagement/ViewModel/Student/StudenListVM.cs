namespace SchoolManagement.ViewModel.Student
{
  
    public class StudentListVM
    {
        public int Id { get; set; }               // Student ID
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string CourseName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
    }
}
