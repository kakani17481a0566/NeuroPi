using NeuroPi.Nutrition.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model.Nutrition
{
    [Table("nut_resource_master", Schema = "nutrition")]
    public class MResourceMaster : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("short_name")]
        public string? ShortName { get; set; }

        [Column("type_id")]
        public int? TypeId { get; set; }

        [ForeignKey("TypeId")]
        public MResourceType? Type { get; set; }

        [Column("preview_url")]
        public string? PreviewUrl { get; set; }

        [Column("module_id")]
        public int? ModuleId { get; set; }

        [ForeignKey("ModuleId")]
        public MNutritionMaster? Module { get; set; }

        [Column("image")]
        public string? Image { get; set; }

        [Column("script")]
        public string? Script { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        // Navigation
        public ICollection<MResourceInstruction> Instructions { get; set; }
        public ICollection<MTimetable> Timetables { get; set; } 
    }
}
