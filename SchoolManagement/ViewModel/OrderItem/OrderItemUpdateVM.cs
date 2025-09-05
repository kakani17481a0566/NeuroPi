namespace SchoolManagement.ViewModel.OrderItem
{
    public class OrderItemUpdateVM
    {

        //public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int OrderQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int UnitPrice { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }

    }
}
