using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockAdjustmentReasonController : ControllerBase
    {
        private readonly IStockAdjustmentReasonService _service;

        public StockAdjustmentReasonController(IStockAdjustmentReasonService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<StockAdjustmentReasonResponseVM> Create([FromBody] StockAdjustmentReasonRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.CreateReason(request, tenantId, userId);
        }

        [HttpGet("{id}")]
        public ResponseResult<StockAdjustmentReasonResponseVM> GetById(int id)
        {
            int tenantId = 1;
            return _service.GetReasonById(id, tenantId);
        }

        [HttpGet]
        public ResponseResult<List<StockAdjustmentReasonResponseVM>> GetAll([FromQuery] string? adjustmentType)
        {
            int tenantId = 1;
            return _service.GetAllReasons(tenantId, adjustmentType);
        }

        [HttpPut("{id}")]
        public ResponseResult<StockAdjustmentReasonResponseVM> Update(int id, [FromBody] StockAdjustmentReasonRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.UpdateReason(id, request, tenantId, userId);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.DeleteReason(id, tenantId, userId);
        }
    }
}
