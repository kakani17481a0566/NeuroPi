using SchoolManagement.ViewModel.CourseTeacher;

namespace SchoolManagement.Services.Interface
{
    public interface ICourseTeacherService
    {
        List<CourseTeacherVM> GetCoursesByTeacherId(int teacherId, int tenantId);
        CourseTeacherVM AssignCourseToTeacher(AssignCourseTeacherVM model);
        bool RemoveCourseFromTeacher(int id, int tenantId);
    }
}
