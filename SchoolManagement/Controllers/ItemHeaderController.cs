using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemHeader;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHeaderController : ControllerBase
    {
        private readonly IItemHeaderService _itemHeaderService;

        public ItemHeaderController(IItemHeaderService itemHeaderService)
        {
            _itemHeaderService = itemHeaderService;
        }

        [HttpGet("{tenantId}")]
        public ResponseResult<List<ItemHeaderResponseVM>> GetAllByTenantId([FromRoute] int tenantId)
        {
            var response = _itemHeaderService.GetAllByTenantId(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<ItemHeaderResponseVM>>(HttpStatusCode.OK, new List<ItemHeaderResponseVM>(), "No data Found");
            }
            return new ResponseResult<List<ItemHeaderResponseVM>>(HttpStatusCode.OK, response, "Item Headers fetched successfully");
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemHeaderRequestVM request)
        {
            var result = _itemHeaderService.CreateItemHeader(request);
            return new ResponseResult<ItemHeaderResponseVM>(HttpStatusCode.Created, result, "Book created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ItemHeaderRequestVM request)
        {
            var result = _itemHeaderService.UpdateItemHeader(id, request);
            if (result == null)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Book not found");
            }
            return new ResponseResult<ItemHeaderResponseVM>(HttpStatusCode.OK, result, "Book updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _itemHeaderService.DeleteItemHeader(id);
             if (!result)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Book not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, result, "Book deleted successfully");
        }
    }
}
