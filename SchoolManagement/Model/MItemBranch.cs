using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_branch")] // <-- change if your actual table name differs
    public class MItemBranch : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }                  

        [Column("item_id")]
        public int ItemId { get; set; }               
        public MItems Item { get; set; }

        [Column("branch_id")]
        public int BranchId { get; set; }            
        [ForeignKey(nameof(BranchId))]
        public MBranch Branch { get; set; }

        [Column("item_quantity")]
        public int ItemQuantity { get; set; }

        
        [Column("item_price")]
        public int ItemPrice { get; set; }

        [Column("item_cost")]
        public int ItemCost { get; set; }

        [Column("item_rol")]
        public int ItemReOrderLevel { get; set; }

        [Column("item_max_level")]
        public int? ItemMaxLevel { get; set; }

        [Column("last_purchase_cost")]
        public decimal? LastPurchaseCost { get; set; }

        [Column("average_cost")]
        public decimal? AverageCost { get; set; }

        [Column("last_stock_count_date")]
        public DateTime? LastStockCountDate { get; set; }

        [Column("last_movement_date")]
        public DateTime? LastMovementDate { get; set; }

        [Column("item_location_id")]
        public int? ItemLocationId { get; set; }
        [ForeignKey(nameof(ItemLocationId))]
        public MItemLocation? ItemLocation { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }
        [ForeignKey(nameof(TenantId))]
        public MTenant Tenant { get; set; }
    }
}
