using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("section")]
    public class MSection : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        [Column("Parameters")]
        public bool? Parameters { get; set; }

        [Column("weightage")]
        public double? Weightage { get; set; }

        [Column("auto_weight_cal")]
        public bool? AutoWeightCal { get; set; }

        [Column("multi_values")]
        public bool? MultiValues { get; set; }

        [Column("version_id")]
        public double? VersionId { get; set; }

        [Column("max")]
        public int? Max { get; set; }

        [Column("min")]
        public int? Min { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
