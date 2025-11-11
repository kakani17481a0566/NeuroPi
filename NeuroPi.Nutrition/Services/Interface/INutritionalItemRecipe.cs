using NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe;

namespace NeuroPi.Nutrition.Interface
{
    public interface INutritionalItemRecipe
    {
        List<NutritionalItemRecipeResponseVM> GetAllNutritionalItemRecipe();

        NutritionalItemRecipeResponseVM GetNutritionalItemRecipeById(int id);

        List<NutritionalItemRecipeResponseVM> GetNutritionalItemRecipeByTenantId(int tenantId);

        NutritionalItemRecipeResponseVM GetNutritionalItemRecipeByIdAndTenantId(int id, int tenantId);

        NutritionalItemRecipeResponseVM CreateNutritionalItemRecipe(NutritionalItemRecipeRequestVM request);

        NutritionalItemRecipeResponseVM UpdateNutritionalItemRecipeByIdAndTenantId(int id, int tenantId, NutritionalItemRecipeUpdateVM update);

        bool DeleteNutritionalItemRecipeById(int id, int tenantId);
    }
}
