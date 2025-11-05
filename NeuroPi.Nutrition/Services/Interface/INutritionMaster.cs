using NeuroPi.Nutrition.ViewModel.NutritionMaster;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionMaster
    {
        List<NutritionMasterResponseVM> GetNutritionMaster();

        NutritionMasterResponseVM GetNutritionMasterById(int id);

        List<NutritionMasterResponseVM> GetNutritionMasterByTenantId(int tenantId);


        NutritionMasterResponseVM GetNutritionMasterByIdAndTenantId(int id, int tenantId);


        NutritionMasterResponseVM CreateNutritionMaster(NutritionMasterRequestVM request);

        NutritionMasterResponseVM UpdateNutritionMaster(int id, int tenantId, NutritionMasterUpdateVM request);

        bool DeleteNutritionMaster(int id, int tenantId);






    }
}
