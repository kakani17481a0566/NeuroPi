using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostHistoryController : ControllerBase
    {
        private readonly ICostHistoryService _service;

        public CostHistoryController(ICostHistoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<CostHistoryResponseVM> RecordCost([FromBody] CostHistoryRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.RecordCost(request, tenantId, userId);
        }

        [HttpGet("current")]
        public ResponseResult<decimal> GetCurrentCost([FromQuery] int itemId, [FromQuery] int? branchId, [FromQuery] string costType)
        {
            int tenantId = 1;
            return _service.GetCurrentCost(itemId, branchId, costType, tenantId);
        }

        [HttpGet("history")]
        public ResponseResult<List<CostHistoryResponseVM>> GetHistory([FromQuery] int itemId, [FromQuery] int? branchId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            int tenantId = 1;
            return _service.GetCostHistory(itemId, branchId, tenantId, fromDate, toDate);
        }

        [HttpGet("trends")]
        public ResponseResult<List<CostTrendVM>> GetTrends([FromQuery] int? branchId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            int tenantId = 1;
            return _service.GetCostTrends(tenantId, branchId, fromDate, toDate);
        }
    }
}
