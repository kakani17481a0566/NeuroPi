using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_nutritional_item")]
    public class MNutritionalItem : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("calories_qty")]
        public int CaloriesQuantity { get; set; }

        [Column("protein_qty")]
        public int ProteinQuantity { get; set; }

        [Column("qty")]
        public int Quantity { get; set; }

        [Column("recipe")]
        public bool Receipe { get; set; }

        // ⭐ CORRECT: maps to column "type"
        [Column("type")]
        public int NutritionalItemTypeId { get; set; }

        // ⭐ OPTIONAL: navigation
        [ForeignKey("NutritionalItemTypeId")]
        public MNutritionalItemType? NutritionalItemType { get; set; }

        [Column("eatble")]
        public bool Eatble { get; set; }

        [Column("item_url")]
        public string? ItemImage { get; set; } = null;

        // ⭐ CORRECT: maps to diet_type_id
        [Column("diet_type_id")]
        public int? DietTypeId { get; set; }

        [ForeignKey("DietTypeId")]
        public MNutritionMaster? DietType { get; set; }

        // ⭐ CORRECT: maps to tenant_id
        [Column("tenant_id")]
        public int TenantId { get; set; }
    }
}
