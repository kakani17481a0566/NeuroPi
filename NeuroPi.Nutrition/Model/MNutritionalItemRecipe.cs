using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{

    [Table("nut_nutritional_item_recipe")]
    public class MNutritionalItemRecipe : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nutritional_item_id")]
        [Required(ErrorMessage = "code is mandatory.")]
        public int NutritionalItemId { get; set; }

        [Column("recipe_item_id")]
        public int RecipeItemId { get; set; }

        [Column("item_qty")]
        public int ItemQty { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }


    }
}
