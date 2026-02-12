using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;

namespace NeuropiForms.Models
{
    [Table("form_submissions")]
    public class MFormSubmission : MBaseModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("form_id")]
        public int FormId { get; set; }

        [ForeignKey("FormId")]
        public virtual MForm MForm { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }

        [Column("submitted_by")]
        public int SubmittedBy { get; set; }

        [Column("target_user_id")]
        public int TargetUserId { get; set; }

        [Column("status_id")]
        [Required(ErrorMessage ="Required statusId")]
        public int StatusId { get; set; }

        [Column("entry_date")]
        public DateTime EntryDate { get; set; } = DateTime.UtcNow;

        [Column("submition_date")]
        public DateTime SubmissionDate { get; set; }

        [Column("version_id")]
        public float VersionId { get; set; }

        [Column("app_id")]
        public int AppId { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

    }
}
