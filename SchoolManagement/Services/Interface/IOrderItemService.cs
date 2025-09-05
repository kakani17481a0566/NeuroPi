using SchoolManagement.ViewModel.OrderItem;

namespace SchoolManagement.Services.Interface
{
    public interface IOrderItemService
    {
        List<OrderItemResponseVM> GetAllOrderItems();
        List<OrderItemResponseVM> GetOrderItemsByTenant(int tenantId);
        OrderItemResponseVM GetOrderItemById(int id);
        OrderItemResponseVM GetOrderItemByIdAndTenant(int id, int tenantId);
        OrderItemResponseVM CreateOrderItem(OrderItemRequestVM orderItemRequestVM);
        OrderItemResponseVM UpdateOrderItem(int id, int tenantId, OrderItemUpdateVM orderItemUpdateVM);
        bool DeleteOrderItemByIdAndTenant(int id, int tenantId);


    }
}
