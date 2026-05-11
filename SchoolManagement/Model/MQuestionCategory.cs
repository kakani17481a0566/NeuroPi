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

        [Column("category_name")]
        public string CategoryName { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        public virtual ICollection<MQuestionCategorySubcategory> Subcategories { get; set; } = new List<MQuestionCategorySubcategory>();
    }
}
