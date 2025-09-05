using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.OrderItem;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public ResponseResult<List<OrderItemResponseVM>> GetAllOrderItems()
        {
            var response = _orderItemService.GetAllOrderItems();
            if (response == null)
            {
                return new ResponseResult<List<OrderItemResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<OrderItemResponseVM>>(HttpStatusCode.OK, response, "OrderItems fetched successfully");

        }

        [HttpGet("{id}")]
        public ResponseResult<OrderItemResponseVM> GetOrderItemById(int id)
        { 
            var response = _orderItemService.GetOrderItemById(id);
            if (response != null)
                {
                return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.OK, response,"Order Item fetched successfully");
            }
            return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.NotFound, response, $" Order Item not found with id {id}");

        }

        [HttpGet("GetByTenant/{tenantId}")]
        public ResponseResult<List<OrderItemResponseVM>> GetOrderItemsByTenant(int tenantId)
        {
            var response = _orderItemService.GetOrderItemsByTenant(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<OrderItemResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<OrderItemResponseVM>>(HttpStatusCode.OK, response, "Order Items fetched successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<OrderItemResponseVM> GetOrderItemByIdAndTenant(int id, int tenantId)
        {
            var response = _orderItemService.GetOrderItemByIdAndTenant(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.OK, response, "Order Item fetched successfully");
            }
            return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.NotFound, response, $" Order Item not found with id {id} for the specified tenant {tenantId}");
        }
        [HttpPost]
        public ResponseResult<OrderItemResponseVM> CreateOrderItem([FromBody] OrderItemRequestVM orderItemRequestVM)
        {
            if (orderItemRequestVM == null)
            {
                return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.BadRequest, null, "Invalid Order Item data");
            }
            var response = _orderItemService.CreateOrderItem(orderItemRequestVM);
            return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.Created, response, "Order Item created successfully");
        }
        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<OrderItemResponseVM> UpdateOrderItem(int id, int tenantId, [FromBody] OrderItemUpdateVM orderItemUpdateVM)
        {
            if (orderItemUpdateVM == null)
            {
                return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.BadRequest, null, "Invalid Order Item data");
            }
            var response = _orderItemService.UpdateOrderItem(id, tenantId, orderItemUpdateVM);
            if (response != null)
            {
                return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.OK, response, "Order Item updated successfully");
            }
            return new ResponseResult<OrderItemResponseVM>(HttpStatusCode.NotFound, response, $" Order Item not found with id {id} for the specified tenant {tenantId}");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteOrderItemByIdAndTenant(int id, int tenantId)
        {
            var isDeleted = _orderItemService.DeleteOrderItemByIdAndTenant(id, tenantId);
            if (isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, isDeleted, "Order Item deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, isDeleted, $" Order Item not found with id {id} for the specified tenant {tenantId}");
        }
    }
}
