using NeuroPi.Nutrition.ViewModel.GeneNutritionalFocus;
using NeuroPi.Nutrition.ViewModel.Genes;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IGenGenesService
    {
        List<GenGenesResponseVM> GetAllGenes();

        GenGenesResponseVM GetGenesById(int id);

        GenGenesResponseVM GetGenesByTenantId(int tenantId);

        GenGenesResponseVM GetGenesByIdAndTenantId(int id, int tenantId);

        GenGenesResponseVM CreateGenes(GenGenesRequestVM request);

        GenGenesResponseVM UpdateGenesByIdAndTenantId(int id, int tenantId, GenGenesUpdateVM update);

        bool DeleteGenesById(int id,int tenantId);

    }
}
