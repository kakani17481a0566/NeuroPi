using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_options")]
    public class MQuestionOption : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("question_id")]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual MQuestionBank QuestionBank { get; set; }

        [Column("sq")]
        public int Sq { get; set; }

        [Column("option_code")]
        public string? OptionCode { get; set; }

        [Column("option_text")]
        public string OptionText { get; set; }

        [Column("is_correct")]
        public bool IsCorrect { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MQuestionResponse> Responses { get; set; } = new List<MQuestionResponse>();
    }
}
