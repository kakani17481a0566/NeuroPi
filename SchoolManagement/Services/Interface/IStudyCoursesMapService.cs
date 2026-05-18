using SchoolManagement.ViewModel.StudyCoursesMap;

namespace SchoolManagement.Services.Interface
{
    public interface IStudyCoursesMapService
    {
        StudyCoursesMapVm CreateStudyCoursesMap(StudyCoursesMapCreateVm vm);
        StudyCoursesMapVm UpdateStudyCoursesMap(int id, int tenantId, StudyCoursesMapUpdateVm vm);
        bool DeleteStudyCoursesMap(int id, int tenantId);
        List<StudyCoursesMapVm> GetAllStudyCoursesMaps(int tenantId);
        StudyCoursesMapVm GetStudyCoursesMapById(int id, int tenantId);
        List<StudyCoursesMapVm> GetStudyCoursesMapsByPlanId(int studyPlanId, int tenantId);
    }
}
