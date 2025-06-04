namespace SchoolManagement.ViewModel.CourseSubject
{
    public class CourseSubjectResponseVM
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public int TenantId { get; set; }

        public static CourseSubjectResponseVM FromModel(Model.MCourseSubject model)
        {
            if (model == null) return null;

            return new CourseSubjectResponseVM
            {
                Id = model.Id,
                CourseId = model.CourseId,
                SubjectId = model.SubjectId,
                TenantId = model.TenantId
            };
        }
    }
}
