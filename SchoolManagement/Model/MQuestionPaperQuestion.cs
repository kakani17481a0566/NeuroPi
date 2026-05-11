using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_paper_questions")]
    public class MQuestionPaperQuestion : MBaseModel
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

        [Column("question_id")]
        public int QuestionId { get; set; }

        [ForeignKey(nameof(QuestionId))]
        public virtual MQuestionBank QuestionBank { get; set; }

        [Column("sq")]
        public int Sq { get; set; }
    }
}
