using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionPaper
{
    public class QuestionPaperRequestVM
    {
        public string PaperName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MQuestionPaper ToModel(QuestionPaperRequestVM request)
        {
            return new MQuestionPaper                  
            {
                PaperName = request.PaperName,
                Description = request.Description,
                IsActive = request.IsActive,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };
        }
    }
}
