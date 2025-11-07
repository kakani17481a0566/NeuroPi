using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_user_mealtype")]
    public class MUserMealType:MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }


        [Column("meal_type_id")]
        [ForeignKey("MealType")]
        public int MealTypeId { get; set; }

        [Column("preferred_time")]
        public string PreferredMealTime { get; set; }

        [Column("tenant_id")]
        [ForeignKey("Tenant")]
        public int TenantId { get; set; }

        public MMealType MealType { get; set; }

    }
}
