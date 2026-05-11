using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.QuestionResponses
{
    public class QuestionResponsesCreateVM
    {
        public int TenantId { get; set; }
        public int PaperId { get; set; }
        public int CandidateId { get; set; }
        public int QuestionId { get; set; }
        public string? ResponseText { get; set; }

        public static MQuestionResponse ToModel(QuestionResponsesCreateVM vm)
        {
            return new MQuestionResponse
            {
                TenantId = vm.TenantId,
                PaperId = vm.PaperId,
                CandidateId = vm.CandidateId,
                QuestionId = vm.QuestionId,
                ResponseText = vm.ResponseText
            };
        }
    }
}
