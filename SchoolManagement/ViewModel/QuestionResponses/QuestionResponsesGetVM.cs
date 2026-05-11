using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionResponses
{
    public class QuestionResponsesGetVM
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int PaperId { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string? ResponseText { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static QuestionResponsesGetVM ToViewModel(MQuestionResponse model)
        {
            if (model == null) return null;

            return new QuestionResponsesGetVM
            {
                Id = model.Id,
                TenantId = model.TenantId,
                PaperId = model.PaperId,
                CandidateId = model.CandidateId,
                QuestionId = model.QuestionId,
                ResponseText = model.ResponseText,
                CreatedBy = model.CreatedBy,
                CreatedOn = model.CreatedOn,
                UpdatedBy = model.UpdatedBy,
                UpdatedOn = model.UpdatedOn
            };
        }
    }
}
