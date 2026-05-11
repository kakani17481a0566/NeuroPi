using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionOption
{
    public class QuestionOptionResponseVM
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public int Sq { get; set; }

        public string? OptionCode { get; set; }

        public string OptionText { get; set; }

        public bool IsCorrect { get; set; }

        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public static QuestionOptionResponseVM ToViewModel(MQuestionOption mQuestion)
        {
            return new QuestionOptionResponseVM
            {
                Id = mQuestion.Id,
                QuestionId = mQuestion.QuestionId,
                Sq = mQuestion.Sq,
                OptionCode = mQuestion.OptionCode,
                OptionText = mQuestion.OptionText,
                IsCorrect = mQuestion.IsCorrect,
                TenantId = mQuestion.TenantId,
                CreatedBy = mQuestion.CreatedBy,
                CreatedOn = mQuestion.CreatedOn,
                UpdatedBy = mQuestion.UpdatedBy,
                UpdatedOn = mQuestion.UpdatedOn
            };
        }

        public static List<QuestionOptionResponseVM> ToViewModelList(List<MQuestionOption> mQuestions)
        {
            return mQuestions.Select(ToViewModel).ToList();
        }
    }
}
