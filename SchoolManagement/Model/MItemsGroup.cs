using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("items_group")]
    public class MItemsGroup : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("set_item_id")]
        public int SetItemId { get; set; }

        [ForeignKey(nameof(SetItemId))]
        public virtual MItems SetItem { get; set; }   // 🔹 Parent/Grouped item

        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Item { get; set; }     // 🔹 Child item

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("discount_price", TypeName = "numeric")]
        public decimal? DiscountPrice { get; set; }

        [Column("fixed_price", TypeName = "numeric")]
        public decimal? FixedPrice { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
