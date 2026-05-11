using SchoolManagement.ViewModel.QuestionCategorySubcategory;

namespace SchoolManagement.Services.Interface
{
    public interface IQuestionCategorySubcategoryService
    {
        List<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategories();

        QuestionCategorySubcategoryResponseVM GetQuestionCategorySubcategoryById(int id);

        List<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategoryByTenantId(int tenantId);

        QuestionCategorySubcategoryResponseVM GetQuestionCategorySubcategoryByIdAndTenantId(int id, int tenantId);

        QuestionCategorySubcategoryResponseVM CreateQuestionCategorySubcategory(QuestionCategorySubcategoryRequestVM request);

        QuestionCategorySubcategoryResponseVM UpdateQuestionCategorySubcategory(int id, int tenantId, QuestionCategorySubcategoryUpdateVM request);

        bool DeleteQuestionCategorySubcategory(int id, int tenantId);
    }
}
