using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.Team;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface ITeamService
    {
        List<MTeam> GetAllTeams();
        MTeamVM GetTeamById(int id);
        MTeam CreateTeam(MTeam team);
        MTeamVM UpdateTeam(int id,int tenantId, MTeamUpdateVM updatedTeam);
        bool DeleteTeam(int id,int tenantId);

        List<MTeamVM> GetAllTeamsByTenantId(int tenantId);

        MTeamVM GetByIdAndTenantId(int id,int tenantId);
    }
}
