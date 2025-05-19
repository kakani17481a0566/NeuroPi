using System.Net;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Response;
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

        public ResponseResult<List<MTeamVM>> GetAllTeamsVM()
        {
            var teams = _context.Teams.Where(t => !t.IsDeleted).ToList();
            var result = MTeamVM.ToViewModelList(teams);
            return new ResponseResult<List<MTeamVM>>(HttpStatusCode.OK, result, "Teams fetched");
        }

        public ResponseResult<MTeamVM> GetTeamVMById(int id)
        {
            var team = _context.Teams.FirstOrDefault(t => t.TeamId == id && !t.IsDeleted);
            if (team == null)
            {
                return new ResponseResult<MTeamVM>(HttpStatusCode.NotFound, null, "Team not found");
            }

            var vm = MTeamVM.ToViewModel(team);
            return new ResponseResult<MTeamVM>(HttpStatusCode.OK, vm, "Team found");
        }

        public ResponseResult<MTeamVM> CreateTeamVM(MTeamInsertVM vm)
        {
            var team = new MTeam
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            _context.Teams.Add(team);
            _context.SaveChanges();

            var teamVM = MTeamVM.ToViewModel(team);

            return new ResponseResult<MTeamVM>(
                HttpStatusCode.Created,
                teamVM,
                "Team created successfully");
        }


        public ResponseResult<MTeamVM> UpdateTeamVM(int id, int tenantId, MTeamUpdateVM updatedTeam)
        {
            var existing = _context.Teams.FirstOrDefault(t =>
                t.TeamId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (existing == null)
            {
                return new ResponseResult<MTeamVM>(HttpStatusCode.NotFound, null, "Team not found");
            }

            existing.Name = updatedTeam.Name;
            existing.UpdatedBy = updatedTeam.UpdatedBy;
            existing.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();

            var vm = MTeamVM.ToViewModel(existing);
            return new ResponseResult<MTeamVM>(HttpStatusCode.OK, vm, "Team updated successfully");
        }

        public ResponseResult<string> DeleteTeamVM(int id, int tenantId)
        {
            var team = _context.Teams.FirstOrDefault(t =>
                t.TeamId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (team == null)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Team not found");
            }

            team.IsDeleted = true;
            _context.SaveChanges();

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Team deleted");
        }

        public ResponseResult<List<MTeamVM>> GetAllTeamsByTenantIdVM(int tenantId)
        {
            var teams = _context.Teams
                .Where(t => t.TenantId == tenantId && !t.IsDeleted)
                .ToList();

            if (teams == null || teams.Count == 0)
            {
                return new ResponseResult<List<MTeamVM>>(HttpStatusCode.NotFound, null, "No teams found");
            }

            var result = MTeamVM.ToViewModelList(teams);
            return new ResponseResult<List<MTeamVM>>(HttpStatusCode.OK, result, "Teams fetched successfully");
        }

        public ResponseResult<MTeamVM> GetByIdAndTenantIdVM(int id, int tenantId)
        {
            var team = _context.Teams
                .FirstOrDefault(t => t.TeamId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (team == null)
            {
                return new ResponseResult<MTeamVM>(HttpStatusCode.NotFound, null, "Team not found");
            }

            var vm = MTeamVM.ToViewModel(team);
            return new ResponseResult<MTeamVM>(HttpStatusCode.OK, vm, "Team found successfully");
        }
    }
}
