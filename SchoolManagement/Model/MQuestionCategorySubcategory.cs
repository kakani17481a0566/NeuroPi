using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("question_category_subcategories")]
    public class MQuestionCategorySubcategory : MBaseModel
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

        [Column("subcategory_name")]
        public string SubcategoryName { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<MQuestionBank> Questions { get; set; } = new List<MQuestionBank>();
    }
}
