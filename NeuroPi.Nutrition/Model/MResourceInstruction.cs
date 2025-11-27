using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model.Nutrition
{
    [Table("nut_resource_instruction", Schema = "nutrition")]
    public class MResourceInstruction : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("resource_id")]
        public int? ResourceId { get; set; }

        [ForeignKey(nameof(ResourceId))]
        public MResourceMaster? Resource { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }
    }
}
