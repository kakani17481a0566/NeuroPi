using System.Net;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Services.Implementation
{
    public class SupplierPerformanceServiceImpl : ISupplierPerformanceService
    {
        private readonly SchoolManagementDb _context;

        public SupplierPerformanceServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        public ResponseResult<SupplierPerformanceResponseVM> RecordPerformance(SupplierPerformanceRequestVM request, int tenantId, int userId)
        {
            try
            {
                var performance = new MSupplierPerformance
                {
                    SupplierId = request.SupplierId,
                    PoId = request.PoId,
                    DeliveryDate = request.DeliveryDate,
                    ExpectedDate = request.ExpectedDate,
                    OnTimeDelivery = request.OnTimeDelivery,
                    QualityRating = request.QualityRating,
                    QuantityAccuracyPct = request.QuantityAccuracyPct,
                    Notes = request.Notes,
                    TenantId = tenantId,
                    CreatedBy = userId == 0 ? null : userId,
                    CreatedOn = DateTime.UtcNow
                };

                _context.SupplierPerformance.Add(performance);
                _context.SaveChanges();

                return GetPerformanceById(performance.Id, tenantId);
            }
            catch (Exception ex)
            {
                return new ResponseResult<SupplierPerformanceResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        private ResponseResult<SupplierPerformanceResponseVM> GetPerformanceById(int id, int tenantId)
        {
            try
            {
                var p = _context.SupplierPerformance
                    .Include(x => x.Supplier)
                    .FirstOrDefault(x => x.Id == id && x.TenantId == tenantId);

                if (p == null)
                    return new ResponseResult<SupplierPerformanceResponseVM>(HttpStatusCode.NotFound, null, "Performance record not found");

                var data = new SupplierPerformanceResponseVM
                {
                    Id = p.Id,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier?.Name ?? "Unknown",
                    PoId = p.PoId,
                    DeliveryDate = p.DeliveryDate,
                    ExpectedDate = p.ExpectedDate,
                    OnTimeDelivery = p.OnTimeDelivery,
                    DaysLate = p.DaysLate,
                    QualityRating = p.QualityRating,
                    QuantityAccuracyPct = p.QuantityAccuracyPct,
                    Notes = p.Notes,
                    CreatedOn = p.CreatedOn
                };

                return new ResponseResult<SupplierPerformanceResponseVM>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<SupplierPerformanceResponseVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<SupplierPerformanceResponseVM>> GetSupplierPerformanceHistory(int supplierId, int tenantId)
        {
            try
            {
                var history = _context.SupplierPerformance
                    .Include(x => x.Supplier)
                    .Where(x => x.SupplierId == supplierId && x.TenantId == tenantId)
                    .OrderByDescending(x => x.DeliveryDate)
                    .ToList();

                var data = history.Select(p => new SupplierPerformanceResponseVM
                {
                    Id = p.Id,
                    SupplierId = p.SupplierId,
                    SupplierName = p.Supplier?.Name ?? "Unknown",
                    PoId = p.PoId,
                    DeliveryDate = p.DeliveryDate,
                    ExpectedDate = p.ExpectedDate,
                    OnTimeDelivery = p.OnTimeDelivery,
                    DaysLate = p.DaysLate,
                    QualityRating = p.QualityRating,
                    QuantityAccuracyPct = p.QuantityAccuracyPct,
                    Notes = p.Notes,
                    CreatedOn = p.CreatedOn
                }).ToList();

                return new ResponseResult<List<SupplierPerformanceResponseVM>>(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<SupplierPerformanceResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<SupplierPerformanceSummaryVM> GetSupplierPerformanceSummary(int supplierId, int tenantId)
        {
            try
            {
                var data = _context.SupplierPerformance
                    .Where(x => x.SupplierId == supplierId && x.TenantId == tenantId)
                    .ToList();

                if (!data.Any())
                    return new ResponseResult<SupplierPerformanceSummaryVM>(HttpStatusCode.NotFound, null, "No performance data found for this supplier");

                var supplier = _context.Supplier.FirstOrDefault(s => s.Id == supplierId && s.Tenant_id == tenantId);

                var summary = new SupplierPerformanceSummaryVM
                {
                    SupplierId = supplierId,
                    SupplierName = supplier?.Name ?? "Unknown",
                    TotalDeliveries = data.Count,
                    OnTimeDeliveries = data.Count(x => x.OnTimeDelivery == true),
                    OnTimeDeliveryPercentage = data.Count > 0 ? (decimal)data.Count(x => x.OnTimeDelivery == true) / data.Count * 100 : 0,
                    AverageQualityRating = data.Any(x => x.QualityRating.HasValue) ? (decimal)data.Average(x => x.QualityRating.Value) : 0,
                    AverageQuantityAccuracy = data.Any(x => x.QuantityAccuracyPct.HasValue) ? data.Average(x => x.QuantityAccuracyPct.Value) : 0,
                    AverageDaysLate = data.Any(x => x.DaysLate.HasValue) ? (int)data.Average(x => x.DaysLate.Value) : 0
                };

                return new ResponseResult<SupplierPerformanceSummaryVM>(HttpStatusCode.OK, summary);
            }
            catch (Exception ex)
            {
                return new ResponseResult<SupplierPerformanceSummaryVM>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }

        public ResponseResult<List<SupplierPerformanceSummaryVM>> GetAllSuppliersPerformanceSummary(int tenantId)
        {
            try
            {
                var suppliers = _context.Supplier.Where(s => s.Tenant_id == tenantId).ToList();
                var summaries = new List<SupplierPerformanceSummaryVM>();

                foreach (var s in suppliers)
                {
                    var result = GetSupplierPerformanceSummary(s.Id, tenantId);
                    if (result.StatusCode == HttpStatusCode.OK && result.Data != null)
                    {
                        summaries.Add(result.Data);
                    }
                }

                return new ResponseResult<List<SupplierPerformanceSummaryVM>>(HttpStatusCode.OK, summaries);
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<SupplierPerformanceSummaryVM>>(HttpStatusCode.InternalServerError, null, $"Error: {ex.Message}");
            }
        }
    }
}
