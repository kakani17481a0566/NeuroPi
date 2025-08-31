using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    public class MItemBranch:MBaseModel

    {
        [Column("id")]
        public int Id { get; set; }

        [Column("id")]
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public MItems Item { get; set; }

        [Column("id")]
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public MBranch Branch { get; set; }

        [Column("item_quantity")]
        public int ItemQuantity { get; set; }

        [Column("item_price")]
        public int ItemPrice { get; set; }

        [Column("item_cost")]
        public int ItemCost { get; set; }

        [Column("item_rol")]
        public int ItemReOrderLevel { get; set; }

        [Column("item_location_id")]
        public int ItemLocationId { get; set; }
        [ForeignKey("ItemLocationId")]
        public MItemLocation ItemLocation { get; set; }

        [Required]
        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }




    }
}
