using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.Team
{
    public class MTeamVM
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }

        public static MTeamVM ToViewModel(MTeam team)
        {
            return new MTeamVM
            {
                TeamId = team.TeamId,
                Name = team.Name,
                TenantId = team.TenantId,
            };
        }
        public static List<MTeamVM> ToViewModelList(List<MTeam> list)
        {
            List<MTeamVM> teamsList= new List<MTeamVM>();
            foreach(MTeam team in list) 
            {
             teamsList.Add(ToViewModel(team));  

            }
            return teamsList;
        }
    }
}
