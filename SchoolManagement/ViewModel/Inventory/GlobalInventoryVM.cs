using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.ViewModel.Inventory
{
    // ========================================
    // STOCKTAKE VIEW MODELS
    // ========================================

    public class StocktakeHeaderRequestVM
    {
        [Required]
        public string StocktakeNumber { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public DateTime StocktakeDate { get; set; }

        public int? CountedBy { get; set; }
        public string? Notes { get; set; }
    }

    public class StocktakeHeaderResponseVM
    {
        public int Id { get; set; }
        public string StocktakeNumber { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public DateTime StocktakeDate { get; set; }
        public string Status { get; set; }
        public int? CountedBy { get; set; }
        public string? CountedByName { get; set; }
        public int? ApprovedBy { get; set; }
        public string? ApprovedByName { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string? Notes { get; set; }
        public int TotalItems { get; set; }
        public int ItemsWithVariance { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class StocktakeLineRequestVM
    {
        [Required]
        public int StocktakeId { get; set; }

        [Required]
        public int ItemId { get; set; }

        public int? BatchId { get; set; }

        [Required]
        public int SystemQuantity { get; set; }

        [Required]
        public int CountedQuantity { get; set; }

        public int? VarianceReasonId { get; set; }
        public string? VarianceNotes { get; set; }
        public int? CountedBy { get; set; }
        public string? UomCode { get; set; }
    }

    public class StocktakeLineResponseVM
    {
        public int Id { get; set; }
        public int StocktakeId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? BatchId { get; set; }
        public string? BatchNumber { get; set; }
        public int SystemQuantity { get; set; }
        public int CountedQuantity { get; set; }
        public int Variance { get; set; }
        public int? VarianceReasonId { get; set; }
        public string? VarianceReasonDescription { get; set; }
        public string? VarianceNotes { get; set; }
        public int? CountedBy { get; set; }
        public string? CountedByName { get; set; }
        public string UomCode { get; set; }
        public DateTime CountedAt { get; set; }
    }

    public class ApproveStocktakeRequestVM
    {
        [Required]
        public int StocktakeId { get; set; }

        [Required]
        public int ApprovedBy { get; set; }

        public string? ApprovalNotes { get; set; }
        public bool IsApproved { get; set; }
    }

    public class StocktakeSummaryVM
    {
        public int HeaderId { get; set; }
        public int TotalItemsCounted { get; set; }
        public int TotalVarianceQty { get; set; }
        public int ItemsWithVariance { get; set; }
        public string Status { get; set; }
    }

    // ========================================
    // ITEM UOM VIEW MODELS
    // ========================================

    public class ItemUomRequestVM
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        [StringLength(10)]
        public string UomCode { get; set; }

        [Required]
        [StringLength(50)]
        public string UomName { get; set; }

        [Required]
        [Range(0.0001, double.MaxValue)]
        public decimal ConversionFactor { get; set; }

        public bool IsBaseUom { get; set; }
        public string? Barcode { get; set; }
    }

    public class ItemUomResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string UomCode { get; set; }
        public string UomName { get; set; }
        public decimal ConversionFactor { get; set; }
        public bool IsBaseUom { get; set; }
        public string? Barcode { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    // ========================================
    // COST HISTORY VIEW MODELS
    // ========================================

    public class CostHistoryRequestVM
    {
        [Required]
        public int ItemId { get; set; }

        public int? BranchId { get; set; }

        [Required]
        [StringLength(20)]
        public string CostType { get; set; } // PURCHASE, AVERAGE, STANDARD, LAST

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitCost { get; set; }

        public DateTime? EffectiveDate { get; set; }
        public string? SourceReferenceType { get; set; }
        public int? SourceReferenceId { get; set; }
    }

    public class CostHistoryResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string CostType { get; set; }
        public decimal UnitCost { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string? SourceReferenceType { get; set; }
        public int? SourceReferenceId { get; set; }
    }

    public class CostTrendVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal CurrentCost { get; set; }
        public decimal PreviousCost { get; set; }
        public decimal CostChange { get; set; }
        public decimal CostChangePercentage { get; set; }
        public DateTime EffectiveDate { get; set; }
    }

    // ========================================
    // STOCK ADJUSTMENT REASON VIEW MODELS
    // ========================================

    public class StockAdjustmentReasonRequestVM
    {
        [Required]
        [StringLength(20)]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [StringLength(20)]
        public string AdjustmentType { get; set; } // GAIN, LOSS, DAMAGE, EXPIRY, THEFT, OBSOLETE

        public bool RequiresApproval { get; set; }
    }

    public class StockAdjustmentReasonResponseVM
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AdjustmentType { get; set; }
        public bool RequiresApproval { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    // ========================================
    // STOCK TRANSACTION LOG VIEW MODELS
    // ========================================

    public class StockTransactionLogRequestVM
    {
        [Required]
        public int BranchId { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public int QuantityChange { get; set; }

        public decimal? UnitCost { get; set; }
        public int? ReferenceId { get; set; }
        public string? ReferenceType { get; set; }
        public int? BatchId { get; set; }
        public string UomCode { get; set; } = "EA";
        public int? AdjustmentReasonId { get; set; }
        public int? SerialNumberId { get; set; }
        public string? Notes { get; set; }
    }

    public class StockTransactionLogResponseVM
    {
        public long Id { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string TransactionType { get; set; }
        public int QuantityChange { get; set; }
        public int? QuantityBefore { get; set; }
        public int? QuantityAfter { get; set; }
        public decimal? UnitCost { get; set; }
        public string? ReferenceType { get; set; }
        public int? ReferenceId { get; set; }
        public string? BatchNumber { get; set; }
        public string UomCode { get; set; }
        public string? AdjustmentReason { get; set; }
        public string? UserName { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class StockMovementSummaryVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int BranchId { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public int OpeningStock { get; set; }
        public int TotalIn { get; set; }
        public int TotalOut { get; set; }
        public int Adjustments { get; set; }
        public int ClosingStock { get; set; }
    }

    // ========================================
    // SUPPLIER PERFORMANCE VIEW MODELS
    // ========================================

    public class SupplierPerformanceRequestVM
    {
        [Required]
        public int SupplierId { get; set; }

        public int? PoId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public bool? OnTimeDelivery { get; set; }

        [Range(1, 5)]
        public int? QualityRating { get; set; }

        [Range(0, 100)]
        public decimal? QuantityAccuracyPct { get; set; }

        public string? Notes { get; set; }
    }

    public class SupplierPerformanceResponseVM
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? PoId { get; set; }
        public string? PoNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public bool? OnTimeDelivery { get; set; }
        public int? DaysLate { get; set; }
        public int? QualityRating { get; set; }
        public decimal? QuantityAccuracyPct { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class SupplierPerformanceSummaryVM
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int TotalDeliveries { get; set; }
        public int OnTimeDeliveries { get; set; }
        public decimal OnTimeDeliveryPercentage { get; set; }
        public decimal AverageQualityRating { get; set; }
        public decimal AverageQuantityAccuracy { get; set; }
        public int AverageDaysLate { get; set; }
    }

    // ========================================
    // SERIAL NUMBER VIEW MODELS
    // ========================================

    public class ItemSerialNumberRequestVM
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        [StringLength(100)]
        public string SerialNumber { get; set; }

        public int? BatchId { get; set; }
        public int? BranchId { get; set; }
        public string Status { get; set; } = "IN_STOCK";
        public DateTime? ReceivedDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
    }

    public class ItemSerialNumberResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string SerialNumber { get; set; }
        public int? BatchId { get; set; }
        public string? BatchNumber { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public string Status { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        public DateTime? WarrantyExpiryDate { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    // ========================================
    // BATCH VIEW MODELS
    // ========================================

    public class ItemBatchRequestVM
    {
        [Required]
        public int ItemId { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        [StringLength(50)]
        public string BatchNumber { get; set; }

        public DateTime? ExpiryDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int QuantityRemaining { get; set; }
        public string QualityStatus { get; set; } = "APPROVED";
        public string? QualityNotes { get; set; }
        public int? SupplierId { get; set; }
        public string? CertificateNumber { get; set; }
    }

    public class ItemBatchResponseVM
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? ManufactureDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int QuantityRemaining { get; set; }
        public string QualityStatus { get; set; }
        public string? QualityNotes { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? CertificateNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    // ========================================
    // DASHBOARD & EXPLORER VIEW MODELS
    // ========================================

    public class InventoryDashboardVM
    {
        public decimal TotalStockValue { get; set; }
        public int LowStockItemsCount { get; set; }
        public int OutOfStockItemsCount { get; set; }
        public int PendingTransfersCount { get; set; }
        public List<StockTransactionLogResponseVM> RecentTransactions { get; set; }
    }

    public class InventoryItemVM
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public string CategoryName { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public int CurrentQuantity { get; set; }
        public string BaseUom { get; set; }
        public int ReOrderLevel { get; set; }
        public decimal AverageCost { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime? LastMovementDate { get; set; }
        public bool IsLowStock => CurrentQuantity <= ReOrderLevel;
    }
}
