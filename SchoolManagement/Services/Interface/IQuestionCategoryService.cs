using SchoolManagement.ViewModel.QuestionCategory;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionCategoryService
    {
        List<QuestionCategoryResponseVM> GetQuestionCategories();

        QuestionCategoryResponseVM GetQuestionCategoryById(int id);

        List<QuestionCategoryResponseVM> GetQuestionCategoryByTenantId(int tenantId);

        QuestionCategoryResponseVM GetQuestionCategoryByIdAndTenantId(int id, int tenantId);

        QuestionCategoryResponseVM CreateQuestionCategory(QuestionCategoryRequestVM request);

        QuestionCategoryResponseVM UpdateQuestionCategory(int id, int tenantId, QuestionCategoryUpdateVM request);

        bool DeleteQuestionCategory(int id, int tenantId);
    }
}
