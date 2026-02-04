using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using NeuroPi.CommonLib.Model;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemUomController : ControllerBase
    {
        private readonly IItemUomService _service;

        public ItemUomController(IItemUomService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<ItemUomResponseVM> Create([FromBody] ItemUomRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.CreateUom(request, tenantId, userId);
        }

        [HttpGet("{id}")]
        public ResponseResult<ItemUomResponseVM> GetById(int id)
        {
            int tenantId = 1;
            return _service.GetUomById(id, tenantId);
        }

        [HttpGet("item/{itemId}")]
        public ResponseResult<List<ItemUomResponseVM>> GetByItemId(int itemId)
        {
            int tenantId = 1;
            return _service.GetUomsByItemId(itemId, tenantId);
        }

        [HttpPut("{id}")]
        public ResponseResult<ItemUomResponseVM> Update(int id, [FromBody] ItemUomRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.UpdateUom(id, request, tenantId, userId);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.DeleteUom(id, tenantId, userId);
        }

        [HttpGet("convert")]
        public ResponseResult<decimal> Convert([FromQuery] int itemId, [FromQuery] string fromUom, [FromQuery] string toUom, [FromQuery] decimal quantity)
        {
            int tenantId = 1;
            return _service.ConvertQuantity(itemId, fromUom, toUom, quantity, tenantId);
        }
    }
}
