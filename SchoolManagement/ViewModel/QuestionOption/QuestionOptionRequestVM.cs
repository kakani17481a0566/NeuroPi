using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionOption
{
    public class QuestionOptionRequestVM
    {
        public int QuestionId { get; set; }

        public int Sq { get; set; }

        public string? OptionCode { get; set; }

        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public static MQuestionOption ToModel(QuestionOptionRequestVM request)
        {
            return new MQuestionOption
            {
                QuestionId = request.QuestionId,
                Sq = request.Sq,
                OptionCode = request.OptionCode,
                OptionText = request.OptionText,
                IsCorrect = request.IsCorrect,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = request.CreatedOn
            };
        }
    }
}
