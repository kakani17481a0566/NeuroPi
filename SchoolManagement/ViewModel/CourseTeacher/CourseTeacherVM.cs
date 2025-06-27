namespace SchoolManagement.ViewModel.CourseTeacher
{
    public class CourseTeacherVM
    {
        public int branchId { get; set; }

        public List<Course> courses { get; set; }

        public int weekId { get; set; }

        public int termId {  get; set; }


    }
    public class Course
    {
        public int id { get; set; }

        public string name { get; set; }
    }
}
