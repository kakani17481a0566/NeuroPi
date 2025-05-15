using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Team;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class TeamServiceImpl : ITeamService
    {
        private readonly NeuroPiDbContext _context;

        public TeamServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

      
        public List<MTeam> GetAllTeams()
        {
            return _context.Teams
                .Where(t => !t.IsDeleted)
                .ToList();
        }

      
        public MTeamVM GetTeamById(int id)
        {
            var result = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted);
            if (result != null)
            {
                return MTeamVM.ToViewModel(result);
            }
            return null;

        }

       
        public MTeam CreateTeam(MTeam team)
        {
            team.IsDeleted = false;
            _context.Teams.Add(team);
            _context.SaveChanges();
            return team;
        }

  
        public MTeamVM UpdateTeam(int id, int tenantId,MTeamUpdateVM updatedTeam)
        {
            var existing = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted && t.TenantId==tenantId);
            if (existing == null) return null;

            existing.Name = updatedTeam.Name;
            existing.TenantId = tenantId;
            existing.UpdatedBy = updatedTeam.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return MTeamVM.ToViewModel(existing);
        }

      
        public bool DeleteTeam(int id, int tenantId)
        {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted && t.TenantId==tenantId);
            if (team == null) return false;

            team.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }

        public List<MTeamVM> GetAllTeamsByTenantId(int tenantId)
        {
            var result=_context.Teams.Where(t=>t.TenantId == tenantId && !t.IsDeleted).ToList();
            if(result!=null &&  result.Count()>0)
            {
                return MTeamVM.ToViewModelList(result);
            }
            return null;
        }

        public MTeamVM GetByIdAndTenantId(int id, int tenantId)
        {
            var result=_context.Teams.FirstOrDefault(t=>t.TeamId==id && t.TenantId==tenantId && !t.IsDeleted);
            if (result != null)
            {
                return MTeamVM.ToViewModel(result);
            }
            return null;
        }
    }
}
