using NeuroPi.UserManagment.ViewModel.TeamUser;

public interface ITeamUserService
{
    List<TeamUserResponseVM> GetTeamUsers();

    TeamUserResponseVM GetTeamUserById(int id);

    TeamUserResponseVM AddTeamUser(TeamUserRequestVM teamUser);

    TeamUserResponseVM UpdateTeamUser(int id, TeamUserRequestVM teamUser);

    bool DeleteTeamUserByTenantIdAndId(int tenantId, int id); 

    List<TeamUserResponseVM> GetTeamUsersByTenantId(int tenantId);

    TeamUserResponseVM GetTeamUserByTenantIdAndId(int tenantId, int id);
}
