using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_unplanned_meal")]
    public class MUnplannedMeal:MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("meal_plan_id")]
        [ForeignKey("MealPlan")]
        public int MealPlanId { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItem")]
        public int NutritionalItemId { get; set; }

        [Column("qty")]
        public int Quantity { get; set; }

        [Column("other_name")]
        public string OtherName { get; set; }

        [Column("other_calories_qty")]
        public int OtherCaloriesQuantity { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public MMealPlan MealPlan { get; set; }
        public MNutritionalItem NutritionalItem { get; set; }

        public MTenant Tenant { get; set; }



    }
}
