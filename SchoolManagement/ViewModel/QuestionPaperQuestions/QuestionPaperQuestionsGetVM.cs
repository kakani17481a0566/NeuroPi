using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionPaperQuestions
{
    public class QuestionPaperQuestionsGetVM
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PaperId { get; set; }
        public int QuestionId { get; set; }
        public int Sq { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static QuestionPaperQuestionsGetVM ToViewModel(MQuestionPaperQuestion model)
        {
            if (model == null) return null;

            return new QuestionPaperQuestionsGetVM
            {
                Id = model.Id,
                TenantId = model.TenantId,
                PaperId = model.PaperId,
                QuestionId = model.QuestionId,
                Sq = model.Sq,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<QuestionPaperQuestionsGetVM> ToViewModelList(List<MQuestionPaperQuestion> models)
        {
            if (models == null || models.Count == 0) return null;

            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
