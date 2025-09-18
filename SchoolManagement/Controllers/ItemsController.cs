using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Items;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemService;
        public ItemsController(IItemsService itemService)
        {
            _itemService = itemService;
        }

        //Get all Items
        //GET: api/items
        [HttpGet]
       public ResponseResult<List<ItemsResponseVM>> GetAll()
        {
            var response = _itemService.GetAllItems();
            if (response == null)
            {
                return new ResponseResult<List<ItemsResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<ItemsResponseVM>>(HttpStatusCode.OK, response, "Items fetched successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<ItemsResponseVM> GetById([FromRoute] int id)
        {
            var response = _itemService.GetItemsById(id);
            if (response != null)
            {
                return new ResponseResult<ItemsResponseVM>(HttpStatusCode.OK, response, "Item is fetched successfully");
            }
            return new ResponseResult<ItemsResponseVM>(HttpStatusCode.BadGateway, response, $" Item not found with id {id}");
        }

        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<List<ItemsResponseVM>> GetByTenantId([FromRoute] int tenantId)
        {
            var response = _itemService.GetItemsByTenant(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<ItemsResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<ItemsResponseVM>>(HttpStatusCode.OK, response, "Items fetched successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<ItemsResponseVM> GetByIdAndTenantId( int id, int tenantId)
        {
            var response = _itemService.GetItemsByIdAndTenant(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<ItemsResponseVM>(HttpStatusCode.OK, response, "Item is fetched successfully");
            }
            return new ResponseResult<ItemsResponseVM>(HttpStatusCode.NotFound, response, $" Item not found with id {id} and tenantid {tenantId}");
        }

        //Create new Item
        //POST: api/items
        [HttpPost]
        public ResponseResult<ItemsResponseVM> Create([FromBody] ItemsRequestVM itemsRequestVM)
        {
            if (itemsRequestVM == null)
            {
                return new ResponseResult<ItemsResponseVM>(HttpStatusCode.BadRequest, null, "Invalid item data");
            }
            var createdItem = _itemService.CreateItems(itemsRequestVM);
            return new ResponseResult<ItemsResponseVM>(HttpStatusCode.Created, createdItem, "Item created successfully");
        }

        //Update Item
        //PUT: api/items/{id}/{tenantId}
        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ItemsResponseVM> Update([FromRoute] int id, [FromRoute] int tenantId, [FromBody] ItemsUpdateVM itemsUpdateVM)
        {
            if (itemsUpdateVM == null)
            {
                return new ResponseResult<ItemsResponseVM>(HttpStatusCode.BadRequest, null, "Invalid item data");
            }
            var updatedItem = _itemService.UpdateItems(id, tenantId, itemsUpdateVM);
            if (updatedItem == null)
            {
                return new ResponseResult<ItemsResponseVM>(HttpStatusCode.NotFound, null, $"Item not found with id {id} and tenantId {tenantId}");
            }
            return new ResponseResult<ItemsResponseVM>(HttpStatusCode.OK, updatedItem, "Item updated successfully");
        }

        //Delete Item
        //DELETE: api/items/{id}/{tenantId}
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> Delete([FromRoute] int id, [FromRoute] int tenantId)
        {
            var isDeleted = _itemService.DeleteItemsByIdAndTenant(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, $"Item not found with id {id} and tenantId {tenantId}");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Item deleted successfully");
        }

        [HttpPost("with-group")]
        public ResponseResult<ItemsResponseVM> CreateWithGroup([FromBody] ItemInsertVM itemInsertVM)
        {
            if (itemInsertVM == null)
            {
                return new ResponseResult<ItemsResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid item data"
                );
            }

            try
            {
                // 🔹 Option 1: If you want CreatedBy from request body
                var createdItem = _itemService.CreateItemWithGroup(itemInsertVM);

                // 🔹 Option 2 (better): Auto-fill CreatedBy from logged-in user claims
                // var userId = int.Parse(User.FindFirst("userId").Value);
                // itemInsertVM.CreatedBy = userId;
                // var createdItem = _itemService.CreateItemWithGroup(itemInsertVM);

                return new ResponseResult<ItemsResponseVM>(
                    HttpStatusCode.Created,
                    createdItem,
                    "Item with group created successfully"
                );
            }
            catch (ArgumentException ex)
            {
                return new ResponseResult<ItemsResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<ItemsResponseVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    $"Failed to create item with group: {ex.Message}"
                );
            }
        }


        [HttpGet("{id}/tenant/{tenantId}/details")]
        public ResponseResult<ItemWithGroupResponseVM> GetItemDetailsWithGroup(int id, int tenantId)
        {
            var result = _itemService.GetItemWithGroup(id, tenantId);

            if (result == null)
            {
                return new ResponseResult<ItemWithGroupResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    $"Item with id {id} and tenant {tenantId} not found"
                );
            }

            return new ResponseResult<ItemWithGroupResponseVM>(
                HttpStatusCode.OK,
                result,
                result.IsGroup
                    ? "Group item with children fetched successfully"
                    : "Single item fetched successfully"
            );
        }



    }
}
