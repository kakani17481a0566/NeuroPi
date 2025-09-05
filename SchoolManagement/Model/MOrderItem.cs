using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("order_item")]
    public class MOrderItem : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }

        // numeric -> decimal (quantities often allow fractions; adjust precision as you prefer)
        [Column("order_qty")]
        public decimal OrderQuantity { get; set; }

        [Column("delivered_qty")]
        public decimal DeliveredQuantity { get; set; }

        // money -> decimal
        [Column("unit_price")]
        public decimal UnitPrice { get; set; }

        [Column("tenant_id")]
        public int TenantId { get; set; }

        // FK attributes belong on navigations; point them at the FK property names
        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual MOrders Orders { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Items { get; set; }
    }
}
