using SchoolManagement.ViewModel.QuestionPaper;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionPaperService
    {
        List<QuestionPaperResponseVM> GetQuestionPapers();

        QuestionPaperResponseVM GetQuestionPaperById(int id);

        List<QuestionPaperResponseVM> GetQuestionPaperByTenantId(int tenantId);

        QuestionPaperResponseVM GetQuestionPaperByIdAndTenantId(int id, int tenantId);

        QuestionPaperResponseVM CreateQuestionPaper(QuestionPaperRequestVM request);

        QuestionPaperResponseVM UpdateQuestionPaper(int id, int tenantId, QuestionPaperUpdateVM request);

        bool DeleteQuestionPaper(int id, int tenantId);
    }
}
