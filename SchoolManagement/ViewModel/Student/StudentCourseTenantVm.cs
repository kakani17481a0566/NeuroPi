namespace SchoolManagement.ViewModel.Student
{
    public class StudentCourseTenantVm
    {
        public int Id { get; set; }                 // s.id
        public string Name { get; set; } = string.Empty;  // s.name

        public int CourseId { get; set; }           // s.course_id
        public string CourseName { get; set; } = string.Empty; // c.name

        public int TenantId { get; set; }           // s.tenant_id
        public string TenantName { get; set; } = string.Empty; // t.name
    }
}
