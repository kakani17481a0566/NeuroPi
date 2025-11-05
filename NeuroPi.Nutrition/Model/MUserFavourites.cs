using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_user_favourites")]
    public class MUserFavourites : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("nutritional_item_id")]
        [ForeignKey("NutritionalItems")]
        public int NutritionalItemId { get; set; } 

        [Column("tenant_id")]
        public int TenantId { get; set; } 

        public  MNutritionalItem NutritionalItems { get; set; }

    }
}
