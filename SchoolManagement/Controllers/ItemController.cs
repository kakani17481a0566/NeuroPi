using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using SchoolManagement.ViewModel;
using SchoolManagement.ViewModel.Master;
using SchoolManagement.ViewModel.Item;



namespace SchoolManagement.Controllers

// Developed by: Lekhan

{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService ItemService;
        public ItemController(IItemService _ItemService)
        {
            ItemService = _ItemService;

        }

        //Get all Items
        //GET: api/item
        [HttpGet]
        public ResponseResult<List<ItemVM>> GetAll()
        {
            var response = ItemService.GetAll();
            if (response == null)
            {
                return new ResponseResult<List<ItemVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemVM>>(HttpStatusCode.OK, response, "Item fetched successfully");
        }
        [HttpGet("{id}")]
        public ResponseResult<ItemVM> GetById([FromRoute] int id)
        {
            var response = ItemService.GetById(id);
            if (response != null)
            {
                return new ResponseResult<ItemVM>(HttpStatusCode.OK, response, "Item is fetched successfully");
            }
            return new ResponseResult<ItemVM>(HttpStatusCode.BadGateway, response, $" Item not found with id {id}");
        }

        [HttpGet("Item/tenantId")]
        public ResponseResult<ItemVM> GetByIdAndTenantId([FromQuery(Name = "id")] int id, [FromQuery(Name = "tenantId")] int tenantId)
        {
            var response = ItemService.GetByIdAndTenantId(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<ItemVM>(HttpStatusCode.OK, response, "Item is fetched successfully");
            }
            return new ResponseResult<ItemVM>(HttpStatusCode.NotFound, response, $" Item not found with id {id} and tenantid {tenantId}");
        }

        [HttpGet("Item/tenant/{id}")]
        public ResponseResult<List<ItemVM>> GetAllItems([FromRoute] int id)
        {
            var response =ItemService.GetAllByTenantId(id);

            if (response == null)
            {
                return new ResponseResult<List<ItemVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemVM>>(HttpStatusCode.OK, response, "items fetched successfully");
        }


        [HttpDelete("/Item/{id}/{tenantId}")]
        public ResponseResult<ItemVM> DeleteByIdAndTenantId([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = ItemService.DeleteByIdAndTenantId(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<ItemVM>(HttpStatusCode.OK, response, "Deleted Successfully");
            }
            return new ResponseResult<ItemVM>(HttpStatusCode.BadRequest, response, $"No Data found with Id {id}");

        }
        [HttpPost]
        public ResponseResult<ItemVM> AddItem([FromBody] ItemRequestVM request)
        {
            var response = ItemService.CreateItem(request);
            if (response != null)
            {
                return new ResponseResult<ItemVM>(HttpStatusCode.OK, response, "created  successfully");
            }
            return new ResponseResult<ItemVM>(HttpStatusCode.BadRequest, response, " not created");
        }
        
    }
}
