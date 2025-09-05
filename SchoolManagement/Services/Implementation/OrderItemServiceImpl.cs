using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.OrderItem;

namespace SchoolManagement.Services.Implementation
{
    public class OrderItemServiceImpl : IOrderItemService
    {
        private readonly SchoolManagementDb _context;
        public OrderItemServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public OrderItemResponseVM CreateOrderItem(OrderItemRequestVM orderItemRequestVM)
        {
            if (orderItemRequestVM == null)
                throw new ArgumentNullException(nameof(orderItemRequestVM));

            // ✅ Verify parent order exists using the primary key (no property names needed)
            var order = _context.Orders.Find(orderItemRequestVM.OrderId);
            if (order == null)
                throw new InvalidOperationException($"Order {orderItemRequestVM.OrderId} not found.");

            // Map and ensure FKs are set explicitly
            var newOrderItem = orderItemRequestVM.ToModel();
            newOrderItem.OrderId = orderItemRequestVM.OrderId;
            newOrderItem.ItemId = orderItemRequestVM.ItemId;
            newOrderItem.TenantId = orderItemRequestVM.TenantId;

            // keep your existing timestamp style
            newOrderItem.CreatedOn = DateTime.UtcNow;

            _context.OrderItem.Add(newOrderItem);
            _context.SaveChanges();

            return OrderItemResponseVM.ToViewModel(newOrderItem);
        }

        public bool DeleteOrderItemByIdAndTenant(int id, int tenantId)
        {
            var orderItem = _context.OrderItem.FirstOrDefault(oi => !oi.IsDeleted && oi.Id == id && oi.TenantId == tenantId);
            if (orderItem == null) return false;
            orderItem.IsDeleted = true;
            orderItem.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;

        }

        public List<OrderItemResponseVM> GetAllOrderItems()
        {
            return _context.OrderItem
                .Where(oi => !oi.IsDeleted)
                .Select(oi => new OrderItemResponseVM
                {
                    Id = oi.Id,
                    OrderId = oi.OrderId,
                    ItemId = oi.ItemId,
                    OrderQuantity = oi.OrderQuantity,
                    DeliveredQuantity = oi.DeliveredQuantity,
                    UnitPrice = oi.UnitPrice,
                    TenantId = oi.TenantId,
                    CreatedBy = oi.CreatedBy,
                    CreatedOn = oi.CreatedOn,
                    UpdatedBy = oi.UpdatedBy,
                    UpdatedOn = oi.UpdatedOn
                }).ToList();
        }

        public OrderItemResponseVM GetOrderItemById(int id)
        {
            var orderItem = _context.OrderItem.FirstOrDefault(oi => !oi.IsDeleted && oi.Id == id);
            if (orderItem == null) return null;
            return OrderItemResponseVM.ToViewModel(orderItem);
            
        }

        public OrderItemResponseVM GetOrderItemByIdAndTenant(int id, int tenantId)
        {
            var orderItem = _context.OrderItem.FirstOrDefault(oi => !oi.IsDeleted && oi.Id == id && oi.TenantId == tenantId);
            if (orderItem == null) return null;
            return OrderItemResponseVM.ToViewModel(orderItem);
        }

        public List<OrderItemResponseVM> GetOrderItemsByTenant(int tenantId)
        {
            var orderItems = _context.OrderItem
                .Where(oi => !oi.IsDeleted && oi.TenantId == tenantId)
                .ToList();
            return new OrderItemResponseVM().ToViewModelList(orderItems);
        }

        public OrderItemResponseVM UpdateOrderItem(int id, int tenantId, OrderItemUpdateVM orderItemUpdateVM)
        {
            var existingOrderItem = _context.OrderItem.FirstOrDefault(oi => !oi.IsDeleted && oi.Id == id && oi.TenantId == tenantId);
            if (existingOrderItem == null) return null;
            existingOrderItem.ItemId = orderItemUpdateVM.ItemId;
            existingOrderItem.OrderQuantity = orderItemUpdateVM.OrderQuantity;
            existingOrderItem.DeliveredQuantity = orderItemUpdateVM.DeliveredQuantity;
            existingOrderItem.UnitPrice = orderItemUpdateVM.UnitPrice;
            existingOrderItem.UpdatedBy = orderItemUpdateVM.UpdatedBy;
            existingOrderItem.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return OrderItemResponseVM.ToViewModel(existingOrderItem);

        }
    }
}
