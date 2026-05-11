using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionCategory;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionCategoryServiceImpl : IQuestionCategoryService
    {
        private readonly SchoolManagementDb _dbContext;
        public QuestionCategoryServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public QuestionCategoryResponseVM CreateQuestionCategory(QuestionCategoryRequestVM request)
        {
            var questionCategory = QuestionCategoryRequestVM.ToModel(request);
            _dbContext.QuestionCategories.Add(questionCategory);
            _dbContext.SaveChanges();
            return QuestionCategoryResponseVM.ToViewModel(questionCategory);

        }

        public bool DeleteQuestionCategory(int id, int tenantId)
        {
            var questionCategory = _dbContext.QuestionCategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId);
            if (questionCategory == null)
            {
                return false;
            }
            questionCategory.IsActive = false;
            questionCategory.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;

        }

        public List<QuestionCategoryResponseVM> GetQuestionCategories()
        {
            var questionCategories = _dbContext.QuestionCategories.Where(q => q.IsActive).ToList();
            return QuestionCategoryResponseVM.ToViewModelList(questionCategories);

        }

        public QuestionCategoryResponseVM GetQuestionCategoryById(int id)
        {
            var questionCategory = _dbContext.QuestionCategories.FirstOrDefault(q => q.Id == id && q.IsActive);
            if (questionCategory == null)
            {
                return null;
            }
            return QuestionCategoryResponseVM.ToViewModel(questionCategory);
        }

        public QuestionCategoryResponseVM GetQuestionCategoryByIdAndTenantId(int id, int tenantId)
        {
            var questionCategory = _dbContext.QuestionCategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (questionCategory == null)
            {
                return null;
            }
            return QuestionCategoryResponseVM.ToViewModel(questionCategory);
        }

        public List<QuestionCategoryResponseVM> GetQuestionCategoryByTenantId(int tenantId)
        {
            var questionCategories = _dbContext.QuestionCategories.Where(q => q.TenantId == tenantId && q.IsActive).ToList();
            if (questionCategories == null || !questionCategories.Any())
            {
                return null;
            }
            return QuestionCategoryResponseVM.ToViewModelList(questionCategories);
        }

        public QuestionCategoryResponseVM UpdateQuestionCategory(int id, int tenantId, QuestionCategoryUpdateVM request)
        {
            var questionCategory = _dbContext.QuestionCategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (questionCategory == null)
            {
                return null;
            }
            questionCategory.CategoryName = request.CategoryName;
            questionCategory.Description = request.Description;
            questionCategory.Code = request.Code;
            questionCategory.IsActive = request.IsActive;
            questionCategory.UpdatedBy = request.UpdatedBy;
            questionCategory.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return QuestionCategoryResponseVM.ToViewModel(questionCategory);
        }
    }
}
