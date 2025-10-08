using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.PostalDeliveryItems
{
    public class PostalDeliveryItemsRequestVM
    {
        public int DeliveryId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public int TenantId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }

        public MPostalDeliveryItems ToModel(PostalDeliveryItemsRequestVM pdiRequestVM)
        {
            return new MPostalDeliveryItems
            {
                DeliveryId = this.DeliveryId,
                ItemId = this.ItemId,
                ItemName = this.ItemName,
                Quantity = this.Quantity,
                UnitCost = this.UnitCost,
                TenantId = this.TenantId,
                CreatedOn = this.CreatedOn,
                CreatedBy = this.CreatedBy
            };
        }
    }
}
