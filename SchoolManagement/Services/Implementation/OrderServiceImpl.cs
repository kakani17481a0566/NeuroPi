using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Orders;

namespace SchoolManagement.Services.Implementation
{
    public class OrderServiceImpl : IOrders
    {
        private readonly SchoolManagementDb _db;

        public OrderServiceImpl(SchoolManagementDb db) => _db = db;

        public List<OrderResponseVM> GetAllOrders()
        {
            var list = _db.Orders.AsNoTracking()
                .Where(o => !o.is_deleted)
                .OrderByDescending(o => o.created_on)
                .ToList();
            return OrderResponseVM.FromModels(list);
        }

        public List<OrderResponseVM> GetBySupplier(int supplierId)
        {
            var list = _db.Orders.AsNoTracking()
                .Where(o => !o.is_deleted && o.supplier_id == supplierId)
                .OrderByDescending(o => o.created_on)
                .ToList();
            return OrderResponseVM.FromModels(list);
        }

        public OrderResponseVM? GetOrderById(int id)
        {
            var m = _db.Orders.AsNoTracking()
                .FirstOrDefault(o => o.id == id && !o.is_deleted);
            return OrderResponseVM.FromModel(m);
        }

        public OrderResponseVM CreateOrder(OrderCreateVM vm)
        {
            var now = DateTime.UtcNow;

            var m = new MOrders
            {
                supplier_id = vm.supplier_id,
                order_date = vm.order_date?.Date ?? DateTime.UtcNow.Date, // respect DB default if you prefer
                exp_date = vm.exp_date?.Date,
                delivery_address = vm.delivery_address,
                delivered_date = vm.delivered_date?.Date,
                order_status = vm.order_status,
                trx_id = vm.trx_id,
                order_type_id = vm.order_type_id,
                tenant_id = vm.tenant_id,

                created_on = now,
                created_by = vm.created_by,
                updated_on = now,
                updated_by = vm.created_by,
                is_deleted = false
            };

            _db.Orders.Add(m);
            _db.SaveChanges();

            return OrderResponseVM.FromModel(m)!;
        }

        public OrderResponseVM? UpdateOrder(int id, OrderRequestVM vm)
        {
            var m = _db.Orders.FirstOrDefault(o => o.id == id && !o.is_deleted);
            if (m == null) return null;

            // update allowed fields only
            m.supplier_id = vm.supplier_id;
            if (vm.order_date.HasValue) m.order_date = vm.order_date.Value.Date;
            if (vm.exp_date.HasValue) m.exp_date = vm.exp_date.Value.Date;
            m.delivery_address = vm.delivery_address;
            if (vm.delivered_date.HasValue) m.delivered_date = vm.delivered_date.Value.Date;
            m.order_status = vm.order_status;
            m.trx_id = vm.trx_id;
            m.order_type_id = vm.order_type_id;
            m.tenant_id = vm.tenant_id;

            m.updated_on = DateTime.UtcNow;
            m.updated_by = vm.updated_by;

            _db.SaveChanges();
            return OrderResponseVM.FromModel(m);
        }

        public bool DeleteOrder(int id, int deleterUserId)
        {
            var m = _db.Orders.FirstOrDefault(o => o.id == id && !o.is_deleted);
            if (m == null) return false;

            m.is_deleted = true;
            m.updated_on = DateTime.UtcNow;
            m.updated_by = deleterUserId;

            _db.SaveChanges();
            return true;
        }
    }
}
