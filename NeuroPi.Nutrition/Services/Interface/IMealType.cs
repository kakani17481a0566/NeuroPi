using NeuroPi.Nutrition.ViewModel;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IMealType
    {
        List<MealTypeResponseVM> GetAllMealTypes();

        List<MealTypeResponseVM> GetMealTypeByTenantId(int tenantId);

        MealTypeResponseVM GetMealTypeById(int id);

        MealTypeResponseVM GetMealTypeByIdAndTenantId(int id, int tenantId);

        MealTypeResponseVM CreateMealType(MealTypeRequestVM mealTypeRequestVM);

        MealTypeResponseVM UpdateMealType(int id, int tenantId, MealTypeUpdateVM mealTypeUpdateVM);

        bool DeleteMealType(int id, int tenantId);
    }
}
