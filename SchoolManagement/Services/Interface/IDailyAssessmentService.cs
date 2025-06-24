using SchoolManagement.ViewModel.DailyAssessment;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IDailyAssessmentService
    {
        List<DailyAssessmentResponseVm> GetAll();
        List<DailyAssessmentResponseVm> GetAllByTenant(int tenantId);
        DailyAssessmentResponseVm GetById(int id);
        DailyAssessmentResponseVm Create(DailyAssessmentRequestVm request);
        DailyAssessmentResponseVm GetById(int id, int tenantId);
        DailyAssessmentResponseVm Update(int id, int tenantId, DailyAssessmentUpdateVm request);
        UpdateGradeResponseVm UpdateStudentGrade(int id, int timeTableId, int studentId, int branchId, int newGradeId);
        bool Delete(int id, int tenantId);

        bool SaveAssessmentMatrix(SaveAssessmentMatrixRequestVm request);
    }
}
