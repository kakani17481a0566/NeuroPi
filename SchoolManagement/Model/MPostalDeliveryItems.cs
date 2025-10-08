using NeuroPi.UserManagment.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Model
{
    [Table("postal_delivery_items")]
    public class MPostalDeliveryItems : MBaseModel
    {
       
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("PostalDeliveries")]
        [Column("delivery_id")]
        public int DeliveryId { get; set; }

        [Column("item_id")]
        [ForeignKey("Items")]
        public int ItemId { get; set; }

        [Column("item_name")]
        [ForeignKey("Name")]
        public string ItemName { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("unit_cost")]
        public decimal UnitCost { get; set; }

        [ForeignKey("Tenant")]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        public virtual MTenant Tenant { get; set; }

        public virtual MPostalDeliveries PostalDeliveries { get; set; }

        public virtual MItems Items { get; set; }
    }
}
