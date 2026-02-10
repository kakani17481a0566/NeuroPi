using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("form_sections_mapping")]
    public class MFormSectionMapping : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int? FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual MForm MForm { get; set; }

        [Column("section_id")]
        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual MSection MSection { get; set; }

        [Column("display_order")]
        public int? DisplayOrder { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
