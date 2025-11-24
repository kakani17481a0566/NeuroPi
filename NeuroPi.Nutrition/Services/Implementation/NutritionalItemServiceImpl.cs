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
        public NutritionalItemResponseVM CreateNutritionalItem(NutritionalItemRequestVM item)
        {
            var newNutrionalItem = NutritionalItemRequestVM.ToModel(item);
            newNutrionalItem.CreatedOn = DateTime.UtcNow;
            _context.NutritionalItems.Add(newNutrionalItem);
            _context.SaveChanges();
            return NutritionalItemResponseVM.ToViewModel(newNutrionalItem);

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

        //public NutritionalItemListResponseVM GetAllItems()
        //{
        //    NutritionalItemListResponseVM result = new NutritionalItemListResponseVM();
        //    List<NutritionalItemDetailsVM> allItems= new List<NutritionalItemDetailsVM>();
        //    var nutritionalItems=_context.NutritionalItems.Where(n=>!n.IsDeleted).ToList();
        //    foreach (var item in nutritionalItems)
        //    {
        //        NutritionalItemDetailsVM nutritonalItem = new NutritionalItemDetailsVM();
        //        nutritonalItem.Id = item.Id;
        //        nutritonalItem.Name = item.Name;
        //        nutritonalItem.CaloriesQuantity = item.CaloriesQuantity;
        //        nutritonalItem.ProteinQuantity = item.ProteinQuantity;
        //        var mealTypes=_context.NutritionalItemMealType.Where(n=>n.NutritionalItemId == item.Id).ToList();
        //        List<int> mealTypesIds=mealTypes.Select(n => n.MealTypeId).ToList();
        //        var vitaminsList=_context.NutritionalItemVitamins.Where(n=>!n.IsDeleted && n.NutritionalItemId==item.Id).ToList();
        //        List<int> vitaminIds = vitaminsList.Select(n => n.VitaminsId).ToList();
        //        nutritonalItem.VitaminIds = vitaminIds;
        //        nutritonalItem.MealTypeIds= mealTypesIds;
        //        var FocusIdsList = _context.NutritionalFocusItem.Where(n => !n.IsDeleted && n.NutritionalItemId ==item.Id).ToList();
        //        List<int> FocusIdList = FocusIdsList.Select(n => n.NutritionalFocusId).ToList();
        //        nutritonalItem.FocusIds = FocusIdList;
        //        var isFavourite = _context.UserFavourites.Any(u=>u.UserId==1);//need to dynamic  code 
        //        nutritonalItem.UserFavourite=isFavourite;
        //        allItems.Add(nutritonalItem);


        //    }
        //    result.AllItems=allItems;
        //    List<MealPlan> mealplansList = new List<MealPlan>();
        //    var date= DateOnly.FromDateTime(DateTime.Now);
        //    var mealPlans = _context.MealPlan.Where(m => m.Date==date && m.UserId==1 && !m.IsDeleted).ToList();// HARD CODED VALUES HERE //NEED TO STOP THE  REPEATED OCCURENCE

        //    foreach (var mealplan in mealPlans)
        //    {
        //        MealPlan mealPlanVM = new MealPlan();
        //        mealPlanVM.MealTypeId=mealplan.MealTypeId;
        //        mealPlanVM.Date=date;
        //        var nutritions = _context.MealPlan.Where(n => n.MealTypeId==mealplan.MealTypeId).Include(m=>m.NutritionalItem).ToList();
        //        List<Items> nutritionsItems = new List<Items>();
        //        foreach (var NutritionItem in nutritions)
        //        {
        //            Items item = new Items();
        //            item.Id=NutritionItem.NutritionalItem.Id;
        //            item.Quantity=NutritionItem.Quantity;
        //            item.CaloriesQuantity=NutritionItem.NutritionalItem.CaloriesQuantity;
        //            item.ProteinQuantity=NutritionItem.NutritionalItem.ProteinQuantity;
        //            item.Type=NutritionItem.NutritionalItem.NutritionalItemTypeId;
        //            var vitaminsList = _context.NutritionalItemVitamins.Where(n => !n.IsDeleted && n.NutritionalItemId==NutritionItem.NutritionalItem.Id).ToList();
        //            List<int> vitaminIds = vitaminsList.Select(n => n.VitaminsId).ToList();
        //            item.VitaminIds=vitaminIds;
        //            var FocusIdsList = _context.NutritionalFocusItem.Where(n => !n.IsDeleted && n.NutritionalItemId == NutritionItem.NutritionalItem.Id).ToList();
        //            List<int> FocusIdList = FocusIdsList.Select(n => n.NutritionalFocusId).ToList();
        //            item.FocusIds = FocusIdList;
        //            var isFavourite = _context.UserFavourites.Any(u => u.UserId==1);//need to dynamic  code 
        //            item.UserFavourite=isFavourite;
        //            nutritionsItems.Add(item);
        //        }
        //        mealPlanVM.Items=nutritionsItems;
        //        mealplansList.Add(mealPlanVM);

        //    }

        //    result.MealPlans=mealplansList.DistinctBy(m=>m.MealTypeId).ToList();

        //    var mealTypesList = _context.MealTypes.Where(mt => !mt.IsDeleted).ToList();
        //    List<Filters> mealTypesFilters = mealTypesList.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
        //    result.MealTypes=mealTypesFilters;

        //    var ItemTypes = _context.NutritionalIteamType.Where(mt => !mt.IsDeleted).ToList();
        //    List<Filters> ItemTypesFilters = ItemTypes.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
        //    result.ItemTypes=ItemTypesFilters;


        //    var Vitamins = _context.Vitamins.Where(mt => !mt.IsDeleted).ToList();
        //    List<Filters> VitaminsFilters = Vitamins.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
        //    result.Vitamins=VitaminsFilters;

        //    var FocusTags = _context.NutritionalFocuses.Where(mt => !mt.IsDeleted).ToList();
        //    List<Filters> FocusTagsFilters = FocusTags.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
        //    result.FocusTags=FocusTagsFilters;



        //    //var meal


        //    return result;
        //}

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
            var nutritionalItemList = _context.NutritionalItems.Where(ni => ni.TenantId == tenantId).ToList();
            if (nutritionalItemList == null || nutritionalItemList.Count == 0)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModelList(nutritionalItemList);
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
            var existingNutritionalItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id == id && ni.TenantId == tenantId && ni.IsDeleted);
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
            existingNutritionalItem.UpdatedBy = item.UpdatedBy;
            existingNutritionalItem.UpdatedOn = DateTime.Now;
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



    }
}
