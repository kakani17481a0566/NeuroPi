using System.ComponentModel.DataAnnotations.Schema;

namespace NeuroPi.CommonLib.Model.Nutrition
{
    [Table("nut_resource_type", Schema = "nutrition")]
    public class MResourceType : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("code")]
        public string? Code { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

        // Navigation
        public ICollection<MResourceMaster> Resources { get; set; } = new List<MResourceMaster>();
    }
}
