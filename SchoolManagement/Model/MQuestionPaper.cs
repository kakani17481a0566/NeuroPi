using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_papers")]
    public class MQuestionPaper : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("paper_name")]
        public string PaperName { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        public virtual ICollection<MQuestionPaperQuestion> PaperQuestions { get; set; } = new List<MQuestionPaperQuestion>();
        public virtual ICollection<MQuestionResponse> Responses { get; set; } = new List<MQuestionResponse>();
    }
}
