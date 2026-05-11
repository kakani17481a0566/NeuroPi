using SchoolManagement.ViewModel.QuestionOption;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionOptionService
    {
        List<QuestionOptionResponseVM> GetQuestionOptions();

        QuestionOptionResponseVM GetQuestionOptionById(int id);

        List<QuestionOptionResponseVM> GetQuestionOptionByTenantId(int tenantId);

        QuestionOptionResponseVM GetQuestionOptionByIdAndTenantId(int id, int tenantId);

        QuestionOptionResponseVM CreateQuestionOption(QuestionOptionRequestVM request);

        QuestionOptionResponseVM UpdateQuestionOption(int id, int tenantId, QuestionOptionUpdateVM request);

        bool DeleteQuestionOption(int id, int tenantId);
    }
}
