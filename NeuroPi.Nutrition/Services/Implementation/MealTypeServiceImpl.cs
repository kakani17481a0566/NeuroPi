using NeuroPi.Nutrition.Data;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;

namespace NeuroPi.Nutrition.Services.Implementation
{
    public class MealTypeServiceImpl : IMealType
    {
        private readonly NeutritionDbContext _context;

        public MealTypeServiceImpl(NeutritionDbContext context)
        {
            _context = context;
        }

        public MealTypeResponseVM CreateMealType(MealTypeRequestVM mealTypeRequestVM)
        {
            var newMealType = MealTypeRequestVM.ToModel(mealTypeRequestVM);
            newMealType.CreatedOn = DateTime.UtcNow;
            _context.MealTypes.Add(newMealType);
            _context.SaveChanges();
            return MealTypeResponseVM.ToViewModel(newMealType);

        }

        public bool DeleteMealType(int id, int tenantId)
        {
            var existingMealType = _context.MealTypes.FirstOrDefault(mt => mt.Id == id && mt.TenantId == tenantId && !mt.IsDeleted);
            if (existingMealType == null)
            {
                return false;
            }
            existingMealType.IsDeleted = true;
            _context.SaveChanges();
            return true;

        }

        public List<MealTypeResponseVM> GetAllMealTypes()
        {
            var mealTypes = _context.MealTypes.Where(mt => !mt.IsDeleted).ToList();
            if (mealTypes != null && mealTypes.Count > 0)
            {
                return MealTypeResponseVM.ToViewModelList(mealTypes);
            }
            return null;

        }

        public MealTypeResponseVM GetMealTypeById(int id)
        {
            var mealType = _context.MealTypes.FirstOrDefault(mt => mt.Id == id && !mt.IsDeleted);
            if (mealType != null)
            {
                return MealTypeResponseVM.ToViewModel(mealType);
            }
            return null;
        }

        public MealTypeResponseVM GetMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var mealType = _context.MealTypes.FirstOrDefault(mt => mt.Id == id && mt.TenantId == tenantId && !mt.IsDeleted);
            if (mealType != null)
            {
                return MealTypeResponseVM.ToViewModel(mealType);
            }
            return null;
        }

        public List<MealTypeResponseVM> GetMealTypeByTenantId(int tenantId)
        {
            var mealTypes = _context.MealTypes.Where(mt => mt.TenantId == tenantId && !mt.IsDeleted).ToList();
            if (mealTypes != null && mealTypes.Count > 0)
            {
                return MealTypeResponseVM.ToViewModelList(mealTypes);
            }
            return null;
        }

        public MealTypeResponseVM UpdateMealType(int id, int tenantId, MealTypeUpdateVM mealTypeUpdateVM)
        {
            var existingMealType = _context.MealTypes.FirstOrDefault(mt => mt.Id == id && mt.TenantId == tenantId && !mt.IsDeleted);
            if (existingMealType == null)
            {
                return null;
            }
            existingMealType.Code = mealTypeUpdateVM.Code;
            existingMealType.Name = mealTypeUpdateVM.Name;
            existingMealType.Description = mealTypeUpdateVM.Description;
            existingMealType.UpdatedBy = mealTypeUpdateVM.UpdatedBy;
            existingMealType.UpdatedOn = mealTypeUpdateVM.UpdatedOn;
            _context.SaveChanges();
            return MealTypeResponseVM.ToViewModel(existingMealType);

        }
    }
}
