using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_meal_plan")]
    public class MMealPlan : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("meal_type_id")]
        [ForeignKey("MealType")]
        public int MealTypeId { get; set; }

        [Column("date")]
        public DateOnly Date { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItem")]
        public int NutritionalItemId { get; set; }

        [Column("qty")]
        public int Quantity { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public MMealType MealType { get; set; }

        public MNutritionalItem NutritionalItem { set; get; }
    }
}
