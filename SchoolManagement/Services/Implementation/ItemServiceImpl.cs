using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Item;
using System.Collections.Generic;
using System.Linq;

// Deloped by: Lekhan
namespace SchoolManagement.Services.Implementation
{
    public class ItemServiceImpl : IItemService
    {
        private readonly SchoolManagementDb _context;

        public ItemServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }

        // Get all items that are not deleted
        // HttpGet: api/item
        public List<ItemVM> GetAll()
        {
            return _context.item
                .Where(i => !i.IsDeleted)
                .Select(i => new ItemVM
                {
                    Id = i.Id,
                    ItemHeaderId = i.ItemHeaderId,
                    BookCondition = i.BookCondition,
                    Status = i.Status,
                    PurchasedOn = i.PurchasedOn,
                    TenantId = i.TenantId
                }).ToList();
        }

        // Get all items by tenant ID that are not deleted
        // HttpGet: api/item/tenant/{tenantId}
        public List<ItemVM> GetAllByTenantId(int tenantId)
        {
            return _context.item
                .Where(i => !i.IsDeleted && i.TenantId == tenantId)
                .Select(i => new ItemVM
                {
                    Id = i.Id,
                    ItemHeaderId = i.ItemHeaderId,
                    BookCondition = i.BookCondition,
                    Status = i.Status,
                    PurchasedOn = i.PurchasedOn,
                    TenantId = i.TenantId
                }).ToList();
        }

        // Get all items by tenant ID and item header ID that are not deleted
        // HttpGet: api/item/tenant/{tenantId}/header/{itemHeaderId}
        public ItemVM GetById(int id)
        {
            var item = _context.item.FirstOrDefault(i => !i.IsDeleted && i.Id == id);
            if (item == null) return null;

            return new ItemVM
            {
                Id = item.Id,
                ItemHeaderId = item.ItemHeaderId,
                BookCondition = item.BookCondition,
                Status = item.Status,
                PurchasedOn = item.PurchasedOn,
                TenantId = item.TenantId
            };
        }
        // Get item by ID and tenant ID that are not deleted
        // HttpGet: api/item/{id}/tenant/{tenantId}
        public ItemVM GetByIdAndTenantId(int id, int tenantId)
        {
            var item = _context.item.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (item == null) return null;

            return new ItemVM
            {
                Id = item.Id,
                ItemHeaderId = item.ItemHeaderId,
                BookCondition = item.BookCondition,
                Status = item.Status,
                PurchasedOn = item.PurchasedOn,
                TenantId = item.TenantId
            };
        }

        // Update item by ID and tenant ID
        // HttpPut: api/item/{id}/tenant/{tenantId}
        public ItemVM UpdateByIdAndTenantId(int id, int tenantId, UpdateItemVM request)
        {
            var item = _context.item.FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (item == null) return null;

            item.BookCondition = request.BookCondition;
            item.Status = request.Status;
            item.ItemHeaderId = request.ItemHeaderId;
            _context.SaveChanges();

            return GetById(id);
        }

        // Delete item by ID and tenant ID (soft delete)
        // HttpDelete: api/item/{id}/tenant/{tenantId}
        public ItemVM DeleteByIdAndTenantId(int id, int tenantId)
        {
            var item = _context.item.FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (item == null) return null;

            item.IsDeleted = true;
            _context.SaveChanges();

            return new ItemVM()
            {
                Id= item.Id,
                BookCondition = item.BookCondition,
                Status = item.Status,
            };
        }

        // Create a new item
        // HttpPost: api/item/create
        public ItemVM CreateItem(ItemRequestVM request)
        {
            var item = new MItem
            {
                ItemHeaderId = request.ItemHeaderId,
                BookCondition = request.BookCondition,
                Status = request.Status,
                PurchasedOn = request.PurchasedOn,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.item.Add(item);
            _context.SaveChanges();

            return GetById(item.Id);
        }
    }
}
