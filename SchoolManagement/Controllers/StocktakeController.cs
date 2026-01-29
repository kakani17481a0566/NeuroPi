using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocktakeController : ControllerBase
    {
        private readonly IStocktakeService _service;

        public StocktakeController(IStocktakeService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<StocktakeHeaderResponseVM> Create([FromBody] StocktakeHeaderRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.CreateStocktake(request, tenantId, userId);
        }

        [HttpGet("{id}")]
        public ResponseResult<StocktakeHeaderResponseVM> GetById(int id)
        {
            int tenantId = 1;
            return _service.GetStocktakeById(id, tenantId);
        }

        [HttpGet]
        public ResponseResult<List<StocktakeHeaderResponseVM>> GetAll([FromQuery] int? branchId, [FromQuery] string? status)
        {
            int tenantId = 1;
            return _service.GetAllStocktakes(tenantId, branchId, status);
        }

        [HttpPut("{id}")]
        public ResponseResult<StocktakeHeaderResponseVM> Update(int id, [FromBody] StocktakeHeaderRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.UpdateStocktake(id, request, tenantId, userId);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.DeleteStocktake(id, tenantId, userId);
        }

        [HttpPost("lines")]
        public ResponseResult<StocktakeLineResponseVM> AddLine([FromBody] StocktakeLineRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.AddStocktakeLine(request, tenantId, userId);
        }

        [HttpGet("{id}/lines")]
        public ResponseResult<List<StocktakeLineResponseVM>> GetLines(int id)
        {
            int tenantId = 1;
            return _service.GetStocktakeLines(id, tenantId);
        }

        [HttpPost("complete/{id}")]
        public ResponseResult<bool> Complete(int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.CompleteStocktake(id, tenantId, userId);
        }

        [HttpPost("approve")]
        public ResponseResult<bool> Approve([FromBody] ApproveStocktakeRequestVM request)
        {
            int tenantId = 1;
            return _service.ApproveStocktake(request, tenantId);
        }

        [HttpPost("post/{id}")]
        public ResponseResult<bool> PostToInventory(int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.PostStocktakeToInventory(id, tenantId, userId);
        }

        [HttpGet("{id}/variance-report")]
        public ResponseResult<List<StocktakeLineResponseVM>> GetVarianceReport(int id)
        {
            int tenantId = 1;
            return _service.GetVarianceReport(id, tenantId);
        }

        [HttpGet("{id}/summary")]
        public ResponseResult<StocktakeSummaryVM> GetSummary(int id)
        {
            int tenantId = 1;
            return _service.GetStocktakeSummary(id, tenantId);
        }
    }
}
