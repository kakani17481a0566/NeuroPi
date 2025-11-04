
using NeuroPi.Nutrition.ViewModel.NutritionalIteamType;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface INutritionalIteamType
    {


        List<NutritionalIteamTypeResponseVM> GetAllNutritionalIteamType();

        NutritionalIteamTypeResponseVM GetNutritionalIteamTypeById(int id);

        NutritionalIteamTypeResponseVM GetNutritionalIteamTypeByTenantId(int tenantId);

        NutritionalIteamTypeResponseVM GetNutritionalIteamTypeByIdAndTenantId(int id, int tenantId);

        NutritionalIteamTypeResponseVM CreateNutritionalIteamType(NutritionalIteamTypeRequestVM request);

        NutritionalIteamTypeResponseVM UpdateNutritionalIteamTypeByIdAndTenantId(int id, int tenantId, NutritionalIteamTypeUpdateVM update);

        bool DeleteNutritionalIteamTypeById(int id, int tenantId);

    }
}
