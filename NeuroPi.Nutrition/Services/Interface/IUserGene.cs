using NeuroPi.Nutrition.ViewModel.UserGene;

namespace NeuroPi.Nutrition.Services.Interface
{
    public interface IUserGene
    {
        List<UserGeneResponseVM> GetUserGene();

        UserGeneResponseVM GetUserGeneById(int id);

        List<UserGeneResponseVM> GetUserGeneByTenantId(int tenantId);

        UserGeneResponseVM GetUserGeneByIdAndTenantId(int id, int tenantId);

        UserGeneResponseVM CreateUserGene(UserGeneRequestVM UserGeneRequestVM);
        UserGeneResponseVM UpdateUserGene(int id, int tenatId, UserGeneUpdateVM UserGeneRequestVM);

        bool DeleteUserGene(int id, int tenantId);

    }
}
