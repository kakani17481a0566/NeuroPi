using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_focus_item")]
    public class MNutritionalFocusItem : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("nutritional_focus_id")]
        [ForeignKey("NutritionalFocus")]
        public int NutritionalFocusId { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItem")]
        public int NutritionalItemId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get;set; }

        public MNutritionalFocus NutritionalFocus { get; set; }

        public MNutritionalItem NutritionalItem { get; set; }

    }
}
