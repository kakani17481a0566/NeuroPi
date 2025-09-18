using SchoolManagement.Data;
using SchoolManagement.Model;
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

        // 🔹 Create normal item
        public ItemsResponseVM CreateItems(ItemsRequestVM itemsRequestVM)
        {
            var newItem = itemsRequestVM.ToModel();
            newItem.CreatedOn = DateTime.UtcNow;
            newItem.IsDeleted = false;

            _context.Items.Add(newItem);
            _context.SaveChanges();

            return ItemsResponseVM.ToViewModel(newItem);
        }

        // 🔹 Soft delete
        public bool DeleteItemsByIdAndTenant(int id, int tenantId)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (item == null) return false;

            item.IsDeleted = true;
            item.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }

        // 🔹 Get all
        public List<ItemsResponseVM> GetAllItems()
        {
            return _context.Items
                .Where(i => !i.IsDeleted)
                .Select(i => ItemsResponseVM.ToViewModel(i))
                .ToList();
        }

        // 🔹 Get by Id
        public ItemsResponseVM GetItemsById(int id)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id);
            return item == null ? null : ItemsResponseVM.ToViewModel(item);
        }

        // 🔹 Get by Id + Tenant
        public ItemsResponseVM GetItemsByIdAndTenant(int id, int tenantId)
        {
            var item = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            return item == null ? null : ItemsResponseVM.ToViewModel(item);
        }

        // 🔹 Get by Tenant
        public List<ItemsResponseVM> GetItemsByTenant(int tenantId)
        {
            return _context.Items
                .Where(i => !i.IsDeleted && i.TenantId == tenantId)
                .Select(i => ItemsResponseVM.ToViewModel(i))
                .ToList();
        }

        // 🔹 Update
        public ItemsResponseVM UpdateItems(int id, int tenantId, ItemsUpdateVM vm)
        {
            var existing = _context.Items.FirstOrDefault(i => !i.IsDeleted && i.Id == id && i.TenantId == tenantId);
            if (existing == null) return null;

            existing.Name = vm.Name;
            existing.CategoryId = vm.CategoryId;
            existing.Height = vm.Height ?? existing.Height;
            existing.Width = vm.Width ?? existing.Width;
            existing.Depth = vm.Depth ?? existing.Depth;
            existing.Description = vm.Description;
            existing.ItemCode = vm.ItemCode;
            existing.IsGroup = vm.IsGroup;
            existing.UpdatedBy = vm.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return ItemsResponseVM.ToViewModel(existing);
        }

        // 🔹 Create item + group children
        public ItemsResponseVM CreateItemWithGroup(ItemInsertVM vm)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                if (string.IsNullOrWhiteSpace(vm.Name))
                    throw new ArgumentException("Item name is required.");
                if (vm.TenantId <= 0)
                    throw new ArgumentException("TenantId is required.");
                if (vm.CreatedBy <= 0)
                    throw new ArgumentException("CreatedBy (user id) is required.");

                var parent = new MItems
                {
                    Name = vm.Name,
                    CategoryId = vm.CategoryId,
                    TenantId = vm.TenantId,
                    Height = vm.Height ?? 0,
                    Width = vm.Width ?? 0,
                    Depth = vm.Depth ?? 0,
                    Description = vm.Description,
                    ItemCode = vm.ItemCode,
                    IsGroup = vm.IsGroup || vm.GroupItems.Any(),
                    CreatedOn = DateTime.UtcNow,
                    CreatedBy = vm.CreatedBy,
                    IsDeleted = false
                };

                _context.Items.Add(parent);
                _context.SaveChanges();

                if (parent.IsGroup && vm.GroupItems.Any())
                {
                    foreach (var child in vm.GroupItems)
                    {
                        var childExists = _context.Items.Any(i => i.Id == child.ItemId && !i.IsDeleted);
                        if (!childExists)
                            throw new ArgumentException($"Child item with Id {child.ItemId} does not exist.");

                        var groupRow = new MItemsGroup
                        {
                            SetItemId = parent.Id,
                            ItemId = child.ItemId,
                            Quantity = child.Quantity ?? 1,
                            FixedPrice = child.FixedPrice,
                            DiscountPrice = child.DiscountPrice,
                            TenantId = vm.TenantId,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = vm.CreatedBy,
                            IsDeleted = false
                        };
                        _context.ItemsGroup.Add(groupRow);
                    }
                    _context.SaveChanges();
                }

                transaction.Commit();
                return ItemsResponseVM.ToViewModel(parent);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        // 🔹 Get item with group
        public ItemWithGroupResponseVM GetItemWithGroup(int id, int tenantId)
        {
            var parent = _context.Items.FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);
            if (parent == null) return null;

            var response = new ItemWithGroupResponseVM
            {
                Id = parent.Id,
                Name = parent.Name,
                Description = parent.Description,
                ItemCode = parent.ItemCode,
                CategoryId = parent.CategoryId,
                TenantId = parent.TenantId,
                IsGroup = parent.IsGroup
            };

            if (parent.IsGroup)
            {
                response.Children = (from ig in _context.ItemsGroup
                                     join c in _context.Items on ig.ItemId equals c.Id
                                     where ig.SetItemId == parent.Id
                                           && ig.TenantId == tenantId
                                           && !ig.IsDeleted
                                           && !c.IsDeleted
                                     select new GroupChildVM
                                     {
                                         ItemId = c.Id,
                                         Name = c.Name,
                                         Quantity = ig.Quantity,
                                         FixedPrice = ig.FixedPrice,
                                         DiscountPrice = ig.DiscountPrice
                                     }).ToList();
            }

            return response;
        }
    }
}
