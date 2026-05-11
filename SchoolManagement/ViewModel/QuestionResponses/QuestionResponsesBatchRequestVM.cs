using System.Text.Json.Serialization;

namespace SchoolManagement.ViewModel.QuestionResponses
{
    public class QuestionResponsesBatchRequestVM
    {
        [JsonPropertyName("tenant_id")]
        public int TenantId { get; set; }

        [JsonPropertyName("paper_id")]
        public int PaperId { get; set; }

        [JsonPropertyName("candidate_id")]
        public int CandidateId { get; set; }

        [JsonPropertyName("created_by")]
        public int? CreatedBy { get; set; }

        [JsonPropertyName("responses")]
        public List<QuestionResponseItemVM> Responses { get; set; }
    }

    public class QuestionResponseItemVM
    {
        [JsonPropertyName("question_id")]
        public int QuestionId { get; set; }

        [JsonPropertyName("response_text")]
        public string? ResponseText { get; set; }
    }
}