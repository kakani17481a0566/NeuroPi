using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("stocktake_header")]
    public class MStocktakeHeader : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("stocktake_number")]
        [StringLength(50)]
        public string StocktakeNumber { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; }

        [Required]
        [Column("stocktake_date")]
        public DateTime StocktakeDate { get; set; }

        [Required]
        [Column("status")]
        [StringLength(20)]
        public string Status { get; set; } = "IN_PROGRESS"; // IN_PROGRESS, COMPLETED, APPROVED, CANCELLED

        [Column("counted_by")]
        public int? CountedBy { get; set; }

        [ForeignKey(nameof(CountedBy))]
        public virtual MUser? CountedByUser { get; set; }

        [Column("approved_by")]
        public int? ApprovedBy { get; set; }

        [ForeignKey(nameof(ApprovedBy))]
        public virtual MUser? ApprovedByUser { get; set; }

        [Column("approval_date")]
        public DateTime? ApprovalDate { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        // Navigation property
        public virtual ICollection<MStocktakeLine> StocktakeLines { get; set; }
    }
}
