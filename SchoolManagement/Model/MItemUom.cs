using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_uom")]
    public class MItemUom : MBaseModel
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
        [Column("uom_code")]
        [StringLength(10)]
        public string UomCode { get; set; }

        [Required]
        [Column("uom_name")]
        [StringLength(50)]
        public string UomName { get; set; }

        [Required]
        [Column("conversion_factor")]
        public decimal ConversionFactor { get; set; } = 1.0m;

        [Column("is_base_uom")]
        public bool IsBaseUom { get; set; } = false;

        [Column("barcode")]
        [StringLength(50)]
        public string? Barcode { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
