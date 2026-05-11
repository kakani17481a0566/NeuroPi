using SchoolManagement.Model;
using System.Text.Json.Serialization;

namespace SchoolManagement.ViewModel.QuestionResponses
{
    public class QuestionResponsesCreateVM
    {
        [JsonPropertyName("tenant_id")]
        public int TenantId { get; set; }

        [JsonPropertyName("paper_id")]
        public int PaperId { get; set; }

        [JsonPropertyName("candidate_id")]
        public int CandidateId { get; set; }

        [JsonPropertyName("question_id")]
        public int QuestionId { get; set; }

        [JsonPropertyName("response_text")]
        public string? ResponseText { get; set; }

        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }

        public static MQuestionResponse ToModel(QuestionResponsesCreateVM vm)
        {
            return new MQuestionResponse
            {
                TenantId = vm.TenantId,
                PaperId = vm.PaperId,
                CandidateId = vm.CandidateId,
                QuestionId = vm.QuestionId,
                ResponseText = vm.ResponseText,
                CreatedBy = vm.CreatedBy ?? 0
            };
        }
    }
}
