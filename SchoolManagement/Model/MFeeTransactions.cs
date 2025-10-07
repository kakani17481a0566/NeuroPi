using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("fee_transactions", Schema = "public")]
    public class MFeeTransactions : MBaseModel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Required]
        [Column("fee_structure_id")]
        public int FeeStructureId { get; set; }

        [Required]
        [Column("student_id")]
        public int StudentId { get; set; }

        [Required]
        [Column("trx_date")]
        public DateTime TrxDate { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(50)]
        [Column("trx_type")]
        public string TrxType { get; set; } = string.Empty;

        [MaxLength(100)]
        [Column("trx_name")]
        public string? TrxName { get; set; }

        [Column("debit", TypeName = "numeric(12,2)")]
        public decimal Debit { get; set; } = 0;

        [Column("credit", TypeName = "numeric(12,2)")]
        public decimal Credit { get; set; } = 0;

        [MaxLength(30)]
        [Column("trx_status")]
        public string TrxStatus { get; set; } = "Pending";

        [MaxLength(100)]
        [Column("trx_id")]
        public string? TrxId { get; set; }

        // --- Navigation Properties ---
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey(nameof(FeeStructureId))]
        public virtual MFeeStructure FeeStructure { get; set; }

        [ForeignKey(nameof(StudentId))]
        public virtual MStudent Student { get; set; }

        // 🔹 Optionally link to audit users
        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [ForeignKey(nameof(UpdatedBy))]
        public virtual MUser? UpdatedByUser { get; set; }
    }
}
