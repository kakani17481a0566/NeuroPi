using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_recipes_instructions")]
    public class MRecipesInstructions : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("seq_no")]
        public int SequenceNumber { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItems")]
        public int NutritionalItemId { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public MNutritionalItem NutritionalItems { get; set; }
    }
}
