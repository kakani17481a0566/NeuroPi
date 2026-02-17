using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("submission_section_values")]
    public class MSubmissionSectionValue : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("submission_id")]
        public int SubmissionId { get; set; }

        [ForeignKey("SubmissionId")]
        public virtual MFormSubmission MFormSubmission { get; set; }

        [Column("section_id")]
        public int SectionId { get; set; }

        [ForeignKey("SectionId")]
        public virtual MSection MSection { get; set; }

        [Column("value")]
        public double Value { get; set; }

        [Column("app_id")]
        public int AppId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

    }
}
