using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionPaperQuestions;

namespace SchoolManagement.Services.Implementation
{
    public class QuestionPaperQuestionsService : IQuestionPaperQuestionsService
    {
        private readonly SchoolManagementDb _context;

        public QuestionPaperQuestionsService(SchoolManagementDb context)
        {
            _context = context;
        }

        public async Task<QuestionPaperQuestionsGetVM> Create(QuestionPaperQuestionsCreateVM request)
        {
            var model = QuestionPaperQuestionsCreateVM.ToModel(request);
            _context.QuestionPaperQuestions.Add(model);
            await _context.SaveChangesAsync();

            return QuestionPaperQuestionsGetVM.ToViewModel(model);
        }

        public async Task<QuestionPaperQuestionsGetVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var model = await _context.QuestionPaperQuestions
                .FirstOrDefaultAsync(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            return QuestionPaperQuestionsGetVM.ToViewModel(model);
        }

        public async Task<List<QuestionPaperQuestionsListVM>> GetAllByTenantId(int tenantId)
        {
            var data = await _context.QuestionPaperQuestions
                .Where(r => r.TenantId == tenantId && !r.IsDeleted)
                .OrderBy(r => r.Sq)
                .ToListAsync();

            return QuestionPaperQuestionsListVM.ToViewModelList(data);
        }

        public async Task<QuestionPaperQuestionsGetVM> Update(int id, int tenantId, QuestionPaperQuestionsUpdateVM request)
        {
            var model = await _context.QuestionPaperQuestions
                .FirstOrDefaultAsync(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            if (model == null) return null;

            model.Sq = request.Sq;
            model.UpdatedBy = request.UpdatedBy;
            model.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return QuestionPaperQuestionsGetVM.ToViewModel(model);
        }

        public async Task<bool> Delete(int id, int tenantId)
        {
            var model = await _context.QuestionPaperQuestions
                .FirstOrDefaultAsync(r => r.Id == id && r.TenantId == tenantId && !r.IsDeleted);

            if (model == null) return false;

            model.IsDeleted = true;
            model.UpdatedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<AssessmentSubdomainVM>> GetAssessmentByTenantId(int tenantId)
        {
            var questions = await _context.QuestionsSymmetric
                .Where(q => q.TenantId == tenantId && q.IsActive)
                .OrderBy(q => q.CategoryId)
                .ThenBy(q => q.SubcategoryId)
                .ThenBy(q => q.Id)
                .ToListAsync();

            var questionIds = questions.Select(q => q.Id).ToList();
            var options = await _context.QuestionOptions
                .Where(o => questionIds.Contains(o.QuestionId) && !o.IsDeleted)
                .OrderBy(o => o.QuestionId)
                .ThenBy(o => o.Sq)
                .ToListAsync();

            var optionsByQuestion = options.GroupBy(o => o.QuestionId)
                .ToDictionary(g => g.Key, g => g.Select(o => o.OptionText).ToList());

            var questionTypeIds = questions.Select(q => q.QuestionTypeId).Distinct().ToList();
            var questionTypes = await _context.Set<MMaster>()
                .Where(m => questionTypeIds.Contains(m.Id) && !m.IsDeleted)
                .ToListAsync();
            var questionTypeNames = questionTypes.ToDictionary(m => m.Id, m => m.Name);
            var likertTypeIds = questionTypes
                .Where(m => m.Name != null && m.Name.Contains("Likert", StringComparison.OrdinalIgnoreCase))
                .Select(m => m.Id)
                .ToHashSet();

            var likertOptions = await _context.Set<MMaster>()
                .Where(m => m.MasterTypeId == 68 && !m.IsDeleted)
                .OrderBy(m => m.Id)
                .Select(m => m.Name)
                .Distinct()
                .ToListAsync();

            var categories = await _context.QuestionCategories
                .Where(c => c.TenantId == tenantId && !c.IsDeleted && c.IsActive)
                .ToListAsync();

            var subcategories = await _context.QuestionCategorySubcategories
                .Where(s => s.TenantId == tenantId && !s.IsDeleted && s.IsActive)
                .ToListAsync();

            var result = new List<AssessmentSubdomainVM>();

            foreach (var category in categories)
            {
                var subs = subcategories.Where(s => s.CategoryId == category.Id).ToList();

                foreach (var sub in subs)
                {
                    var subQuestions = questions
                        .Where(q => q.CategoryId == category.Id && q.SubcategoryId == sub.Id)
                        .Select(q =>
                        {
                            var isLikert = likertTypeIds.Contains(q.QuestionTypeId);
                            return new AssessmentQuestionVM
                            {
                                Id = q.Id,
                                Qid = q.QId ?? $"Q{q.Id:D4}",
                                Qname = q.QuestionText,
                                QuestionTypeId = q.QuestionTypeId,
                                QuestionTypeName = questionTypeNames.ContainsKey(q.QuestionTypeId)
                                    ? questionTypeNames[q.QuestionTypeId]
                                    : null,
                                Qoptions = isLikert
                                    ? likertOptions
                                    : (optionsByQuestion.ContainsKey(q.Id)
                                        ? optionsByQuestion[q.Id]
                                        : new List<string>())
                            };
                        })
                        .ToList();

                    if (subQuestions.Count == 0)
                        continue;

                    result.Add(new AssessmentSubdomainVM
                    {
                        Domainname = category.CategoryName,
                        Subdomainname = sub.SubcategoryName,
                        Questions = subQuestions
                    });
                }
            }

            return result;
        }
    }
}
