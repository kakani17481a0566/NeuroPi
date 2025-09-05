using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.OrderItem
{
    public class OrderItemRequestVM
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int OrderQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int UnitPrice { get; set; }
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }


        public MOrderItem ToModel()
        {
            return new MOrderItem
            {
                OrderId = this.OrderId,
                ItemId = this.ItemId,
                OrderQuantity = this.OrderQuantity,
                DeliveredQuantity = this.DeliveredQuantity,
                UnitPrice = this.UnitPrice,
                TenantId = this.TenantId,
                CreatedBy = this.CreatedBy,
                CreatedOn = this.CreatedOn
            };
        }
    }
}

