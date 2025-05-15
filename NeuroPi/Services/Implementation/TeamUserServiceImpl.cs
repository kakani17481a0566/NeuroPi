using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.TeamUser;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class TeamUserServiceImpl : ITeamUserService
    {
        private readonly NeuroPiDbContext _context;

        public TeamUserServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public TeamUserResponseVM AddTeamUser(TeamUserRequestVM teamUser)
        {
            var teamUserModel = TeamUserRequestVM.ToModel(teamUser);
            _context.TeamUsers.Add(teamUserModel);
            int result = _context.SaveChanges();

            if (result > 0)
            {
                TeamUserResponseVM response = new TeamUserResponseVM()
                {
                    Id = teamUserModel.TeamUserId,
                    TenantId = teamUserModel.TenantId,
                    UserId = teamUserModel.UserId,
                    TeamId = teamUserModel.UserId,
                };
                return response;
            }

            return null;
        }

        public bool DeleteTeamUserByTenantIdAndId(int tenantId, int id)
        {
            var teamUser = _context.TeamUsers
                .FirstOrDefault(t => t.TeamUserId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (teamUser == null)
            {
                return false; // Team user not found or mismatch with tenantId and id
            }

            // Mark the team user as deleted (soft delete)
            teamUser.IsDeleted = true;
            teamUser.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return true;
        }

        public TeamUserResponseVM GetTeamUserById(int id)
        {
            var result = _context.TeamUsers.FirstOrDefault(t => t.TeamUserId == id && !t.IsDeleted);
            if (result != null)
            {
                return TeamUserResponseVM.ToViewModel(result);
            }
            return null;
        }

        public List<TeamUserResponseVM> GetTeamUsers()
        {
            var result = _context.TeamUsers.Where(t => !t.IsDeleted).ToList();
            if (result != null && result.Count > 0)
            {
                return TeamUserResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public TeamUserResponseVM UpdateTeamUser(int id, TeamUserRequestVM teamUser)
        {
            var teamUserModel = _context.TeamUsers.FirstOrDefault(t => t.TeamUserId == id && !t.IsDeleted);
            if (teamUserModel != null)
            {
                teamUserModel.TeamUserId = id;
                teamUserModel.UserId = teamUser.UserId;
                teamUserModel.TenantId = teamUser.TenantId;
                teamUserModel.TeamId = teamUser.TeamId;
                teamUserModel.UpdatedOn = DateTime.UtcNow;
                _context.SaveChanges();
                return TeamUserResponseVM.ToViewModel(teamUserModel);
            }
            return null;
        }

        // Keep only one definition of GetTeamUsersByTenantId
        public List<TeamUserResponseVM> GetTeamUsersByTenantId(int tenantId)
        {
            var result = _context.TeamUsers.Where(t => t.TenantId == tenantId && !t.IsDeleted).ToList();
            if (result != null && result.Count > 0)
            {
                return TeamUserResponseVM.ToViewModelList(result);
            }
            return null;
        }

        public TeamUserResponseVM GetTeamUserByTenantIdAndId(int tenantId, int id)
        {
            var result = _context.TeamUsers
                                 .FirstOrDefault(t => t.TenantId == tenantId && t.TeamUserId == id && !t.IsDeleted);

            if (result != null)
            {
                return TeamUserResponseVM.ToViewModel(result);
            }

            return null; // Return null if no matching team user is found
        }

    }
}
