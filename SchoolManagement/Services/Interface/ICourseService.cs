using SchoolManagement.ViewModel.Course;

namespace SchoolManagement.Services.Interface
{
    public interface ICourseService
    {
        List<CourseVM> GetCoursesByTenantId(int tenantId);
        CourseVM GetCourseById(int id, int tenantId);
    }
}
