using SchoolManagement.Model;

namespace SchoolManagement.ViewModel.Orders
{
    public class OrderResponseVM
    {
        public int id { get; set; }
        public int supplier_id { get; set; }
        public DateTime order_date { get; set; }
        public DateTime? exp_date { get; set; }
        public string? delivery_address { get; set; }
        public DateTime? delivered_date { get; set; }
        public string? order_status { get; set; }
        public string? trx_id { get; set; }
        public int? order_type_id { get; set; }
        public int? tenant_id { get; set; }
        public DateTime created_on { get; set; }
        public int? created_by { get; set; }
        public DateTime updated_on { get; set; }
        public int? updated_by { get; set; }
        public bool is_deleted { get; set; }

        public static OrderResponseVM? FromModel(MOrders? m)
        {
            if (m == null) return null;
            return new OrderResponseVM
            {
                id = m.id,
                supplier_id = m.supplier_id,
                order_date = m.order_date,
                exp_date = m.exp_date,
                delivery_address = m.delivery_address,
                delivered_date = m.delivered_date,
                order_status = m.order_status,
                trx_id = m.trx_id,
                order_type_id = m.order_type_id,
                tenant_id = m.tenant_id,
                created_on = m.created_on,
                created_by = m.created_by,
                updated_on = m.updated_on,
                updated_by = m.updated_by,
                is_deleted = m.is_deleted
            };
        }

        public static List<OrderResponseVM> FromModels(IEnumerable<MOrders> models)
            => models.Select(FromModel)!.Where(x => x != null)!.ToList()!;
    }
}
