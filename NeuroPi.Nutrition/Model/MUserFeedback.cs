using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_user_feedback")]
    public class MUserFeedback : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("feedback_type_id")]
        public int FeedbackTypeId { get; set; }

        [Column("feedback_text")]
        public string FeedbackText { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }   // nullable as per your requirement

        [ForeignKey("FeedbackTypeId")]
        public virtual MNutritionMaster FeedbackType { get; set; }
    }
}
