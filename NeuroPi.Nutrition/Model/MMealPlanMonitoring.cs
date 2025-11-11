using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_meal_plan_monitoring")]
    public class MMealPlanMonitoring : MBaseModel
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

        [Column("tenant_id")]
        public int TenantId { get; set; }   

        public MMealPlan MealPlan { get; set; }
        public MNutritionalItem NutritionalItem { get; set; }


    }
}
