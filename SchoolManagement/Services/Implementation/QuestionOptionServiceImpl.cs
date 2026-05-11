using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionOption;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionOptionServiceImpl : IQuestionOptionService
    {
        private readonly SchoolManagementDb _dbContext;
        public QuestionOptionServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public QuestionOptionResponseVM CreateQuestionOption(QuestionOptionRequestVM request)
        {
            var QuestionOption = QuestionOptionRequestVM.ToModel(request);
            _dbContext.QuestionOptions.Add(QuestionOption);
            _dbContext.SaveChanges();
            return QuestionOptionResponseVM.ToViewModel(QuestionOption);

        }

        public bool DeleteQuestionOption(int id, int tenantId)
        {
            var QuestionOption = _dbContext.QuestionOptions.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId);
            if (QuestionOption == null)
            {
                return false;
            }
            QuestionOption.IsDeleted = false;
            QuestionOption.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;

        }

        public List<QuestionOptionResponseVM> GetQuestionOptions()
        {
            var QuestionOptions = _dbContext.QuestionOptions.Where(q => !q.IsDeleted).ToList();
            return QuestionOptionResponseVM.ToViewModelList(QuestionOptions);

        }

        public QuestionOptionResponseVM GetQuestionOptionById(int id)
        {
            var QuestionOption = _dbContext.QuestionOptions.FirstOrDefault(q => q.Id == id && !q.IsDeleted);
            if (QuestionOption == null)
            {
                return null;
            }
            return QuestionOptionResponseVM.ToViewModel(QuestionOption);
        }

        public QuestionOptionResponseVM GetQuestionOptionByIdAndTenantId(int id, int tenantId)
        {
            var QuestionOption = _dbContext.QuestionOptions.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && !q.IsDeleted);
            if (QuestionOption == null)
            {
                return null;
            }
            return QuestionOptionResponseVM.ToViewModel(QuestionOption);
        }

        public List<QuestionOptionResponseVM> GetQuestionOptionByTenantId(int tenantId)
        {
            var QuestionOptions = _dbContext.QuestionOptions.Where(q => q.TenantId == tenantId && !q.IsDeleted).ToList();
            if (QuestionOptions == null || !QuestionOptions.Any())
            {
                return null;
            }
            return QuestionOptionResponseVM.ToViewModelList(QuestionOptions);
        }

        public QuestionOptionResponseVM UpdateQuestionOption(int id, int tenantId, QuestionOptionUpdateVM request)
        {
            var QuestionOption = _dbContext.QuestionOptions.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && !q.IsDeleted);
            if (QuestionOption == null)
            {
                return null;
            }
            QuestionOption.QuestionId = request.QuestionId;
            QuestionOption.Sq = request.Sq;
            QuestionOption.OptionCode = request.OptionCode;
            QuestionOption.OptionText = request.OptionText;
            QuestionOption.IsCorrect = request.IsCorrect;
            QuestionOption.UpdatedBy = request.UpdatedBy;
            QuestionOption.UpdatedOn = request.UpdatedOn ?? DateTime.UtcNow;
            _dbContext.SaveChanges();   
            return QuestionOptionResponseVM.ToViewModel(QuestionOption);

        }
    }
}
