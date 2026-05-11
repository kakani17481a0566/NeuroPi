using SchoolManagement.ViewModel.QuestionPaperQuestions;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionPaperQuestionsService
    {
        Task<QuestionPaperQuestionsGetVM> Create(QuestionPaperQuestionsCreateVM request);
        Task<QuestionPaperQuestionsGetVM> GetByIdAndTenantId(int id, int tenantId);
        Task<List<QuestionPaperQuestionsListVM>> GetAllByTenantId(int tenantId);
        Task<QuestionPaperQuestionsGetVM> Update(int id, int tenantId, QuestionPaperQuestionsUpdateVM request);
        Task<bool> Delete(int id, int tenantId);
        Task<List<AssessmentSubdomainVM>> GetAssessmentByTenantId(int tenantId);
    }
}
