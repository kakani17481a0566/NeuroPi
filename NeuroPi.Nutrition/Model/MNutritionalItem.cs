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


        [Column("type")]
        [ForeignKey("NutritionalItemType")]
        public int NutritionalItemTypeId { get; set; }

        [Column("edible")]
        public bool Edible { get; set; }

        [Column("item_url")]
        public string? ItemImage { get; set; } = null;

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public MNutritionalItemType NutritionalItemType { get; set; }
    }
}
