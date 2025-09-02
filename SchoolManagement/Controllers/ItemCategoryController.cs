using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemCategory;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _itemCategoryService;
        public ItemCategoryController(IItemCategoryService itemCategoryService)
        {
            _itemCategoryService = itemCategoryService;
        }

        [HttpGet]
        public ResponseResult<List<ItemCategoryResponseVM>> GetAll()
        {
            var response = _itemCategoryService.GetAllItemsCategory();
            if (response == null)
            {
                return new ResponseResult<List<ItemCategoryResponseVM>>(System.Net.HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemCategoryResponseVM>>(System.Net.HttpStatusCode.OK, response, "Item Categories fetched successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<ItemCategoryResponseVM> GetById([FromRoute] int id)
        {
            var response = _itemCategoryService.GetItemCategoryById(id);
            if (response != null)
            {
                return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.OK, response, "Item Category is fetched successfully");
            }
            return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.BadGateway, response, $" Item Category not found with id {id}");
        }

        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<List<ItemCategoryResponseVM>> GetByTenantId([FromRoute] int tenantId)
        {
            var response = _itemCategoryService.GetItemCategoryByTenantId(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<ItemCategoryResponseVM>>(System.Net.HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<ItemCategoryResponseVM>>(System.Net.HttpStatusCode.OK, response, "Item Categories fetched successfully");
        }

        [HttpGet("GetByIdAndTenant/{id}/{tenantId}")]
        public ResponseResult<ItemCategoryResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var response = _itemCategoryService.GetItemCategoryByTenantIdAndId(tenantId, id);
            if (response != null)
            {
                return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.OK, response, "Item Category is fetched successfully");
            }
            return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.BadGateway, response, $" Item Category not found with id {id} for the specified tenant {tenantId}");
        }

        [HttpPost]
        public ResponseResult<ItemCategoryResponseVM> Create([FromBody] ItemCategoryRequestVM itemCategoryRequestVM)
        {
            var response = _itemCategoryService.CreateItemCategory(itemCategoryRequestVM);
            if (response != null)
            {
                return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.OK, response, "Item Category is created successfully");
            }
            return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.BadGateway, response, "Item Category creation failed");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ItemCategoryResponseVM> Update([FromBody] ItemCategoryUpdateVM itemCategoryUpdateVM, int id, int tenantId)
        {
           if(!ModelState.IsValid)
                {
                return new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.BadRequest, null, "Invalid data");
            }
            var response = _itemCategoryService.UpdateItemCategory(id, tenantId, itemCategoryUpdateVM);
            return response != null
                ? new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.OK, response, "Item Category is updated successfully")
                : new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.NotFound, response, "Item Category not found");

        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<ItemCategoryResponseVM> Delete(int id, int tenantId)
        {
            var response = _itemCategoryService.DeleteItemCategory(id, tenantId);
            return response != null
                ? new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.OK, response, "Item Category is deleted successfully")
                : new ResponseResult<ItemCategoryResponseVM>(System.Net.HttpStatusCode.NotFound, response, "Item Category not found");
        }
    }
}
