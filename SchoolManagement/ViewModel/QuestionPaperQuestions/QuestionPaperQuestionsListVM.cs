using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionPaperQuestions
{
    public class QuestionPaperQuestionsListVM
    {
        public int Id { get; set; }
        public int PaperId { get; set; }
        public int QuestionId { get; set; }
        public int Sq { get; set; }

        public static QuestionPaperQuestionsListVM ToViewModel(MQuestionPaperQuestion model)
        {
            if (model == null) return null;

            return new QuestionPaperQuestionsListVM
            {
                Id = model.Id,
                PaperId = model.PaperId,
                QuestionId = model.QuestionId,
                Sq = model.Sq
            };
        }

        public static List<QuestionPaperQuestionsListVM> ToViewModelList(List<MQuestionPaperQuestion> models)
        {
            if (models == null || models.Count == 0) return null;

            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
