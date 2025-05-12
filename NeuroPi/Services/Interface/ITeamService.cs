using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface ITeamService
    {
        List<MTeam> GetAllTeams();
        MTeam GetTeamById(int id);
        MTeam CreateTeam(MTeam team);
        MTeam UpdateTeam(int id, MTeam updatedTeam);
        bool SoftDeleteTeam(int id);
    }
}
