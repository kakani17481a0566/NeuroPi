using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.Model
{
    [Table("item_cost_history")]
    public class MItemCostHistory : MBaseModel
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

        [Column("branch_id")]
        public int? BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public virtual MBranch? Branch { get; set; }

        [Required]
        [Column("cost_type")]
        [StringLength(20)]
        public string CostType { get; set; } // PURCHASE, AVERAGE, STANDARD, LAST

        [Required]
        [Column("unit_cost")]
        public decimal UnitCost { get; set; }

        [Required]
        [Column("effective_date")]
        public DateTime EffectiveDate { get; set; } = DateTime.UtcNow;

        [Column("source_reference_type")]
        [StringLength(50)]
        public string? SourceReferenceType { get; set; }

        [Column("source_reference_id")]
        public int? SourceReferenceId { get; set; }

        [Required]
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [ForeignKey(nameof(TenantId))]
        public virtual MTenant Tenant { get; set; }
    }
}
