using SchoolManagement.ViewModel.Assessment;

namespace SchoolManagement.Services.Interface
{
    public interface IAssessmentService
    {
        List<AssessmentResponseVM> GetAllAssessments();

        List<AssessmentResponseVM> GetAssessmentsByTenantId(int tenantId);

        AssessmentResponseVM GetAssessmentById(int id);

        AssessmentResponseVM GetAssessmentByIdAndTenantId(int id, int tenantId);

        AssessmentResponseVM CreateAssessment(AssessmentRequestVM assessment);
        AssessmentResponseVM UpdateAssessment(int id, int tenantId, AssessmentUpdateVM assessment);
        bool DeleteAssessmentByIdAndTenantId(int id, int tenantId);
    }
}
