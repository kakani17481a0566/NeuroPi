using SchoolManagement.ViewModel.StudyPlan;

namespace SchoolManagement.Services.Interface
{
    public interface IStudyPlanService
    {
        StudyPlanVm CreateStudyPlan(StudyPlanCreateVm vm);
        StudyPlanVm UpdateStudyPlan(int id, int tenantId, StudyPlanUpdateVm vm);
        bool DeleteStudyPlan(int id, int tenantId);
        List<StudyPlanVm> GetAllStudyPlans(int tenantId);
        StudyPlanVm GetStudyPlanById(int id, int tenantId);
    }
}
