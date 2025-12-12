using SchoolManagement.ViewModel.Assessment;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IAssessmentService
    {
        List<AssessmentResponseVM> GetAll(int tenantId);
        AssessmentResponseVM GetById(int id, int tenantId);
        List<AssessmentResponseVM> GetBySkillId(int skillId, int tenantId);
        AssessmentResponseVM Create(AssessmentRequestVM request);
        AssessmentResponseVM Update(int id, int tenantId, AssessmentUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
