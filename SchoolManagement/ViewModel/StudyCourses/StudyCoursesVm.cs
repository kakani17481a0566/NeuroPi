using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.StudyCourses
{
    public class StudyCoursesVm
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public int TenantId { get; set; }

        public static StudyCoursesVm ToViewModel(MStudyCourses model)
        {
            return new StudyCoursesVm
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Url = model.Url,
                TenantId = model.TenantId
            };
        }

        public static List<StudyCoursesVm> ToViewModelList(List<MStudyCourses> models)
        {
            return models.Select(ToViewModel).ToList();
        }
    }
}
