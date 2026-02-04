using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_serial_numbers")]
    public class MItemSerialNumber : MBaseModel
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
        [Column("serial_number")]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        [Column("batch_id")]
        public int? BatchId { get; set; }

        [ForeignKey(nameof(BatchId))]
        public virtual MItemBatch? Batch { get; set; }

        [Column("branch_id")]
        public int? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch? Branch { get; set; }

        [Column("status")]
        [StringLength(20)]
        public string Status { get; set; } = "IN_STOCK"; // IN_STOCK, SOLD, DAMAGED, RETURNED

        [Column("received_date")]
        public DateTime? ReceivedDate { get; set; }

        [Column("sold_date")]
        public DateTime? SoldDate { get; set; }

        [Column("warranty_expiry_date")]
        public DateTime? WarrantyExpiryDate { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
