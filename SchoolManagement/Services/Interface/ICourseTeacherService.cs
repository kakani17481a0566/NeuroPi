using SchoolManagement.ViewModel.CourseTeacher;

namespace SchoolManagement.Services.Interface
{
    public interface ICourseTeacherService
    {
        List<CourseTeacherResponseVM> GetAllCourseTeachers();
        List<CourseTeacherResponseVM> GetCourseTeachersByTenant(int tenantId);
        CourseTeacherResponseVM GetCourseTeacherById(int id);
        CourseTeacherResponseVM GetCourseTeacherByIdAndTenant(int id, int tenantId);

        CourseTeacherResponseVM CreateCourseTeacher(CourseTeacherRequestVM courseTeacherRequestVM);

        CourseTeacherResponseVM UpdateCourseTeacher(int id, int tenantId, CourseTeacherUpdateVM courseTeacherUpdateVM);
        bool DeleteCourseTeacherByIdAndTenant(int id, int tenantId);

    }
}
