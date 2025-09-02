using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Items;

namespace SchoolManagement.Services.Implementation
{
    public class ItemsServiceImpl : IItemsService
    {
        private readonly SchoolManagementDb _context;
        public ItemsServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public ItemsResponseVM CreateItems(ItemsRequestVM itemsRequestVM)
        {
            var newItems = itemsRequestVM.ToModel();
            newItems.CreatedOn = DateTime.UtcNow;
            _context.Items.Add(newItems);
            _context.SaveChanges();
            return ItemsResponseVM.ToViewModel(newItems);
        }

        public bool DeleteItemsByIdAndTenant(int id, int tenantId)
        {
            var items = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (items == null) return false;
            items.IsDeleted = true;
            items.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        public List<ItemsResponseVM> GetAllItems()
        {
            return _context.Items
                .Where(i => !i.IsDeleted)
                .Select(i => new ItemsResponseVM
                {
                    Id = i.Id,
                    CategoryId = i.CategoryId,
                    Height = i.Height,
                    Width = i.Width,
                    Depth = i.Depth,
                    Name = i.Name,
                    TenantId = i.TenantId,
                    createdBy = i.CreatedBy,
                    createdOn = i.CreatedOn,
                    updatedBy = i.UpdatedBy,
                    updatedOn = i.UpdatedOn
                }).ToList();
        }

        public ItemsResponseVM GetItemsById(int id)
        {
            var items = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id);
            if (items == null) return null;
            return new ItemsResponseVM
            {
                Id = items.Id,
                CategoryId = items.CategoryId,
                Height = items.Height,
                Width = items.Width,
                Depth = items.Depth,
                Name = items.Name,
                TenantId = items.TenantId,
                createdBy = items.CreatedBy,
                createdOn = items.CreatedOn,
                updatedBy = items.UpdatedBy,
                updatedOn = items.UpdatedOn
            };
        }

        public ItemsResponseVM GetItemsByIdAndTenant(int id, int tenantId)
        {
            var items = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (items == null) return null;
            return new ItemsResponseVM
            {
                Id = items.Id,
                CategoryId = items.CategoryId,
                Height = items.Height,
                Width = items.Width,
                Depth = items.Depth,
                Name = items.Name,
                TenantId = items.TenantId,
                createdBy = items.CreatedBy,
                createdOn = items.CreatedOn,
                updatedBy = items.UpdatedBy,
                updatedOn = items.UpdatedOn
            };
        }

        public List<ItemsResponseVM> GetItemsByTenant(int tenantId)
        {
            return _context.Items
                .Where(i => !i.IsDeleted && i.TenantId == tenantId)
                .Select(i => new ItemsResponseVM
                {
                    Id = i.Id,
                    CategoryId = i.CategoryId,
                    Height = i.Height,
                    Width = i.Width,
                    Depth = i.Depth,
                    Name = i.Name,
                    TenantId = i.TenantId,
                    createdBy = i.CreatedBy,
                    createdOn = i.CreatedOn,
                    updatedBy = i.UpdatedBy,
                    updatedOn = i.UpdatedOn
                }).ToList();
        }

        public ItemsResponseVM UpdateItems(int id, int tenantId, ItemsUpdateVM itemsUpdateVM)
        {
            var existingitems = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (existingitems == null) return null;
            // Update fields
           existingitems.CategoryId = itemsUpdateVM.CategoryId;
           existingitems.Height = itemsUpdateVM.Height;
           existingitems.Width = itemsUpdateVM.Width;
           existingitems.Depth = itemsUpdateVM.Depth;
           existingitems.Name = itemsUpdateVM.Name;
           existingitems.UpdatedBy = itemsUpdateVM.UpdatedBy;
           existingitems.UpdatedOn = DateTime.UtcNow;
           _context.SaveChanges();
           return ItemsResponseVM.ToViewModel(existingitems);

        }
    }
}
