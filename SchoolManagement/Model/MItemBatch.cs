using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_batches")]
    public class MItemBatch : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Item { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; }

        [Required]
        [Column("batch_number")]
        [StringLength(50)]
        public string BatchNumber { get; set; }

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("manufacture_date")]
        public DateTime? ManufactureDate { get; set; }

        [Column("received_date")]
        public DateTime? ReceivedDate { get; set; }

        [Column("quantity_remaining")]
        public int QuantityRemaining { get; set; } = 0;

        [Column("quality_status")]
        [StringLength(20)]
        public string QualityStatus { get; set; } = "APPROVED"; // PENDING, APPROVED, REJECTED, QUARANTINE

        [Column("quality_notes")]
        public string? QualityNotes { get; set; }

        [Column("supplier_id")]
        public int? SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public virtual MSupplier? Supplier { get; set; }

        [Column("certificate_number")]
        [StringLength(50)]
        public string? CertificateNumber { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
