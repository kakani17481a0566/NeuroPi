using SchoolManagement.ViewModel.DailyAssessment;
using System.Collections.Generic;


// Developed by: Mohith
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

        bool Delete(int id, int tenantId);
    }
}
