using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("stock_adjustment_reasons")]
    public class MStockAdjustmentReason : MBaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("code")]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        [Column("description")]
        public string Description { get; set; }

        [Required]
        [Column("adjustment_type")]
        [StringLength(20)]
        public string AdjustmentType { get; set; } // GAIN, LOSS, DAMAGE, EXPIRY, THEFT, OBSOLETE

        [Column("requires_approval")]
        public bool RequiresApproval { get; set; } = false;

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
