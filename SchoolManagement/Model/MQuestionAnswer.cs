using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Model
{
    [Table("questions_answers")]
    public class MQuestionAnswer : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("questions_id")]
        public int? QuestionsId { get; set; }

        [ForeignKey(nameof(QuestionsId))]
        public virtual MQuestion Question { get; set; }

        [Column("answer")]
        public string Answer { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
