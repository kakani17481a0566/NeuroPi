using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("stocktake_lines")]
    public class MStocktakeLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("stocktake_id")]
        public int StocktakeId { get; set; }

        [ForeignKey(nameof(StocktakeId))]
        public virtual MStocktakeHeader StocktakeHeader { get; set; }

        [Required]
        [Column("item_id")]
        public int ItemId { get; set; }

        [ForeignKey(nameof(ItemId))]
        public virtual MItems Item { get; set; }

        [Column("batch_id")]
        public int? BatchId { get; set; }

        [ForeignKey(nameof(BatchId))]
        public virtual MItemBatch? Batch { get; set; }

        [Column("uom_code")]
        [StringLength(10)]
        public string UomCode { get; set; } = "EA";

        [Required]
        [Column("system_quantity")]
        public int SystemQuantity { get; set; }

        [Required]
        [Column("counted_quantity")]
        public int CountedQuantity { get; set; }

        // Variance is computed column in DB: counted_quantity - system_quantity
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("variance")]
        public int Variance { get; set; }

        [Column("variance_reason_id")]
        public int? VarianceReasonId { get; set; }

        [ForeignKey(nameof(VarianceReasonId))]
        public virtual MStockAdjustmentReason? VarianceReason { get; set; }

        [Column("variance_notes")]
        public string? VarianceNotes { get; set; }

        [Column("counted_by")]
        public int? CountedBy { get; set; }

        [ForeignKey(nameof(CountedBy))]
        public virtual MUser? CountedByUser { get; set; }

        [Column("counted_at")]
        public DateTime CountedAt { get; set; } = DateTime.UtcNow;
    }
}
