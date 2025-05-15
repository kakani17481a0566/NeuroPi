using NeuroPi.UserManagment.ViewModel.TeamUser;

public interface ITeamUserService
{
    List<TeamUserResponseVM> GetTeamUsers();

    TeamUserResponseVM GetTeamUserById(int id);

    TeamUserResponseVM AddTeamUser(TeamUserRequestVM teamUser);

    TeamUserResponseVM UpdateTeamUser(int id, TeamUserRequestVM teamUser);

    bool DeleteTeamUserByTenantIdAndId(int tenantId, int id); // Safe delete method

    List<TeamUserResponseVM> GetTeamUsersByTenantId(int tenantId); // Method for getting team users by tenantId

    TeamUserResponseVM GetTeamUserByTenantIdAndId(int tenantId, int id); // New method to get by tenantId and id
}
