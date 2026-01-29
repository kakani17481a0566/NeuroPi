using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("supplier_performance")]
    public class MSupplierPerformance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("supplier_id")]
        public int SupplierId { get; set; }

        [ForeignKey(nameof(SupplierId))]
        public virtual MSupplier Supplier { get; set; }

        [Column("po_id")]
        public int? PoId { get; set; }

        [Column("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        [Column("expected_date")]
        public DateTime? ExpectedDate { get; set; }

        [Column("on_time_delivery")]
        public bool? OnTimeDelivery { get; set; }

        // Computed column in DB: delivery_date - expected_date
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("days_late")]
        public int? DaysLate { get; set; }

        [Column("quality_rating")]
        public int? QualityRating { get; set; } // 1-5

        [Column("quantity_accuracy_pct")]
        public decimal? QuantityAccuracyPct { get; set; }

        [Column("notes")]
        public string? Notes { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        public virtual MUser? CreatedByUser { get; set; }

        [Column("created_on")]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
