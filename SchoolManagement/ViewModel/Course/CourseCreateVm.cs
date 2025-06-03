namespace SchoolManagement.ViewModel.Course
{
    public class CourseCreateVm
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }


        public int CreatedBy { get; set; }
    }
}
