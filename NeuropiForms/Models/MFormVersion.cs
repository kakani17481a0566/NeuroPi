using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("forms_version")]
    public class MFormVersion : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int? FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual MForm MForm { get; set; }

        [Column("version_id")]
        public double? VersionId { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
