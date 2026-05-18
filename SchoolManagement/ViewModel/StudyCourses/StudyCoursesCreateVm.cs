namespace SchoolManagement.ViewModel.StudyCourses
{
    public class StudyCoursesCreateVm
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
    }
}
