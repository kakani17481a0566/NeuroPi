using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_categories")]
    public class MQuestionCategory : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("category_name")]
        public string CategoryName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MQuestionCategorySubcategory> Subcategories { get; set; } = new List<MQuestionCategorySubcategory>();
        public virtual ICollection<MQuestionBank> Questions { get; set; } = new List<MQuestionBank>();
    }
}
