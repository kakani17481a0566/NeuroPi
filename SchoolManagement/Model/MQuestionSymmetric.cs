using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("questions_symmetric")]
    public class MQuestionSymmetric
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("q_id")]
        public string? QId { get; set; }

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

        [Column("rating_id")]
        public int? RatingId { get; set; }

        [ForeignKey(nameof(RatingId))]
        public virtual MMaster? Rating { get; set; }

        [Column("min_grade")]
        public int MinGrade { get; set; }

        [Column("max_grade")]
        public int MaxGrade { get; set; }

        [Column("min_scaling")]
        public int? MinScaling { get; set; }

        [ForeignKey(nameof(MinScaling))]
        public virtual MMaster? MinScalingRef { get; set; }

        [Column("max_scaling")]
        public int? MaxScaling { get; set; }

        [ForeignKey(nameof(MaxScaling))]
        public virtual MMaster? MaxScalingRef { get; set; }

        [Column("counselor_note")]
        public string? CounselorNote { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
