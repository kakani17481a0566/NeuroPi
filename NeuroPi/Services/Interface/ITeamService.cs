using NeuroPi.Models;
using System.Collections.Generic;

namespace NeuroPi.Services.Interface
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
