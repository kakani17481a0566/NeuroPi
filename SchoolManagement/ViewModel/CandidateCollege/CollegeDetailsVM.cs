namespace SchoolManagement.ViewModel.CandidateCollege
{
    public class CollegeDetailsVM
    {

        public int CollegeId { get; set; }

        public string  CollegeName { get; set; }

        public List<Course> coursesList {  get; set; }
    }
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; }
    }
}
