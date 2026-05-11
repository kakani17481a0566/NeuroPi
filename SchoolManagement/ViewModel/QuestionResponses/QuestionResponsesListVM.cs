using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionResponses
{
    public class QuestionResponsesListVM
    {
        public int Id { get; set; }
        public int PaperId { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string? ResponseText { get; set; }

        public static QuestionResponsesListVM ToViewModel(MQuestionResponse model)
        {
            if (model == null) return null;

            return new QuestionResponsesListVM
            {
                Id = model.Id,
                PaperId = model.PaperId,
                CandidateId = model.CandidateId,
                QuestionId = model.QuestionId,
                ResponseText = model.ResponseText
            };
        }

        public static List<QuestionResponsesListVM> ToViewModelList(List<MQuestionResponse> models)
        {
            if (models == null || models.Count == 0) return null;

            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
