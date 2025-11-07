using NeuroPi.Nutrition.ViewModel.NutritionalItemMealType;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionalItemMealType
    {
        List<NutritionalItemMealTypeResponseVM> GetNutritionalItemMealType();

        NutritionalItemMealTypeResponseVM GetNutritionalItemMealTypeById(int id);

        NutritionalItemMealTypeResponseVM GetNutritionalItemMealTypeByIdAndTenantId(int id, int tenantId);

        List<NutritionalItemMealTypeResponseVM> GetNutritionalItemMealTypeByTenantId(int tenantId);

        NutritionalItemMealTypeResponseVM CreateNutrionalItemMealType(NutritionalItemMealTypeRequestVM vm);

        NutritionalItemMealTypeResponseVM UpdateNutritionalMealType(int id, int tenantId, NutritionalItemMealTypeUpdateVM vm);

        bool DeleteNutritionalItemMealType(int id, int tenantId);
    }
}
