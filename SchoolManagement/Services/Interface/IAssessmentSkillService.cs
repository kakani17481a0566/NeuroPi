using SchoolManagement.ViewModel.Assessment;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IAssessmentSkillService
    {
        List<AssessmentSkillResponseVM> GetAll(int tenantId);
        AssessmentSkillResponseVM GetById(int id, int tenantId);
        List<AssessmentSkillResponseVM> GetBySubjectId(int subjectId, int tenantId);
        AssessmentSkillResponseVM Create(AssessmentSkillRequestVM request);
        AssessmentSkillResponseVM Update(int id, int tenantId, AssessmentSkillUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
