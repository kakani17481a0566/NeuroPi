using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Course
{
    public class CourseVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }

        public static CourseVm ToViewModel(MCourse course)
        {
            return new CourseVm
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                TenantId = course.TenantId,
            };
        }
        public static List<CourseVm> ToViewModelList(List<MCourse> courses)
        {
            return courses.Select(c=>ToViewModel(c)).ToList();
        }
    }
}
