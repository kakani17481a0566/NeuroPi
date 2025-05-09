using NeuroPi.Data;
using NeuroPi.Models;
using NeuroPi.Services.Interface;

namespace NeuroPi.Services.Implementation
{
    public class TeamService : ITeamService
    {
        private readonly NeuroPiDbContext _context;

        public TeamService(NeuroPiDbContext context)
        {
            _context = context;
        }

        public List<MTeam> GetAllTeams()
        {
            return _context.Teams
                .Where(t => t.DeletedAt == null)
                .ToList();
        }

        public MTeam GetTeamById(int id)
        {
            return _context.Teams
                .FirstOrDefault(t => t.TeamId == id && t.DeletedAt == null);
        }

        public MTeam CreateTeam(MTeam team)
        {
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

        public MTeam UpdateTeam(int id, MTeam updatedTeam)
        {
            var existing = _context.Teams.FirstOrDefault(t => t.TeamId == id && t.DeletedAt == null);
            if (existing == null) return null;

            existing.Name = updatedTeam.Name;
            existing.TenantId = updatedTeam.TenantId;
            existing.UpdatedBy = updatedTeam.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return existing;
        }

        public bool SoftDeleteTeam(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == id && t.DeletedAt == null);
            if (team == null) return false;

            team.DeletedAt = DateTime.UtcNow;
            _context.SaveChanges();
            return true;
        }
    }
}
