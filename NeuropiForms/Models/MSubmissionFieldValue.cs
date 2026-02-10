using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("submission_field_values")]
    public class MSubmissionFieldValue : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("submission_id")]
        public int? SubmissionId { get; set; }

        [ForeignKey("SubmissionId")]
        public virtual MFormSubmission MFormSubmission { get; set; }

        [Column("field_id")]
        public int? FieldId { get; set; }

        [ForeignKey("FieldId")]
        public virtual MField MField { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("value_date")]
        public DateTime? ValueDate { get; set; }

        [Column("remarks")]
        public string Remarks { get; set; }

        [Column("app_id")]
        public int? AppId { get; set; }

        [Column("tenant_id")]
        public int? TenantId { get; set; }

    }
}
