using SchoolManagement.ViewModel.QuestionResponses;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionResponsesService
    {
        List<QuestionResponsesListVM> GetAllByTenantId(int tenantId);
        QuestionResponsesGetVM GetByIdAndTenantId(int id, int tenantId);
        QuestionResponsesGetVM Create(QuestionResponsesCreateVM request);
        List<QuestionResponsesGetVM> CreateBatch(QuestionResponsesBatchRequestVM request);
        QuestionResponsesGetVM Update(int id, int tenantId, QuestionResponsesUpdateVM request);
        bool Delete(int id, int tenantId);
    }
}
