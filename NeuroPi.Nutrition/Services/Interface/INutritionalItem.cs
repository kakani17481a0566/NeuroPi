using NeuroPi.Nutrition.ViewModel;
using NeuroPi.Nutrition.ViewModel.NutritionalItem;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionalItem
    {
        List<NutritionalItemResponseVM> GetNutritionalItemResponses();
        NutritionalItemResponseVM GetNutrionalItemById(int id); 

        NutritionalItemResponseVM GetNutritionalItemByIdAndTenantId(int id, int tenantId);

        List<NutritionalItemResponseVM> GetNutrionalItemByTenantId(int tenantId);

        NutritionalItemResponseVM CreateNutritionalItem(NutritionalItemRequestVM item);

        NutritionalItemResponseVM UpdateNutritionalItem(int id, int tenantId,NutritionalItemUpdateVM item);

        bool DeleteNutritionalItem(int id, int tenantId);

        NutritionalItemListResponseVM GetAllItems();

        SaveMealPlanResponseVM SaveMealPlan(SaveMealPlanVM model);

        SaveMealPlanResponseVM EditMealPlan(SaveMealPlanVM model);




    }
}
