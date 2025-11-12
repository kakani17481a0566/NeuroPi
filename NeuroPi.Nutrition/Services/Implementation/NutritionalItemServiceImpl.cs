using Microsoft.EntityFrameworkCore;
using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Model;
using NeuroPi.Nutrition.Services.Interface;
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

        public NutritionalItemListResponseVM GetAllItems()
        {
            NutritionalItemListResponseVM result = new NutritionalItemListResponseVM();
            List<NutritionalItemDetailsVM> allItems= new List<NutritionalItemDetailsVM>();
            var nutritionalItems=_context.NutritionalItems.Where(n=>!n.IsDeleted).ToList();
            foreach (var item in nutritionalItems)
            {
                NutritionalItemDetailsVM nutritonalItem = new NutritionalItemDetailsVM();
                nutritonalItem.Id = item.Id;
                nutritonalItem.Name = item.Name;
                nutritonalItem.CaloriesQuantity = item.CaloriesQuantity;
                nutritonalItem.ProteinQuantity = item.ProteinQuantity;
                var mealTypes=_context.NutritionalItemMealType.Where(n=>n.NutritionalItemId == item.Id).ToList();
                List<int> mealTypesIds=mealTypes.Select(n => n.MealTypeId).ToList();
                var vitaminsList=_context.NutritionalItemVitamins.Where(n=>!n.IsDeleted && n.NutritionalItemId==item.Id).ToList();
                List<int> vitaminIds = vitaminsList.Select(n => n.VitaminsId).ToList();
                nutritonalItem.VitaminIds = vitaminIds;
                nutritonalItem.MealTypeIds= mealTypesIds;
                var FocusIdsList = _context.NutritionalFocusItem.Where(n => !n.IsDeleted && n.NutritionalItemId ==item.Id).ToList();
                List<int> FocusIdList = FocusIdsList.Select(n => n.NutritionalFocusId).ToList();
                nutritonalItem.FocusIds = FocusIdList;
                var isFavourite = _context.UserFavourites.Any(u=>u.UserId==1);//need to dynamic  code 
                nutritonalItem.UserFavourite=isFavourite;
                allItems.Add(nutritonalItem);
            }
            result.AllItems=allItems;
            List<MealPlan> mealplansList = new List<MealPlan>();
            var date= DateOnly.FromDateTime(DateTime.Now);
            var mealPlans = _context.MealPlan.Where(m => m.Date==date && m.UserId==1 && !m.IsDeleted).ToList();// HARD CODED VALUES HERE //NEED TO STOP THE  REPEATED OCCURENCE
            
            foreach (var mealplan in mealPlans)
            {
                MealPlan mealPlanVM = new MealPlan();
                mealPlanVM.MealTypeId=mealplan.MealTypeId;
                mealPlanVM.Date=date;
                var nutritions = _context.MealPlan.Where(n => n.MealTypeId==mealplan.MealTypeId).Include(m=>m.NutritionalItem).ToList();
                List<Items> nutritionsItems = new List<Items>();
                foreach (var NutritionItem in nutritions)
                {
                    Items item = new Items();
                    item.Id=NutritionItem.NutritionalItem.Id;
                    item.Quantity=NutritionItem.Quantity;
                    item.CaloriesQuantity=NutritionItem.NutritionalItem.CaloriesQuantity;
                    item.ProteinQuantity=NutritionItem.NutritionalItem.ProteinQuantity;
                    item.Type=NutritionItem.NutritionalItem.NutritionalItemTypeId;
                    var vitaminsList = _context.NutritionalItemVitamins.Where(n => !n.IsDeleted && n.NutritionalItemId==NutritionItem.NutritionalItem.Id).ToList();
                    List<int> vitaminIds = vitaminsList.Select(n => n.VitaminsId).ToList();
                    item.VitaminIds=vitaminIds;
                    var FocusIdsList = _context.NutritionalFocusItem.Where(n => !n.IsDeleted && n.NutritionalItemId == NutritionItem.NutritionalItem.Id).ToList();
                    List<int> FocusIdList = FocusIdsList.Select(n => n.NutritionalFocusId).ToList();
                    item.FocusIds = FocusIdList;
                    var isFavourite = _context.UserFavourites.Any(u => u.UserId==1);//need to dynamic  code 
                    item.UserFavourite=isFavourite;
                    nutritionsItems.Add(item);
                }
                mealPlanVM.Items=nutritionsItems;
                mealplansList.Add(mealPlanVM);

            }
            
            result.MealPlans=mealplansList.DistinctBy(m=>m.MealTypeId).ToList();

            var mealTypesList = _context.MealTypes.Where(mt => !mt.IsDeleted).ToList();
            List<Filters> mealTypesFilters = mealTypesList.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
            result.MealTypes=mealTypesFilters;

            var ItemTypes = _context.NutritionalIteamType.Where(mt => !mt.IsDeleted).ToList();
            List<Filters> ItemTypesFilters = ItemTypes.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
            result.ItemTypes=ItemTypesFilters;


            var Vitamins = _context.Vitamins.Where(mt => !mt.IsDeleted).ToList();
            List<Filters> VitaminsFilters = Vitamins.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
            result.Vitamins=VitaminsFilters;

            var FocusTags = _context.NutritionalFocuses.Where(mt => !mt.IsDeleted).ToList();
            List<Filters> FocusTagsFilters = FocusTags.Select(mt => new Filters { Id=mt.Id, Name=mt.Name }).ToList();
            result.FocusTags=FocusTagsFilters;



            //var meal


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
            var nutritionalItemList = _context.NutritionalItems.Where(ni=> ni.TenantId == tenantId).ToList();
            if(nutritionalItemList == null || nutritionalItemList.Count ==0)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModelList(nutritionalItemList);
        }

        public NutritionalItemResponseVM GetNutritionalItemByIdAndTenantId(int id, int tenantId)
        {
            var nutritionItem = _context.NutritionalItems.FirstOrDefault(ni => ni.Id ==id &&ni.TenantId == tenantId && !ni.IsDeleted);
            if (nutritionItem == null)
            {
                return null;
            }
            return NutritionalItemResponseVM.ToViewModel(nutritionItem);


        }

        public List<NutritionalItemResponseVM> GetNutritionalItemResponses()
        {
            var nutritionalItemList = _context.NutritionalItems.Where(ni => !ni.IsDeleted).ToList();
            if(nutritionalItemList == null)
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
            existingNutritionalItem.Edible = item.Edible;
            existingNutritionalItem.UpdatedBy = item.UpdatedBy;
            existingNutritionalItem.UpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return NutritionalItemResponseVM.ToViewModel(existingNutritionalItem);

        }
    }
}
