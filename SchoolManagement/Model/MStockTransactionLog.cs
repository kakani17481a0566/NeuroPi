using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("stock_transaction_log")]
    public class MStockTransactionLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Required]
        [Column("branch_id")]
        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch Branch { get; set; }

        [Required]
        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Item { get; set; }

        [Required]
        [Column("transaction_type")]
        [StringLength(50)]
        public string TransactionType { get; set; } // PURCHASE_RECEIPT, SALE, TRANSFER_IN, TRANSFER_OUT, ADJUSTMENT, STOCKTAKE

        [Required]
        [Column("quantity_change")]
        public int QuantityChange { get; set; }

        [Column("quantity_before")]
        public int? QuantityBefore { get; set; }

        [Column("quantity_after")]
        public int? QuantityAfter { get; set; }

        [Column("unit_cost")]
        public decimal? UnitCost { get; set; }

        [Column("reference_id")]
        public int? ReferenceId { get; set; }

        [Column("reference_type")]
        [StringLength(50)]
        public string? ReferenceType { get; set; } // PO, TRANSFER, STOCKTAKE, SALE

        [Column("batch_id")]
        public int? BatchId { get; set; }

        [ForeignKey(nameof(BatchId))]
        public virtual MItemBatch? Batch { get; set; }

        [Column("uom_code")]
        [StringLength(10)]
        public string UomCode { get; set; } = "EA";

        [Column("adjustment_reason_id")]
        public int? AdjustmentReasonId { get; set; }

        [ForeignKey(nameof(AdjustmentReasonId))]
        public virtual MStockAdjustmentReason? AdjustmentReason { get; set; }

        [Column("serial_number_id")]
        public int? SerialNumberId { get; set; }

        [ForeignKey(nameof(SerialNumberId))]
        public virtual MItemSerialNumber? SerialNumber { get; set; }

        [Column("user_name")]
        [StringLength(100)]
        public string? UserName { get; set; }

        [Column("ip_address")]
        [StringLength(45)]
        public string? IpAddress { get; set; }

        [Required]
        [Column("transaction_date")]
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }
    }
}
