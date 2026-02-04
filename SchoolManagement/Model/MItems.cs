using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("items")]
    public class MItems : MBaseModel
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual MItemCategory ItemCategory { get; set; }

        [Column("parent_item_id")]
        public int? ParentItemId { get; set; }

        [ForeignKey(nameof(ParentItemId))]
        public virtual MItems ParentItem { get; set; }

        [Column("height")]
        public int Height { get; set; }

        [Column("width")]
        public int Width { get; set; }

        [Column("depth")]
        public int Depth { get; set; }

        [Column("size")]
        public int? Size { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        // 🔹 Extra fields from DB schema
        [Column("description")]
        public string? Description { get; set; }

        [Column("is_group")]
        public bool IsGroup { get; set; } = false;

        [Column("item_code")]
        public string? ItemCode { get; set; }

        [Column("base_uom")]
        [StringLength(10)]
        public string? BaseUom { get; set; }

        [Column("is_batch_tracked")]
        public bool IsBatchTracked { get; set; } = false;

        [Column("is_serialized")]
        public bool IsSerialized { get; set; } = false;

        [Column("min_order_quantity")]
        public int? MinOrderQuantity { get; set; }

        [Column("lead_time_days")]
        public int? LeadTimeDays { get; set; }
    }
}
