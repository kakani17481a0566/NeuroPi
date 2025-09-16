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
                    CreatedBy = i.CreatedBy,
                    CreatedOn = i.CreatedOn,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedOn = i.UpdatedOn
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
                CreatedBy = items.CreatedBy,
                CreatedOn = items.CreatedOn,
                UpdatedBy = items.UpdatedBy,
                UpdatedOn = items.UpdatedOn
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
                CreatedBy = items.CreatedBy,
                CreatedOn = items.CreatedOn,
                UpdatedBy = items.UpdatedBy,
                UpdatedOn = items.UpdatedOn
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
                    CreatedBy = i.CreatedBy,
                    CreatedOn = i.CreatedOn,
                    UpdatedBy = i.UpdatedBy,
                    UpdatedOn = i.UpdatedOn
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

        public ItemsResponseVM CreateItemWithGroup(ItemInsertVM vm)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                // 1️⃣ Validate required fields
                if (string.IsNullOrWhiteSpace(vm.Name))
                    throw new ArgumentException("Item name is required.");

                if (vm.TenantId <= 0)
                    throw new ArgumentException("TenantId is required.");

                if (vm.CreatedBy <= 0)
                    throw new ArgumentException("CreatedBy (user id) is required.");

                // 2️⃣ Create parent item
                var parentItem = new MItems
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
                    CreatedBy = vm.CreatedBy,   // ✅ correct: userId, not tenantId
                    IsDeleted = false
                };

                _context.Items.Add(parentItem);
                _context.SaveChanges();

                // 3️⃣ Insert child items if this is a group
                if (parentItem.IsGroup && vm.GroupItems.Any())
                {
                    foreach (var child in vm.GroupItems)
                    {
                        // validate child item exists
                        var childExists = _context.Items.Any(i => i.Id == child.ItemId && !i.IsDeleted);
                        if (!childExists)
                            throw new ArgumentException($"Child item with Id {child.ItemId} does not exist.");

                        var groupRow = new MItemsGroup
                        {
                            SetItemId = parentItem.Id,
                            ItemId = child.ItemId,
                            Quantity = child.Quantity ?? 1,
                            FixedPrice = child.FixedPrice,
                            DiscountPrice = child.DiscountPrice,
                            TenantId = vm.TenantId,
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = vm.CreatedBy,   // ✅ same user
                            IsDeleted = false
                        };

                        _context.ItemsGroup.Add(groupRow);
                    }

                    _context.SaveChanges();
                }

                // 4️⃣ Commit
                transaction.Commit();

                // 5️⃣ Return response
                return new ItemsResponseVM
                {
                    Id = parentItem.Id,
                    Name = parentItem.Name,
                    CategoryId = parentItem.CategoryId,
                    TenantId = parentItem.TenantId,
                    Height = parentItem.Height,
                    Width = parentItem.Width,
                    Depth = parentItem.Depth,
                    Description = parentItem.Description,
                    ItemCode = parentItem.ItemCode,
                    IsGroup = parentItem.IsGroup,
                    CreatedBy = parentItem.CreatedBy,
                    CreatedOn = parentItem.CreatedOn,
                    UpdatedBy = parentItem.UpdatedBy,
                    UpdatedOn = parentItem.UpdatedOn
                };
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public ItemWithGroupResponseVM GetItemWithGroup(int id, int tenantId)
        {
            var parent = _context.Items
                .FirstOrDefault(i => i.Id == id && i.TenantId == tenantId && !i.IsDeleted);

            if (parent == null)
                return null;

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
