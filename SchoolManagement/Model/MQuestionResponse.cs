using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_responses")]
    public class MQuestionResponse : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("paper_id")]
        public int PaperId { get; set; }

        [ForeignKey(nameof(PaperId))]
        public virtual MQuestionPaper Paper { get; set; }

        [Column("candidate_id")]
        public int CandidateId { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual MQuestionSymmetric Question { get; set; }

        [Column("response_text")]
        public string? ResponseText { get; set; }
    }
}
