using NeuroPi.CommonLib.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.Nutrition.Model
{
    [Table("nut_master_type")]
    public class MNutritionMasterType : MBaseModel
    {

        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

    }
}
