using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.CommonLib.Model;


namespace SchoolManagement.Model
{
    [Table("inventory_transfers")]
    public class MInventoryTransfer : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("transfer_type")]
        [Required]
        public string TransferType { get; set; } // 'REFILL', 'TRANSFER'

        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey("ItemId")]
        public virtual MItems Item { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("size")]
        public int? Size { get; set; }

        // Source Branch (Nullable for Refills)
        [Column("from_branch_id")]
        public int? FromBranchId { get; set; }

        [ForeignKey("FromBranchId")]
        public virtual MBranch? FromBranch { get; set; }

        // Target Branch
        [Column("to_branch_id")]
        public int ToBranchId { get; set; }

        [ForeignKey("ToBranchId")]
        public virtual MBranch ToBranch { get; set; }

        [Column("supplier_id")]
        public int? SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual MSupplier? Supplier { get; set; }

        // Workflow
        [Column("status")]
        public string Status { get; set; } = "PENDING"; // PENDING, APPROVED, REJECTED

        [Column("approved_by")]
        public int? ApprovedBy { get; set; }

        [Column("approval_date")]
        public DateTime? ApprovalDate { get; set; }

        // Tenancy
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey("TenantId")]
        public virtual MTenant Tenant { get; set; }
    }
}
