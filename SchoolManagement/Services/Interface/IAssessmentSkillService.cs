using SchoolManagement.ViewModel.AssessmentSkills;

namespace SchoolManagement.Services.Interface
{
    public interface IAssessmentSkillService
    {
        List<AssessmentSkillsResponseVM> GetAllSkills();

        List<AssessmentSkillsResponseVM> GetSkillsByTenantId(int tenantId);

        AssessmentSkillsResponseVM GetSkillById(int id);

        AssessmentSkillsResponseVM GetSkillByIdAndTenantId(int id, int tenantId);

        AssessmentSkillsResponseVM CreateAssessmentSkill(AssessmentSkillsRequestVM skill);

        AssessmentSkillsResponseVM UpdateAssessmentSkill(int id, int tenantId, AssessmentSkillsUpdateVM skill);
        bool DeleteAssessmentSkillByIdAndTenantId(int id, int tenantId);
    }
}
