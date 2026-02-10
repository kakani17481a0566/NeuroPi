using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("forms")]
    public class MForm : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("active_version")]
        public bool? ActiveVersion { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; } = true;

        [Column("is_published")]
        public bool? IsPublished { get; set; } = false;

        [Column("version_id")]
        public double? VersionId { get; set; }

        [Column("max_versions")]
        public int? MaxVersions { get; set; }

        [Column("compliance_id")]
        public int? ComplianceId { get; set; }

        [ForeignKey("ComplianceId")]
        public virtual MComplianceStatus MComplianceStatus { get; set; }

        [Column("storage_type_id")]
        public int? StorageTypeId { get; set; }

        [Column("storageid")]
        public string StorageId { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
