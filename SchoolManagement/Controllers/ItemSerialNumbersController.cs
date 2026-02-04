using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemSerialNumbersController : ControllerBase
    {
        private readonly IItemSerialNumbersService _service;

        public ItemSerialNumbersController(IItemSerialNumbersService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<ItemSerialNumberResponseVM> Create([FromBody] ItemSerialNumberRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.CreateSerialNumber(request, tenantId, userId);
        }

        [HttpPut("{id}")]
        public ResponseResult<ItemSerialNumberResponseVM> Update([FromRoute] int id, [FromBody] ItemSerialNumberRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.UpdateSerialNumber(id, request, tenantId, userId);
        }

        [HttpGet("{id}")]
        public ResponseResult<ItemSerialNumberResponseVM> GetById([FromRoute] int id)
        {
            int tenantId = 1;
            return _service.GetSerialNumberById(id, tenantId);
        }

        [HttpGet("item/{itemId}")]
        public ResponseResult<List<ItemSerialNumberResponseVM>> GetByItem([FromRoute] int itemId)
        {
            int tenantId = 1;
            return _service.GetSerialNumbersByItem(itemId, tenantId);
        }

        [HttpGet("batch/{batchId}")]
        public ResponseResult<List<ItemSerialNumberResponseVM>> GetByBatch([FromRoute] int batchId)
        {
            int tenantId = 1;
            return _service.GetSerialNumbersByBatch(batchId, tenantId);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete([FromRoute] int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.DeleteSerialNumber(id, tenantId, userId);
        }

        [HttpGet("validate")]
        public ResponseResult<bool> Validate([FromQuery] string serialNumber, [FromQuery] int itemId)
        {
            int tenantId = 1;
            return _service.ValidateSerialNumber(serialNumber, itemId, tenantId);
        }
    }
}
