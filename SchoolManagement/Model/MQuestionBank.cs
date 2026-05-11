using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_bank")]
    public class MQuestionBank : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual MQuestionCategory Category { get; set; }

        [Column("subcategory_id")]
        public int SubcategoryId { get; set; }

        [ForeignKey(nameof(SubcategoryId))]
        public virtual MQuestionCategorySubcategory Subcategory { get; set; }

        [Column("question_text")]
        public string QuestionText { get; set; }

        [Column("question_type_id")]
        public int QuestionTypeId { get; set; }

        [ForeignKey(nameof(QuestionTypeId))]
        public virtual MMaster QuestionType { get; set; }

        [Column("is_required")]
        public bool IsRequired { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MQuestionOption> Options { get; set; } = new List<MQuestionOption>();
        public virtual ICollection<MQuestionPaperQuestion> PaperQuestions { get; set; } = new List<MQuestionPaperQuestion>();
        public virtual ICollection<MQuestionResponse> Responses { get; set; } = new List<MQuestionResponse>();
    }
}
