using NeuroPi.Models;

namespace NeuroPi.ViewModel.TeamUser
{
    public class TeamUserRequestVM
    {
        public int TeamId { get; set; }

        public int UserId { get; set; }

        public int TenantId { get; set; }

        public static MTeamUser ToModel(TeamUserRequestVM teamUser)
        {
            return new MTeamUser
            {
                TeamId = teamUser.TeamId
                ,
                UserId = teamUser.UserId,
                TenantId = teamUser.TenantId
            };
        }


    }
}
