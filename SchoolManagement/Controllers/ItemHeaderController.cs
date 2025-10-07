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
                return new ResponseResult<List<ItemHeaderResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<ItemHeaderResponseVM>>(HttpStatusCode.OK, response, "Item Headers fetched successfully");
        }
    }
}
