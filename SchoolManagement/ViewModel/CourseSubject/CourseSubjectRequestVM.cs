namespace SchoolManagement.ViewModel.CourseSubject
{
    public class CourseSubjectRequestVM
    {
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int TenantId { get; set; }

        public Model.MCourseSubject ToModel()
        {
            return new Model.MCourseSubject
            {
                CourseId = this.CourseId,
                SubjectId = this.SubjectId,
                TenantId = this.TenantId,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
