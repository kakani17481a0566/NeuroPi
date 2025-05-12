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

        public void DeleteTeamUser(int id)
        {
            var teamuser = _context.TeamUsers.FirstOrDefault(t => t.TeamUserId == id);
            if (teamuser != null)
            {
                _context.TeamUsers.Remove(teamuser);
                _context.SaveChanges();
            }


        }

        public TeamUserResponseVM GetTeamUserById(int id)
        {
            var result = _context.TeamUsers.FirstOrDefault(t => t.TeamUserId == id);
            if (result != null)
            {
                return TeamUserResponseVM.ToViewModel(result);
            }
            return null;
        }
        public List<TeamUserResponseVM> GetTeamUsers()
        {
            var result = _context.TeamUsers.ToList();
            if (result != null && result.Count > 0)
            {
                return TeamUserResponseVM.ToViewModelList(result);
            }
            return null;

        }

        public TeamUserResponseVM UpdateTeamUser(int id, TeamUserRequestVM teamUser)
        {
            var teamUserModel = _context.TeamUsers.FirstOrDefault(t => t.TeamUserId == id);
            if (teamUserModel != null)
            {
                teamUserModel.TeamUserId = id;
                teamUserModel.UserId = teamUser.UserId;
                teamUserModel.TenantId = teamUser.TenantId;
                teamUserModel.TeamId = teamUser.TeamId;
                _context.SaveChanges();
                return TeamUserResponseVM.ToViewModel(teamUserModel);
            }
            return null;

        }
    }
}
