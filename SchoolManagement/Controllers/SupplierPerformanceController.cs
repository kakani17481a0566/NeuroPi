using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierPerformanceController : ControllerBase
    {
        private readonly ISupplierPerformanceService _service;

        public SupplierPerformanceController(ISupplierPerformanceService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<SupplierPerformanceResponseVM> RecordPerformance([FromBody] SupplierPerformanceRequestVM request, [FromQuery] int tenantId, [FromQuery] int userId)
        {
            return _service.RecordPerformance(request, tenantId, userId);
        }

        [HttpGet("supplier/{supplierId}")]
        public ResponseResult<List<SupplierPerformanceResponseVM>> GetBySupplier(int supplierId, [FromQuery] int tenantId)
        {
            return _service.GetSupplierPerformanceHistory(supplierId, tenantId);
        }

        [HttpGet("supplier/{supplierId}/summary")]
        public ResponseResult<SupplierPerformanceSummaryVM> GetSummary(int supplierId, [FromQuery] int tenantId)
        {
            return _service.GetSupplierPerformanceSummary(supplierId, tenantId);
        }

        [HttpGet("summary-all")]
        public ResponseResult<List<SupplierPerformanceSummaryVM>> GetAllSummaries([FromQuery] int tenantId)
        {
            return _service.GetAllSuppliersPerformanceSummary(tenantId);
        }
    }
}
