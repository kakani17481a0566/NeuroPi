using NeuroPi.Nutrition.ViewModel.UnplannedMeal;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUnplannedMeal
    {
        List<UnplannedMealResponseVM> GetUnplannedMeals();

        UnplannedMealResponseVM GetUnplannedMealById(int id);

        UnplannedMealResponseVM GetUnplannedMealByIdAndTenantId (int id, int tenantId);

        List<UnplannedMealResponseVM> GetUnplannedMealsByTenantId(int tenantId);

        UnplannedMealResponseVM CreateUnplannedMeal(UnplannedMealRequestVM RequestVM);

        UnplannedMealResponseVM UpdateUnplannedMeal(int id,int tenantId, UnplannedMealUpdateVM UpdateVM);

        bool DeleteUnplannedMeal(int id, int tenantId);
    }
}
