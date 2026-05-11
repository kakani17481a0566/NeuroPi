using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionPaper
{
    public class QuestionPaperResponseVM
    {
        public int Id { get; set; }

        public string PaperName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static QuestionPaperResponseVM ToViewModel(MQuestionPaper quesionPaper)
        {
            return new QuestionPaperResponseVM
            {
                Id = quesionPaper.Id,
                PaperName = quesionPaper.PaperName,
                Description = quesionPaper.Description,
                IsActive = quesionPaper.IsActive,
                TenantId = quesionPaper.TenantId,
                CreatedBy = quesionPaper.CreatedBy,
                CreatedOn = quesionPaper.CreatedOn,
                UpdatedBy = quesionPaper.UpdatedBy,
                UpdatedOn = quesionPaper.UpdatedOn
            };
        }

        public static List<QuestionPaperResponseVM> ToViewModelList(List<MQuestionPaper> questionPapers)
        {
            return questionPapers.Select(q => ToViewModel(q)).ToList();
        }
    }
}