using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_nutritional_item_type")]
    public class MNutritionalIteamType : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("code")]
        [Required(ErrorMessage = "code is mandatory.")]
        public string? Code { get; set; }

        [Column("name")]
        public string? Name { get; set; }        

        [Column("tenant_id")]
        public int TenantId { get; set; }


    }
}
