using NeuroPi.ViewModel.TeamUser;

namespace NeuroPi.Services.Interface
{
    public interface ITeamUserService
    {
        List<TeamUserResponseVM> GetTeamUsers();

        TeamUserResponseVM GetTeamUserById(int id);

        TeamUserResponseVM AddTeamUser(TeamUserRequestVM teamUser);

        TeamUserResponseVM UpdateTeamUser( int id,TeamUserRequestVM teamUser);

        void DeleteTeamUser(int id);
    }
}
