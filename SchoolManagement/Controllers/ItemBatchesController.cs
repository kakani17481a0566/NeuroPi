using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Inventory;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemBatchesController : ControllerBase
    {
        private readonly IItemBatchesService _service;

        public ItemBatchesController(IItemBatchesService service)
        {
            _service = service;
        }

        [HttpPost]
        public ResponseResult<ItemBatchResponseVM> Create([FromBody] ItemBatchRequestVM request)
        {
            // TODO: dynamic tenant/user from claims
            int tenantId = 1;
            int userId = 1;
            return _service.CreateBatch(request, tenantId, userId);
        }

        [HttpPut("{id}")]
        public ResponseResult<ItemBatchResponseVM> Update([FromRoute] int id, [FromBody] ItemBatchRequestVM request)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.UpdateBatch(id, request, tenantId, userId);
        }

        [HttpGet("{id}")]
        public ResponseResult<ItemBatchResponseVM> GetById([FromRoute] int id)
        {
            int tenantId = 1;
            return _service.GetBatchById(id, tenantId);
        }

        [HttpGet("item/{itemId}")]
        public ResponseResult<List<ItemBatchResponseVM>> GetByItem([FromRoute] int itemId)
        {
            int tenantId = 1;
            return _service.GetBatchesByItem(itemId, tenantId);
        }

        [HttpGet("branch/{branchId}")]
        public ResponseResult<List<ItemBatchResponseVM>> GetByBranch([FromRoute] int branchId)
        {
            int tenantId = 1;
            return _service.GetBatchesByBranch(branchId, tenantId);
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete([FromRoute] int id)
        {
            int tenantId = 1;
            int userId = 1;
            return _service.DeleteBatch(id, tenantId, userId);
        }
    }
}
