using SchoolManagement.ViewModel.TimeTableAssessment;

namespace SchoolManagement.Services.Interface
{
    public interface ITimeTableAssessmentService
    {
        List<TimeTableAssessmentResponseVM> GetAllTimeTableAssessments();

        List<TimeTableAssessmentResponseVM> GetTimeTableAssessmentsByTenantId(int tenantId);

        TimeTableAssessmentResponseVM GetTimeTableAssessmentById(int id);

        TimeTableAssessmentResponseVM GetTimeTableAssessmentByTenantIdAndId(int tenantId, int id);

        TimeTableAssessmentResponseVM AddTimeTableAssessment(TimeTableAssessmentRequestVM timeTableAssessment);

        TimeTableAssessmentResponseVM UpdateTimeTableAssessment(int id,int tenantId, TimeTableAssessmentUpdateVM timeTableAssessment);

        bool DeleteTimeTableAssessment(int id, int tenantId);
    }
}
