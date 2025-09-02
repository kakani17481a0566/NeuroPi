namespace SchoolManagement.ViewModel.Orders
{
    public class OrdersVM
    {
        public int id { get; set; }
        public int supplier_id { get; set; }
        public DateTime order_date { get; set; }
        public DateTime exp_date { get; set; }
        public string delivery_address { get; set; }
        public DateTime delivery_date { get; set; }

        public string order_status { get; set; }
        public int trx_id { get; set; }
        public int order_type_id { get; set; }
        public DateTime created_on { get; set; }
        public DateTime updated_on { get; set; }
        public int tenant_id { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public bool is_delete { get; set; }

    }
}
