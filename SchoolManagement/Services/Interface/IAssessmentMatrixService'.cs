using SchoolManagement.ViewModel.DailyAssessment;

namespace SchoolManagement.Services.Interface
{
    public interface IAssessmentMatrixService
    {
        AssessmentMatrixResponse GetMatrix(int timeTableId, int tenantId, int courseId, int branchId);
    }
}
