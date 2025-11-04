 
using NeuroPi.Nutrition.ViewModel.NutritionalFocus;

namespace NeuroPi.Nutrition.Interface
{
    public interface INutritionalFocus
    {
        List<NutritionalFocusResponseVM> GetAllNutritionalFocus();

        NutritionalFocusResponseVM GetNutritionalFocusById(int id);

        NutritionalFocusResponseVM GetNutritionalFocusByTenantId(int tenantId);

        NutritionalFocusResponseVM GetNutritionalFocusByIdAndTenantId(int id, int tenantId);

        NutritionalFocusResponseVM CreateNutritionalFocus(NutritionalFocusRequestVM request);

        NutritionalFocusResponseVM UpdateNutritionalFocusByIdAndTenantId(int id, int tenantId, NutritionalFocusUpdateVM update);

        bool DeleteNutritionalFocusById(int id, int tenantId);

    }
}
