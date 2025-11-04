using NeuroPi.Nutrition.ViewModel.Vitamins;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IVitamins
    {
        List<VitaminsResponseVM> GetAllVitamins();

        VitaminsResponseVM GetVitaminById(int id);

        List<VitaminsResponseVM> GetVitaminByTenantId(int tenantId);

        VitaminsResponseVM GetVitaminsByIdAndTenantID(int id, int tenantId);

        VitaminsResponseVM CreateVitamin(VitaminsRequestVm vitaminRequest);

        VitaminsResponseVM UpdateVitamin(int id, int tenantId, VitaminsUpdateVM vitaminUpdate);

        bool DeleteVitamin(int id,int tenantId);
    }
}
