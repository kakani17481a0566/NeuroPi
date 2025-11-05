using NeuroPi.Nutrition.ViewModel;
   

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IGeneNutritionalFocus
    {

        List<GeneNutritionalFocusResponseVM> GetAllGeneNutritionalFocus();

        GeneNutritionalFocusResponseVM GetGeneNutritionalFocusById(int id);

        //GeneNutritionalFocusResponseVM GetGeneIdById(int id);
        //GeneNutritionalFocusResponseVM GetNutritionalFocusById(int id);

        List<GeneNutritionalFocusResponseVM> GetGeneNutritionalFocusByTenantId(int tenantId);

        GeneNutritionalFocusResponseVM GetGeneNutritionalFocusByIdAndTenantId(int id, int tenantId);

        GeneNutritionalFocusResponseVM CreateGeneNutritionalFocus(GeneNutritionalFocusRequestVM request);

        GeneNutritionalFocusResponseVM UpdateGeneNutritionalFocusByIdAndTenantId(int id, int tenantId, GeneNutritionalFocusUpdateVM update);

        bool    DeleteGeneNutritionalFocusById(int id, int tenantId);
    }
}
