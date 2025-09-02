namespace SchoolManagement.ViewModel.Orders
{
    public class OrderRequestVM
    {
        public int supplier_id { get; set; }
        public DateTime? order_date { get; set; }
        public DateTime? exp_date { get; set; }
        public string? delivery_address { get; set; }
        public DateTime? delivered_date { get; set; }
        public string? order_status { get; set; }
        public string? trx_id { get; set; }
        public int? order_type_id { get; set; }
        public int? tenant_id { get; set; }
        public int updated_by { get; set; }            // required: who updates
    }
}
