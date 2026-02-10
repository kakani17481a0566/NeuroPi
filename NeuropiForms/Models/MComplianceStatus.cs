using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("compliance_status")]
    public class MComplianceStatus : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("compliance_id")]
        public int? ComplianceId { get; set; }

        [ForeignKey("ComplianceId")]
        public virtual MComplianceMaster MComplianceMaster { get; set; }

        [Column("status_name")]
        public string StatusName { get; set; }

        [Column("min")]
        public int? Min { get; set; }

        [Column("max")]
        public int? Max { get; set; }

        [Column("col_code")]
        public string ColCode { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
