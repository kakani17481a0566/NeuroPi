using NeuroPi.Nutrition.ViewModel.NutritionMasterType;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionMasterType
    {
        List<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypes();

        List<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypeByTenantId(int tenantId);

        NutritionMasterTypeResponseVM GetAllNutritionMasterTypeByIdAndTenantId(int id, int tenantId);

        NutritionMasterTypeResponseVM GetAllNutritionMasterTypeById(int id);

        NutritionMasterTypeResponseVM CreateNutritionMasterType(NutritionMasterTypeRequestVM RequestVM);

        NutritionMasterTypeResponseVM UpdateNutritionMasterType(int id, int tenantId,NutritionMasterTypeUpdateVM RequestVM);

        bool DeleteNutritionMasterType(int id, int tenantId);

    }
}
