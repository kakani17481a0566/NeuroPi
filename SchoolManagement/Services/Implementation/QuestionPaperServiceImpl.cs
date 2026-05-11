using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionPaper;
using SchoolManagement.ViewModel.QuestionPaper;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionPaperServiceImpl : IQuestionPaperService
    {
        private readonly SchoolManagementDb _dbContext;
        public QuestionPaperServiceImpl(SchoolManagementDb dbContext)
        {
            _dbContext = dbContext;
        }
        public QuestionPaperResponseVM CreateQuestionPaper(QuestionPaperRequestVM request)
        {
            var questionPaper = QuestionPaperRequestVM.ToModel(request);
            _dbContext.QuestionPapers.Add(questionPaper);
            _dbContext.SaveChanges();
            return QuestionPaperResponseVM.ToViewModel(questionPaper);


        }

        public bool DeleteQuestionPaper(int id, int tenantId)
        {
            var QuestionPaper = _dbContext.QuestionPapers.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId);
            if (QuestionPaper == null)
            {
                return false;
            }
            QuestionPaper.IsActive = false;
            QuestionPaper.UpdatedOn = DateTime.UtcNow;
            _dbContext.SaveChanges();
            return true;

        }

        public List<QuestionPaperResponseVM> GetQuestionPapers()
        {
            var QuestionPapers = _dbContext.QuestionPapers.Where(q => q.IsActive).ToList();
            return QuestionPaperResponseVM.ToViewModelList(QuestionPapers);

        }

        public QuestionPaperResponseVM GetQuestionPaperById(int id)
        {
            var QuestionPaper = _dbContext.QuestionPapers.FirstOrDefault(q => q.Id == id && q.IsActive);
            if (QuestionPaper == null)
            {
                return null;
            }
            return QuestionPaperResponseVM.ToViewModel(QuestionPaper);
        }

        public QuestionPaperResponseVM GetQuestionPaperByIdAndTenantId(int id, int tenantId)
        {
            var QuestionPaper = _dbContext.QuestionPapers.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (QuestionPaper == null)
            {
                return null;
            }
            return QuestionPaperResponseVM.ToViewModel(QuestionPaper);
        }

        public List<QuestionPaperResponseVM> GetQuestionPaperByTenantId(int tenantId)
        {
            var QuestionPapers = _dbContext.QuestionPapers.Where(q => q.TenantId == tenantId && q.IsActive).ToList();
            if (QuestionPapers == null || !QuestionPapers.Any())
            {
                return null;
            }
            return QuestionPaperResponseVM.ToViewModelList(QuestionPapers);
        }

        public QuestionPaperResponseVM UpdateQuestionPaper(int id, int tenantId, QuestionPaperUpdateVM request)
        {
            var QuestionPaper = _dbContext.QuestionPapers.FirstOrDefault(q => q.Id == id && q.TenantId == tenantId && q.IsActive);
            if (QuestionPaper == null)
            {
                return null;
            }
            QuestionPaper.PaperName = request.PaperName;
            QuestionPaper.Description = request.Description;
            QuestionPaper.IsActive = request.IsActive;
            QuestionPaper.UpdatedBy = request.UpdatedBy;
            QuestionPaper.UpdatedOn = request.UpdatedOn ?? DateTime.UtcNow;
            _dbContext.SaveChanges();
            return QuestionPaperResponseVM.ToViewModel(QuestionPaper);



        }
    }
}
