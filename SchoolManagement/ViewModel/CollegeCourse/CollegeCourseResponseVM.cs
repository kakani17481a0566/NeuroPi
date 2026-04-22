namespace SchoolManagement.ViewModel.CollegeCourse
{
    public class CollegeCourseResponseVM
    {
        public int Id { get; set; }
        public int CollegeId { get; set; }
        public string CollegeName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int TenantId { get; set; }
    }
}
