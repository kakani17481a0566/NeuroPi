using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ItemBranch;

namespace SchoolManagement.Services.Implementation
{
    public class ItemBranchServiceImpl : IItemBranchService
    {
        private readonly SchoolManagementDb _context;
        public ItemBranchServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        public ItemBranchResponseVM CreateItemBranch(ItemBranchRequestVM itemBranchRequestVM)
        {
            var newItemBranch = itemBranchRequestVM.ToModel();
            newItemBranch.CreatedOn = DateTime.UtcNow;
            _context.ItemBranch.Add(newItemBranch);
            _context.SaveChanges();
            return ItemBranchResponseVM.ToViewModel(newItemBranch);

        }

        public bool DeleteItemBranchByIdAndTenant(int id, int tenantId)
        {
            var itemBranch = _context.ItemBranch.FirstOrDefault(ib => !ib.IsDeleted && ib.Id == id && ib.TenantId == tenantId);
            if (itemBranch == null) return false;
            itemBranch.IsDeleted = true;
            itemBranch.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        public List<ItemBranchResponseVM> GetAllItemBranches()
        {
            return _context.ItemBranch
                .Where(ib => !ib.IsDeleted)
                .Select(ib => new ItemBranchResponseVM
                {
                    Id = ib.Id,
                    ItemId = ib.ItemId,
                    BranchId = ib.BranchId,
                    ItemQuantity = ib.ItemQuantity,
                    ItemPrice = ib.ItemPrice,
                    ItemCost = ib.ItemCost,
                    ItemReOrderLevel = ib.ItemReOrderLevel,
                    ItemLocationId = ib.ItemLocationId,
                    TenantId = ib.TenantId,
                    CreatedBy = ib.CreatedBy,
                    CreatedOn = ib.CreatedOn,
                    UpdatedBy = ib.UpdatedBy,
                    UpdatedOn = ib.UpdatedOn
                }).ToList();

        }

        public ItemBranchResponseVM GetItemBranchById(int id)
        {
            var itemBranch = _context.ItemBranch.FirstOrDefault(ib => !ib.IsDeleted && ib.Id == id);
            if (itemBranch == null) return null;
            return new ItemBranchResponseVM
            {
                Id = itemBranch.Id,
                ItemId = itemBranch.ItemId,
                BranchId = itemBranch.BranchId,
                ItemQuantity = itemBranch.ItemQuantity,
                ItemPrice = itemBranch.ItemPrice,
                ItemCost = itemBranch.ItemCost,
                ItemReOrderLevel = itemBranch.ItemReOrderLevel,
                ItemLocationId = itemBranch.ItemLocationId,
                TenantId = itemBranch.TenantId,
                CreatedBy = itemBranch.CreatedBy,
                CreatedOn = itemBranch.CreatedOn,
                UpdatedBy = itemBranch.UpdatedBy,
                UpdatedOn = itemBranch.UpdatedOn
            };
        }

        public ItemBranchResponseVM GetItemBranchByIdAndTenant(int id, int tenantId)
        {
            var itemBranch = _context.ItemBranch.FirstOrDefault(ib => !ib.IsDeleted && ib.Id == id && ib.TenantId == tenantId);
            if (itemBranch == null) return null;
            return new ItemBranchResponseVM {
                Id = itemBranch.Id,
                ItemId = itemBranch.ItemId,
                BranchId = itemBranch.BranchId,
                ItemQuantity = itemBranch.ItemQuantity,
                ItemPrice = itemBranch.ItemPrice,
                ItemCost = itemBranch.ItemCost,
                ItemReOrderLevel = itemBranch.ItemReOrderLevel,
                ItemLocationId = itemBranch.ItemLocationId,
                TenantId = itemBranch.TenantId,
                CreatedBy = itemBranch.CreatedBy,
                CreatedOn = itemBranch.CreatedOn,
                UpdatedBy = itemBranch.UpdatedBy,
                UpdatedOn = itemBranch.UpdatedOn
                };
        }

        public List<ItemBranchResponseVM> GetItemBranchesByTenant(int tenantId)
        {
           return _context.ItemBranch
                .Where(ib => !ib.IsDeleted && ib.TenantId == tenantId)
                .Select(ib => new ItemBranchResponseVM
                {
                    Id = ib.Id,
                    ItemId = ib.ItemId,
                    BranchId = ib.BranchId,
                    ItemQuantity = ib.ItemQuantity,
                    ItemPrice = ib.ItemPrice,
                    ItemCost = ib.ItemCost,
                    ItemReOrderLevel = ib.ItemReOrderLevel,
                    ItemLocationId = ib.ItemLocationId,
                    TenantId = ib.TenantId,
                    CreatedBy = ib.CreatedBy,
                    CreatedOn = ib.CreatedOn,
                    UpdatedBy = ib.UpdatedBy,
                    UpdatedOn = ib.UpdatedOn
                }).ToList();
        }

        public ItemBranchResponseVM UpdateItemBranch(int id, int tenantId, ItemBranchUpdateVM itemBranchUpdateVM)
        {
            var existingItemBranch = _context.ItemBranch.FirstOrDefault(ib => !ib.IsDeleted && ib.Id == id && ib.TenantId == tenantId);
            if (existingItemBranch == null) return null;
            existingItemBranch.ItemId = itemBranchUpdateVM.ItemId;
            existingItemBranch.BranchId = itemBranchUpdateVM.BranchId;
            existingItemBranch.ItemQuantity = itemBranchUpdateVM.ItemQuantity;
            existingItemBranch.ItemPrice = itemBranchUpdateVM.ItemPrice;
            existingItemBranch.ItemCost = itemBranchUpdateVM.ItemCost;
            existingItemBranch.ItemReOrderLevel = itemBranchUpdateVM.ItemReOrderLevel;
            existingItemBranch.ItemLocationId = itemBranchUpdateVM.ItemLocationId;
            existingItemBranch.UpdatedBy = itemBranchUpdateVM.UpdatedBy;
            existingItemBranch.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return ItemBranchResponseVM.ToViewModel(existingItemBranch);

        }
    }
}
