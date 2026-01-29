using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryExplorerController : ControllerBase
    {
        private readonly IInventoryExplorerService _service;

        public InventoryExplorerController(IInventoryExplorerService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public ResponseResult<List<InventoryItemVM>> GetList([FromQuery] int? branchId, [FromQuery] int? categoryId, [FromQuery] string? searchTerm)
        {
            int tenantId = 1;
            return _service.GetInventoryList(tenantId, branchId, categoryId, searchTerm);
        }

        [HttpGet("detail/{itemId}/{branchId}")]
        public ResponseResult<InventoryItemVM> GetDetail(int itemId, int branchId)
        {
            int tenantId = 1;
            return _service.GetInventoryDetail(itemId, branchId, tenantId);
        }
    }
}
