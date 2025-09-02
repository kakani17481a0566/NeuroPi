using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemCategory;

namespace SchoolManagement.Services.Implementation
{
    public class ItemCategoryServiceImpl : IItemCategoryService
    {
        private readonly SchoolManagementDb _context;
        public ItemCategoryServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        
        public ItemCategoryResponseVM CreateItemCategory(ItemCategoryRequestVM itemCategoryRequestVM)
        {
           var newItemCategory = itemCategoryRequestVM.ToModel();
              newItemCategory.CreatedOn = DateTime.UtcNow;
              _context.ItemCategory.Add(newItemCategory);
              _context.SaveChanges();
              return ItemCategoryResponseVM.ToViewModel(newItemCategory);

        }

        public ItemCategoryResponseVM DeleteItemCategory(int id, int tenantId)
        {
            var itemCategory = _context.ItemCategory.FirstOrDefault(ic => !ic.IsDeleted && ic.Id == id && ic.TenantId == tenantId);
            if (itemCategory == null) return null;
            itemCategory.IsDeleted = true;
            itemCategory.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return ItemCategoryResponseVM.ToViewModel(itemCategory);

        }

        public List<ItemCategoryResponseVM> GetAllItemsCategory()
        {
            return _context.ItemCategory
                .Where(ic => !ic.IsDeleted)
                .Select(ic => new ItemCategoryResponseVM
                {
                    Id = ic.Id,
                    Name = ic.Name,
                    TenantId = ic.TenantId,
                    CreatedBy = ic.CreatedBy,
                    CreatedOn = ic.CreatedOn,
                    UpdatedBy = ic.UpdatedBy,
                    UpdatedOn = ic.UpdatedOn
                }).ToList();
        }

        public ItemCategoryResponseVM GetItemCategoryById(int id)
        {
            var itemCategory = _context.ItemCategory.FirstOrDefault(ic => !ic.IsDeleted && ic.Id == id);
            if (itemCategory == null) return null;
            return new ItemCategoryResponseVM
            {
                Id = itemCategory.Id,
                Name = itemCategory.Name,
                TenantId = itemCategory.TenantId,
                CreatedBy = itemCategory.CreatedBy,
                CreatedOn = itemCategory.CreatedOn,
                UpdatedBy = itemCategory.UpdatedBy,
                UpdatedOn = itemCategory.UpdatedOn
            };
        }

        public List<ItemCategoryResponseVM> GetItemCategoryByTenantId(int tenantId)
        {
            return _context.ItemCategory
                .Where(ic => !ic.IsDeleted && ic.TenantId == tenantId)
                .Select(ic => new ItemCategoryResponseVM
                {
                    Id = ic.Id,
                    Name = ic.Name,
                    TenantId = ic.TenantId,
                    CreatedBy = ic.CreatedBy,
                    CreatedOn = ic.CreatedOn,
                    UpdatedBy = ic.UpdatedBy,
                    UpdatedOn = ic.UpdatedOn
                }).ToList();
        }

        public ItemCategoryResponseVM GetItemCategoryByTenantIdAndId(int tenantId, int id)
        {
            var itemCategory = _context.ItemCategory.FirstOrDefault(ic => !ic.IsDeleted && ic.Id == id && ic.TenantId == tenantId);
            if (itemCategory == null) return null;
            return new ItemCategoryResponseVM
            {
                Id = itemCategory.Id,
                Name = itemCategory.Name,
                TenantId = itemCategory.TenantId,
                CreatedBy = itemCategory.CreatedBy,
                CreatedOn = itemCategory.CreatedOn,
                UpdatedBy = itemCategory.UpdatedBy,
                UpdatedOn = itemCategory.UpdatedOn
            };
        }

        public ItemCategoryResponseVM UpdateItemCategory(ItemCategoryUpdateVM itemCategoryUpdateVM)
        {
            var existingItemCategory = _context.ItemCategory.FirstOrDefault(ic => !ic.IsDeleted && ic.Id == itemCategoryUpdateVM.Id && ic.TenantId == itemCategoryUpdateVM.TenantId);
            if (existingItemCategory == null) return null;
            existingItemCategory.Name = itemCategoryUpdateVM.Name;
            existingItemCategory.UpdatedBy = itemCategoryUpdateVM.UpdatedBy;
            existingItemCategory.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return ItemCategoryResponseVM.ToViewModel(existingItemCategory);
        }

        public ItemCategoryResponseVM UpdateItemCategory(int id, int tenantId, ItemCategoryUpdateVM itemCategoryUpdateVM)
        {
            throw new NotImplementedException();
        }
    }
}
