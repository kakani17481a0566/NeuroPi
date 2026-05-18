using SchoolManagement.ViewModel.StudyCourses;

namespace SchoolManagement.Services.Interface
{
    public interface IStudyCoursesService
    {
        StudyCoursesVm CreateStudyCourse(StudyCoursesCreateVm vm);
        StudyCoursesVm UpdateStudyCourse(int id, int tenantId, StudyCoursesUpdateVm vm);
        bool DeleteStudyCourse(int id, int tenantId);
        List<StudyCoursesVm> GetAllStudyCourses(int tenantId);
        StudyCoursesVm GetStudyCourseById(int id, int tenantId);
    }
}
