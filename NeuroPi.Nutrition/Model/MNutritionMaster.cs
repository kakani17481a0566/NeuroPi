using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NeuroPi.CommonLib.Model;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_master")]
    public class MNutritionMaster: MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string? Code { get; set; }


        [Column("masters_type_id")]
        [ForeignKey("NutritionMasterType")]
        public int NutritionMasterTypeId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        public MNutritionMasterType NutritionMasterType { get; set; }

       

    }
}
