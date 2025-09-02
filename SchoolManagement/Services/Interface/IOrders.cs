using SchoolManagement.ViewModel.Orders;

namespace SchoolManagement.Services.Interface
{
    public interface IOrders
    {
        List<OrderResponseVM> GetAllOrders();
        OrderResponseVM? GetOrderById(int id);
        List<OrderResponseVM> GetBySupplier(int supplierId);
        OrderResponseVM CreateOrder(OrderCreateVM vm);
        OrderResponseVM? UpdateOrder(int id, OrderRequestVM vm);
        bool DeleteOrder(int id, int deleterUserId);
    }
}
