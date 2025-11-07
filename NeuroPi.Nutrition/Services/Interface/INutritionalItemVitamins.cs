using NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionalItemVitamins
    {
        List<NutritionalItemVitaminsResponseVM> GetNutritionalItemVitamins();

        NutritionalItemVitaminsResponseVM GetNutritionalItemVitaminsById(int id);

        NutritionalItemVitaminsResponseVM GetNutritionalItemVitaminsByIdAndTenantId(int id, int tenantId);

        List<NutritionalItemVitaminsResponseVM > GetNutritionalVitaminByTenantId(int tenantId);

        NutritionalItemVitaminsResponseVM CreateNutritionalItemVitamins(NutritionalItemVitaminsRequestVM request);

        NutritionalItemVitaminsResponseVM UpdateNutritionalItemVitamins(int id, int tenantId, NutritionalItemVitaminsUpdateVM request);

        bool DeleteNutritionalItemVitamins(int id, int tenantId);
    }
}
