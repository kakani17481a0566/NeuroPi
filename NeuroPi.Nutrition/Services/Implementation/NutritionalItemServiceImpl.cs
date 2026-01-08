using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;
using NeuroPi.Nutrition.ViewModel.MealPlanMonitoring;
using NeuroPi.Nutrition.ViewModel.NutritionalItem;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class NutritionalItemServiceImpl : INutritionalItem
    {
        private readonly NeutritionDbContext _context;
        public NutritionalItemServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }
        public NutritionalItemResponseVM CreateNutritionalItem(NutritionalItemRequestVM vm)
        {
            // 1 Validation
            var duplicate = _context.NutritionalItems
                .AsNoTracking()
                .FirstOrDefault(x => x.TenantId == vm.TenantId &&
                                     x.Name.ToLower() == vm.Name.ToLower() &&
                                     !x.IsDeleted);

            if (duplicate != null)
                throw new Exception("A nutritional item with this name already exists.");

            // 2 Create main item
            var item = NutritionalItemRequestVM.ToModel(vm);
            _context.NutritionalItems.Add(item);
            _context.SaveChanges(); // ID generated

            int itemId = item.Id;
            var now = DateTime.UtcNow;

            //  Meal type mapping
            if (vm.MealTypeIds?.Any() == true)
            {
                var mealLinks = vm.MealTypeIds.Select(id => new MNutritionalItemMealType
                {
                    NutritionalItemId = itemId,
                    MealTypeId = id,
                    TenantId = vm.TenantId,
                    CreatedBy = vm.CreatedBy,
                    CreatedOn = now
                });

                _context.NutritionalItemMealType.AddRange(mealLinks);
            }

            //  Vitamin mapping
            if (vm.VitaminIds?.Any() == true)
            {
                var vitaminLinks = vm.VitaminIds.Select(id => new MNutritionalItemVitamins
                {
                    NutritionalItemId = itemId,
                    VitaminsId = id,
                    TenantId = vm.TenantId,
                    CreatedBy = vm.CreatedBy,
                    CreatedOn = now
                });

                _context.NutritionalItemVitamins.AddRange(vitaminLinks);
            }

            //  Focus Tag mapping
            if (vm.FocusIds?.Any() == true)
            {
                var focusLinks = vm.FocusIds.Select(id => new MNutritionalFocusItem
                {
                    NutritionalItemId = itemId,
                    NutritionalFocusId = id,
                    TenantId = vm.TenantId,
                    CreatedBy = vm.CreatedBy,
                    CreatedOn = now
                });

                _context.NutritionalFocusItem.AddRange(focusLinks);
            }

            _context.SaveChanges();

            //  Build Response with Names
            var response = NutritionalItemResponseVM.ToViewModel(item);

            response.MealTypes = _context.MealTypes
                .Where(x => vm.MealTypeIds.Contains(x.Id) && x.TenantId == vm.TenantId && !x.IsDeleted)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            response.Vitamins = _context.Vitamins
                .Where(x => vm.VitaminIds.Contains(x.Id) && x.TenantId == vm.TenantId && !x.IsDeleted)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            response.FocusTags = _context.NutritionalFocuses
                .Where(x => vm.FocusIds.Contains(x.Id) && x.TenantId == vm.TenantId && !x.IsDeleted)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            return response;
        }




        public bool DeleteNutritionalItem(int id, int tenantId)
        {
            var existingNutritionalItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id == id && ni.TenantId == tenantId && !ni.IsDeleted);
            if (existingNutritionalItem == null)
            {
                return false;
            }
            existingNutritionalItem.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

       

        public NutritionalItemListResponseVM GetAllItems(int userId, int tenantId, DateTime? selectedDate)
        {
            // If no date passed → use today
            var date = selectedDate?.Date ?? DateTime.Now.Date;

            var result = new NutritionalItemListResponseVM();

            // ============================================================
            // 1. Get all nutritional items for this tenant
            // ============================================================
            var nutritionalItems = _context.NutritionalItems
                .Where(n => !n.IsDeleted && n.TenantId == tenantId)
                .ToList();

            // Preload efficiently
            var mealTypeMap = _context.NutritionalItemMealType
                .Where(n => !n.IsDeleted && n.TenantId == tenantId)
                .GroupBy(n => n.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.MealTypeId).ToList());

            var vitaminMap = _context.NutritionalItemVitamins
                .Where(n => !n.IsDeleted && n.TenantId == tenantId)
                .GroupBy(n => n.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.VitaminsId).ToList());

            var focusMap = _context.NutritionalFocusItem
                .Where(n => !n.IsDeleted && n.TenantId == tenantId)
                .GroupBy(n => n.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.NutritionalFocusId).ToList());

            var favourites = _context.UserFavourites
                .Where(f => f.UserId == userId && !f.IsDeleted && f.TenantId == tenantId)
                .Select(f => f.NutritionalItemId)
                .ToHashSet();

            // Build response
            result.AllItems = nutritionalItems.Select(item => new NutritionalItemDetailsVM
            {
                Id = item.Id,
                Name = item.Name,
                CaloriesQuantity = item.CaloriesQuantity,
                ProteinQuantity = item.ProteinQuantity,
                MealTypeIds = mealTypeMap.GetValueOrDefault(item.Id, new List<int>()),
                VitaminIds = vitaminMap.GetValueOrDefault(item.Id, new List<int>()),
                FocusIds = focusMap.GetValueOrDefault(item.Id, new List<int>()),
                DietTypeId = item.DietTypeId,
                ItemImage = item.ItemImage,
                UserFavourite = favourites.Contains(item.Id)

            }).ToList();

            // ============================================================
            // 2. Meal Plan for specific date + tenant
            // ============================================================
            result.MealPlansDate = DateOnly.FromDateTime(date);

            var allMealTypes = _context.MealTypes
                .Where(mt => !mt.IsDeleted && mt.TenantId == tenantId)
                .Select(mt => new { mt.Id, mt.Name })
                .ToList();

            var mealPlans = _context.MealPlan
                .Where(m => m.Date == DateOnly.FromDateTime(date) &&
                            m.UserId == userId &&
                            m.TenantId == tenantId &&
                            !m.IsDeleted)
                .Include(m => m.NutritionalItem)
                .ToList();

            result.MealPlans = allMealTypes.Select(mt =>
            {
                var plansForType = mealPlans.Where(m => m.MealTypeId == mt.Id).ToList();

                return new MealPlan
                {
                    MealTypeId = mt.Id,
                    Items = plansForType.Select(n => new Items
                    {
                        Id = n.NutritionalItem.Id,
                        Quantity = n.Quantity,
                        CaloriesQuantity = n.NutritionalItem.CaloriesQuantity,
                        ProteinQuantity = n.NutritionalItem.ProteinQuantity,
                        Type = n.NutritionalItem.NutritionalItemTypeId,
                        FocusIds = focusMap.GetValueOrDefault(n.NutritionalItem.Id, new List<int>()),
                        VitaminIds = vitaminMap.GetValueOrDefault(n.NutritionalItem.Id, new List<int>()),
                        DietTypeId = n.NutritionalItem.DietTypeId ?? 0,
                        ItemImage = n.NutritionalItem.ItemImage,
                        UserFavourite = favourites.Contains(n.NutritionalItem.Id)
                    }).ToList()
                };
            }).ToList();

            // ============================================================
            // 3. Filters – tenant-based
            // ============================================================
            result.MealTypes = allMealTypes
                .Select(mt => new Filters { Id = mt.Id, Name = mt.Name })
                .ToList();

            result.ItemTypes = _context.NutritionalIteamType
                .Where(v => !v.IsDeleted && v.TenantId == tenantId)
                .Select(v => new Filters { Id = v.Id, Name = v.Name })
                .ToList();

            result.Vitamins = _context.Vitamins
                .Where(v => !v.IsDeleted && v.TenantId == tenantId)
                .Select(v => new Filters { Id = v.Id, Name = v.Name })
                .ToList();

            result.FocusTags = _context.NutritionalFocuses
                .Where(f => !f.IsDeleted && f.TenantId == tenantId)
                .Select(f => new Filters { Id = f.Id, Name = f.Name })
                .ToList();

            return result;
        }






        public NutritionalItemResponseVM GetNutrionalItemById(int id)
        {
            var nutritionalItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id == id && !ni.IsDeleted);
            if (nutritionalItem == null)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModel(nutritionalItem);
        }

        public List<NutritionalItemResponseVM> GetNutrionalItemByTenantId(int tenantId)
        {
            // ---------------------------
            // Load all nutritional items for this tenant
            // ---------------------------
            var items = _context.NutritionalItems
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .AsNoTracking()
                .ToList();

            if (!items.Any())
                return new List<NutritionalItemResponseVM>();


            // ---------------------------
            //  Load mapping tables for all items (3 queries only)
            // ---------------------------
            var itemIds = items.Select(x => x.Id).ToList();

            var mealMap = _context.NutritionalItemMealType
                .Where(x => itemIds.Contains(x.NutritionalItemId) && !x.IsDeleted)
                .AsNoTracking()
                .GroupBy(x => x.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(e => e.MealTypeId).ToList());

            var vitaminMap = _context.NutritionalItemVitamins
                .Where(x => itemIds.Contains(x.NutritionalItemId) && !x.IsDeleted)
                .AsNoTracking()
                .GroupBy(x => x.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(e => e.VitaminsId).ToList());

            var focusMap = _context.NutritionalFocusItem
                .Where(x => itemIds.Contains(x.NutritionalItemId) && !x.IsDeleted)
                .AsNoTracking()
                .GroupBy(x => x.NutritionalItemId)
                .ToDictionary(g => g.Key, g => g.Select(e => e.NutritionalFocusId).ToList());


            // ---------------------------
            //  Load Master tables once (super fast)
            // ---------------------------
            var mealTypes = _context.MealTypes
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .AsNoTracking()
                .ToDictionary(x => x.Id, x => x.Name);

            var vitamins = _context.Vitamins
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .AsNoTracking()
                .ToDictionary(x => x.Id, x => x.Name);

            var focusTags = _context.NutritionalFocuses
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .AsNoTracking()
                .ToDictionary(x => x.Id, x => x.Name);


            // ---------------------------
            //  Build the final response list
            // ---------------------------
            var response = new List<NutritionalItemResponseVM>();

            foreach (var item in items)
            {
                var vm = NutritionalItemResponseVM.ToViewModel(item);

                // Meal Types
                if (mealMap.ContainsKey(item.Id))
                {
                    vm.MealTypes = mealMap[item.Id]
                        .Where(id => mealTypes.ContainsKey(id))
                        .Select(id => new Filters { Id = id, Name = mealTypes[id] })
                        .ToList();
                }

                // Vitamins
                if (vitaminMap.ContainsKey(item.Id))
                {
                    vm.Vitamins = vitaminMap[item.Id]
                        .Where(id => vitamins.ContainsKey(id))
                        .Select(id => new Filters { Id = id, Name = vitamins[id] })
                        .ToList();
                }

                // Focus Tags
                if (focusMap.ContainsKey(item.Id))
                {
                    vm.FocusTags = focusMap[item.Id]
                        .Where(id => focusTags.ContainsKey(id))
                        .Select(id => new Filters { Id = id, Name = focusTags[id] })
                        .ToList();
                }

                response.Add(vm);
            }

            return response;
        }


        public NutritionalItemResponseVM GetNutritionalItemByIdAndTenantId(int id, int tenantId)
        {
            var nutritionItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id == id && ni.TenantId == tenantId && !ni.IsDeleted);
            if (nutritionItem == null)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModel(nutritionItem);


        }

        public List<NutritionalItemResponseVM> GetNutritionalItemResponses()
        {
            var nutritionalItemList = _context.NutritionalItems.Where(ni => !ni.IsDeleted).ToList();
            if (nutritionalItemList == null)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModelList(nutritionalItemList);
        }

        public NutritionalItemResponseVM UpdateNutritionalItem(int id, int tenantId, NutritionalItemUpdateVM item)
        {
            var existingNutritionalItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id == id && ni.TenantId == tenantId && !ni.IsDeleted);
            if (existingNutritionalItem == null)
            {
                return null;
            }
            existingNutritionalItem.Name = item.Name;
            existingNutritionalItem.Code = item.Code;
            existingNutritionalItem.Description = item.Description;
            existingNutritionalItem.CaloriesQuantity = item.CaloriesQuantity;
            existingNutritionalItem.ProteinQuantity = item.ProteinQuantity;
            existingNutritionalItem.Quantity = item.Quantity;
            existingNutritionalItem.Receipe = item.Receipe;
            existingNutritionalItem.NutritionalItemTypeId = item.NutritionalItemTypeId;
            existingNutritionalItem.Eatble = item.Edible;
            existingNutritionalItem.ItemImage = item.ItemImage;
            existingNutritionalItem.UpdatedBy = item.UpdatedBy;
            existingNutritionalItem.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();
            return NutritionalItemResponseVM.ToViewModel(existingNutritionalItem);

        }


        public SaveMealPlanResponseVM SaveMealPlan(SaveMealPlanVM request)
        {
            int userId = request.UserId;
            int tenantId = request.TenantId;
            var date = request.Date;

            SaveMealPlanResponseVM response = new SaveMealPlanResponseVM
            {
                StatusCode = 200,
                Message = "Meal plan saved successfully",
                Date = date
            };

            // 1️⃣ Load existing
            var existingPlans = _context.MealPlan
                .Where(m => m.UserId == userId &&
                            m.TenantId == tenantId &&
                            m.Date == date &&
                            !m.IsDeleted)
                .ToList();

            // 2️⃣ Convert incoming
            var incomingItems = request.ToModelList();

            // 3️⃣ INSERT or UPDATE
            foreach (var item in incomingItems)
            {
                var existing = existingPlans.FirstOrDefault(x =>
                    x.MealTypeId == item.MealTypeId &&
                    x.NutritionalItemId == item.NutritionalItemId
                );

                if (existing != null)
                {
                    // UPDATE
                    existing.Quantity = item.Quantity;
                    existing.UpdatedBy = userId;
                    existing.UpdatedOn = DateTime.UtcNow;

                    response.SavedItems.Add(new SavedMealItemVM
                    {
                        MealTypeId = item.MealTypeId,
                        NutritionalItemId = item.NutritionalItemId,
                        Qty = item.Quantity,
                        IsUpdated = true
                    });
                }
                else
                {
                    // INSERT
                    item.CreatedBy = userId;
                    item.CreatedOn = DateTime.UtcNow;

                    _context.MealPlan.Add(item);

                    response.SavedItems.Add(new SavedMealItemVM
                    {
                        MealTypeId = item.MealTypeId,
                        NutritionalItemId = item.NutritionalItemId,
                        Qty = item.Quantity,
                        IsInserted = true
                    });
                }
            }

            // 4️⃣ Save changes
            _context.SaveChanges();
            return response;
        }


        public SaveMealPlanResponseVM EditMealPlan(SaveMealPlanVM model)
        {
            int userId = model.UserId;
            int tenantId = model.TenantId;
            var date = model.Date;

            SaveMealPlanResponseVM response = new SaveMealPlanResponseVM
            {
                StatusCode = 200,
                Message = "Meal plan updated successfully",
                Date = date
            };

            // 1️⃣ Load existing
            var existing = _context.MealPlan
                .Where(m => m.UserId == userId &&
                            m.TenantId == tenantId &&
                            m.Date == date &&
                            !m.IsDeleted)
                .ToList();

            // 2️⃣ Convert incoming
            var incoming = model.ToModelList();
            var processedKeys = new HashSet<string>();

            // 3️⃣ INSERT + UPDATE
            foreach (var item in incoming)
            {
                string key = $"{item.MealTypeId}-{item.NutritionalItemId}";
                processedKeys.Add(key);

                var match = existing.FirstOrDefault(x =>
                    x.MealTypeId == item.MealTypeId &&
                    x.NutritionalItemId == item.NutritionalItemId
                );

                if (match == null)
                {
                    // INSERT
                    item.CreatedBy = userId;
                    item.CreatedOn = DateTime.UtcNow;

                    _context.MealPlan.Add(item);

                    response.SavedItems.Add(new SavedMealItemVM
                    {
                        MealTypeId = item.MealTypeId,
                        NutritionalItemId = item.NutritionalItemId,
                        Qty = item.Quantity,
                        IsInserted = true
                    });
                }
                else
                {
                    // UPDATE
                    if (match.Quantity != item.Quantity)
                    {
                        match.Quantity = item.Quantity;
                        match.UpdatedBy = userId;
                        match.UpdatedOn = DateTime.UtcNow;

                        response.SavedItems.Add(new SavedMealItemVM
                        {
                            MealTypeId = item.MealTypeId,
                            NutritionalItemId = item.NutritionalItemId,
                            Qty = item.Quantity,
                            IsUpdated = true
                        });
                    }
                }
            }

            // 4️⃣ DELETE items not in UI anymore
            foreach (var old in existing)
            {
                string key = $"{old.MealTypeId}-{old.NutritionalItemId}";

                if (!processedKeys.Contains(key))
                {
                    _context.MealPlan.Remove(old);

                    response.SavedItems.Add(new SavedMealItemVM
                    {
                        MealTypeId = old.MealTypeId,
                        NutritionalItemId = old.NutritionalItemId,
                        Qty = old.Quantity,
                        IsDeleted = true
                    });
                }
            }

            // 5️⃣ Save
            _context.SaveChanges();
            return response;
        }

        public NutritionalDropdownsVM GetDropdowns(int tenantId)
        {
            var result = new NutritionalDropdownsVM();

            // Meal Types
            result.MealTypes = _context.MealTypes
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            // Vitamins
            result.Vitamins = _context.Vitamins
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            // Focus Tags
            result.FocusTags = _context.NutritionalFocuses
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)

                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            // Nutritional Item Types
            result.ItemTypes = _context.NutritionalIteamType
                .Where(x => !x.IsDeleted && x.TenantId == tenantId)
                .Select(x => new Filters { Id = x.Id, Name = x.Name })
                .ToList();

            return result;
        }

    }
}
