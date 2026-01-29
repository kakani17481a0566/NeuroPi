using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockTransactionLogController : ControllerBase
    {
        private readonly IStockTransactionLogService _service;

        public StockTransactionLogController(IStockTransactionLogService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<StockTransactionLogResponseVM> LogTransaction([FromBody] StockTransactionLogRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.LogTransaction(request, tenantId, userId);
        }

        [HttpGet("history")]
        public ResponseResult<List<StockTransactionLogResponseVM>> GetHistory([FromQuery] int itemId, [FromQuery] int? branchId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            int tenantId = 1;
            return _service.GetTransactionHistory(itemId, branchId, tenantId, fromDate, toDate);
        }

        [HttpGet("type")]
        public ResponseResult<List<StockTransactionLogResponseVM>> GetByType([FromQuery] string transactionType, [FromQuery] int? branchId, [FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            int tenantId = 1;
            return _service.GetTransactionsByType(transactionType, tenantId, branchId, fromDate, toDate);
        }

        [HttpGet("stock")]
        public ResponseResult<int> GetStock([FromQuery] int itemId, [FromQuery] int branchId)
        {
            int tenantId = 1;
            return _service.GetCurrentStock(itemId, branchId, tenantId);
        }

        [HttpGet("summary")]
        public ResponseResult<StockMovementSummaryVM> GetSummary([FromQuery] int itemId, [FromQuery] int branchId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            int tenantId = 1;
            return _service.GetStockMovementSummary(itemId, branchId, tenantId, fromDate, toDate);
        }
    }
}
