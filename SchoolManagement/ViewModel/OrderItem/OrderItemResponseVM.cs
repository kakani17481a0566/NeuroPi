using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.OrderItem
{
    public class OrderItemResponseVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }

        // numeric -> decimal
        public decimal OrderQuantity { get; set; }
        public decimal DeliveredQuantity { get; set; }
        public decimal UnitPrice { get; set; }

        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        // timestamp with time zone -> DateTimeOffset
        public DateTimeOffset CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }

        public static OrderItemResponseVM ToViewModel(MOrderItem orderItem)
        {
            return new OrderItemResponseVM
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ItemId = orderItem.ItemId,
                OrderQuantity = orderItem.OrderQuantity,
                DeliveredQuantity = orderItem.DeliveredQuantity,
                UnitPrice = orderItem.UnitPrice,
                TenantId = orderItem.TenantId,
                CreatedBy = orderItem.CreatedBy,
                CreatedOn = orderItem.CreatedOn,
                UpdatedBy = orderItem.UpdatedBy,
                UpdatedOn = orderItem.UpdatedOn
            };
        }

        public List<OrderItemResponseVM> ToViewModelList(List<MOrderItem> orderItemList)
        {
            var result = new List<OrderItemResponseVM>();
            foreach (var orderItem in orderItemList)
            {
                result.Add(ToViewModel(orderItem));
            }
            return result;
        }
    }
}
