using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
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
