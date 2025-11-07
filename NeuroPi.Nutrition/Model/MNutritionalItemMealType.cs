using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_nutritional_item_mealtype")]
    public class MNutritionalItemMealType :MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItems")]
        public int NutritionalItemId { get; set; }


        [Column("meal_type_id")]
        [ForeignKey("MealType")]
        public int MealTypeId { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public MNutritionalItem NutritionalItems { get; set; }
        public MMealType MealType { get; set; }

        public MTenant Tenant { get; set; }


    }
}
