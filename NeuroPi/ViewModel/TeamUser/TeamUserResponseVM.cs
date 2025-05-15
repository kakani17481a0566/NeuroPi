using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.TeamUser
{
    public class TeamUserResponseVM
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public MTenant Tenant { get; set; }

        public int UserId { get; set; }

        public MUser User { get; set; }

        public int TeamId { get; set; }

        public MTeam Team { get; set; }

        public static TeamUserResponseVM ToViewModel(MTeamUser user)
        {
            return new TeamUserResponseVM
            {
                Id = user.TeamUserId,
                TenantId = user.TenantId,
                TeamId = user.TeamId,
                UserId = user.UserId,
                User = user.User,
                Team = user.Team,
            };
        }
        public static List<TeamUserResponseVM> ToViewModelList(List<MTeamUser> teamUsers)
        {
            return teamUsers.Select(t => new TeamUserResponseVM
            {
                Id = t.TeamUserId,
                TenantId = t.TenantId,
                UserId = t.UserId,
                TeamId = t.TeamId,
                // Include any other fields as required
            }).ToList();
        }

    }
}
