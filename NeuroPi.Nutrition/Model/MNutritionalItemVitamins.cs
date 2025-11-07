using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_nutritional_item_vitamins")]
    public class MNutritionalItemVitamins:MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nutritional_item_id")]
        public int NutritionalItemId { get; set; }

        [Column("vitamins_id")]
        public int VitaminsId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }



    }
}
