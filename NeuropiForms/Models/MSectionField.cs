using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("section_fields")]
    public class MSectionField : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("section_id")]
        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual MSection MSection { get; set; }

        [Column("field_id")]
        public int? FieldId { get; set; }

        [ForeignKey("FieldId")]
        public virtual MField MField { get; set; }

        [Column("display_order")]
        public int? DisplayOrder { get; set; }

        [Column("is_required")]
        public bool? IsRequired { get; set; }

        [Column("custom_label")]
        public string? CustomLabel { get; set; }

        [Column("version_id")]
        public double? VersionId { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
