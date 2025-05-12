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
        public static List<TeamUserResponseVM> ToViewModelList(List<MTeamUser> users)
        {
            List<TeamUserResponseVM> teamUserResponseVMs = new List<TeamUserResponseVM>();
            foreach (MTeamUser user in users)
            {
                teamUserResponseVMs.Add(ToViewModel(user));
            }
            return teamUserResponseVMs;
        }
    }
}
