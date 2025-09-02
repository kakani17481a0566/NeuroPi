using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemLocation;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemLocationController : ControllerBase
    {
        private readonly IItemLocationService _itemLocationService;
        public ItemLocationController(IItemLocationService itemLocationService)
        {
            _itemLocationService = itemLocationService;
        }

        [HttpGet]
        public ResponseResult<List<ItemLocationResponseVM>> GetAll()
        {
            var response = _itemLocationService.GetItemLocations();
            if (response == null)
            {
                return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.OK, response, "Item Locations fetched successfully");
        }

        [HttpGet("{branchId}")]
        public ResponseResult<ItemLocationResponseVM> GetById(int branchId)
        {
            var response = _itemLocationService.GetItemLocationById(branchId);
            if (response != null)
            {
                return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.OK, response, "Item Location is fetched successfully");
            }
            return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.BadGateway, response, $" Item Location not found with id {branchId}");
        }

        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<List<ItemLocationResponseVM>> GetByTenantId(int tenantId)
        {
            var response = _itemLocationService.ItemLocationByTenantId(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.OK, response, "Item Locations fetched successfully");
        }

        [HttpGet("{branchId}/{tenantId}")]
        public ResponseResult<List<ItemLocationResponseVM>> GetByIdAndTenantId(int branchId, int tenantId)
        {
            var response = _itemLocationService.GetItemLocationById(branchId, tenantId);
            if (response != null)
            {
                return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.OK, response, "Item Location is fetched successfully");
            }
            return new ResponseResult<List<ItemLocationResponseVM>>(HttpStatusCode.BadGateway, response, $" Item Location not found with id {branchId} for the specified tenant {tenantId}");
        }

        [HttpPost]
        public ResponseResult<ItemLocationResponseVM> Create([FromBody] ItemLocationRequestVM itemLocationRequestVM)
        {
            var response = _itemLocationService.CreateItemLocation(itemLocationRequestVM);
            if (response != null)
            {
                return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.OK, response, "Item Location is created successfully");
            }
            return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.BadGateway, response, "Item Location creation failed");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ItemLocationResponseVM> Update(int id, int tenantId, [FromBody] ItemLocationUpdateVM itemLocationUpdateVM)
        {
            var response = _itemLocationService.UpdateItemLocation(id, tenantId, itemLocationUpdateVM);
            if (response != null)
            {
                return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.OK, response, "Item Location is updated successfully");
            }
            return new ResponseResult<ItemLocationResponseVM>(HttpStatusCode.BadGateway, response, $" Item Location not found with id {id} for the specified tenant {tenantId}");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var response = _itemLocationService.DeleteItemLocation(id, tenantId);
            if (response)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, response, "Item Location is deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.BadGateway, response, $" Item Location not found with id {id} for the specified tenant {tenantId}");
        }


    }
}
