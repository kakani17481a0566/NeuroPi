using SchoolManagement.ViewModel.StudyPlanSteps;

namespace SchoolManagement.Services.Interface
{
    public interface IStudyPlanStepsService
    {
        StudyPlanStepsVm CreateStudyPlanStep(StudyPlanStepsCreateVm vm);
        StudyPlanStepsVm UpdateStudyPlanStep(int id, int tenantId, StudyPlanStepsUpdateVm vm);
        bool DeleteStudyPlanStep(int id, int tenantId);
        List<StudyPlanStepsVm> GetAllStudyPlanSteps(int tenantId);
        StudyPlanStepsVm GetStudyPlanStepById(int id, int tenantId);
        List<StudyPlanStepsVm> GetStudyPlanStepsByPlanId(int studyPlanId, int tenantId);
    }
}
