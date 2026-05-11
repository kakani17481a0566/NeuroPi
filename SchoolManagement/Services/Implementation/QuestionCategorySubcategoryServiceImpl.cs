using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionCategorySubcategory;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionCategorySubcategoryServiceImpl : IQuestionCategorySubcategoryService
    {
        private readonly SchoolManagementDb _dbContext;
        public QuestionCategorySubcategoryServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public QuestionCategorySubcategoryResponseVM CreateQuestionCategorySubcategory(QuestionCategorySubcategoryRequestVM request)
        {
            var QuestionCategorySubcategory = QuestionCategorySubcategoryRequestVM.ToModel(request);
            _dbContext.QuestionCategorySubcategories.Add(QuestionCategorySubcategory);
            _dbContext.SaveChanges();
            return QuestionCategorySubcategoryResponseVM.ToViewModel(QuestionCategorySubcategory);

        }

        public bool DeleteQuestionCategorySubcategory(int id, int tenantId)
        {
            var QuestionCategorySubcategory = _dbContext.QuestionCategorySubcategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId);
            if (QuestionCategorySubcategory == null)
            {
                return false;
            }
            QuestionCategorySubcategory.IsActive = false;
            QuestionCategorySubcategory.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;

        }

        public List<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategories()
        {
            var questionCategoriesSubcategories = _dbContext.QuestionCategorySubcategories.Where(q => q.IsActive).ToList();
            return QuestionCategorySubcategoryResponseVM.ToViewModelList(questionCategoriesSubcategories);

        }

        public QuestionCategorySubcategoryResponseVM GetQuestionCategorySubcategoryById(int id)
        {
            var QuestionCategorySubcategory = _dbContext.QuestionCategorySubcategories.FirstOrDefault(q => q.Id == id && q.IsActive);
            if (QuestionCategorySubcategory == null)
            {
                return null;
            }
            return QuestionCategorySubcategoryResponseVM.ToViewModel(QuestionCategorySubcategory);
        }

        public QuestionCategorySubcategoryResponseVM GetQuestionCategorySubcategoryByIdAndTenantId(int id, int tenantId)
        {
            var QuestionCategorySubcategory = _dbContext.QuestionCategorySubcategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (QuestionCategorySubcategory == null)
            {
                return null;
            }
            return QuestionCategorySubcategoryResponseVM.ToViewModel(QuestionCategorySubcategory);
        }

        public List<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategoryByTenantId(int tenantId)
        {
            var questionCategoriesSubcategories = _dbContext.QuestionCategorySubcategories.Where(q => q.TenantId == tenantId && q.IsActive).ToList();
            if (questionCategoriesSubcategories == null || !questionCategoriesSubcategories.Any())
            {
                return null;
            }
            return QuestionCategorySubcategoryResponseVM.ToViewModelList(questionCategoriesSubcategories);
        }

        public QuestionCategorySubcategoryResponseVM UpdateQuestionCategorySubcategory(int id, int tenantId, QuestionCategorySubcategoryUpdateVM request)
        {
            var QuestionCategorySubcategory = _dbContext.QuestionCategorySubcategories.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (QuestionCategorySubcategory == null)
            {
                return null;
            }
            QuestionCategorySubcategory.CategoryId = request.CategoryId;
            QuestionCategorySubcategory.SubcategoryName = request.SubcategoryName;
            QuestionCategorySubcategory.Code = request.Code;
            QuestionCategorySubcategory.Description = request.Description;
            QuestionCategorySubcategory.IsActive = request.IsActive;
            QuestionCategorySubcategory.UpdatedBy = request.UpdatedBy;
            QuestionCategorySubcategory.UpdatedOn = request.UpdatedOn ?? DateTime.UtcNow;
            _dbContext.SaveChanges();
            return QuestionCategorySubcategoryResponseVM.ToViewModel(QuestionCategorySubcategory);



        }
    }
}
