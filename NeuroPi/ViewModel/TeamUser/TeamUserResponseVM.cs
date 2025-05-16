using System;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.TeamUser
{
    public class TeamUserResponseVM
    {
        public int TeamUserId { get; set; }
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public static TeamUserResponseVM ToViewModel(MTeamUser model)
        {
            return new TeamUserResponseVM
            {
                TeamUserId = model.TeamUserId,
                TeamId = model.TeamId,
                UserId = model.UserId,
                TenantId = model.TenantId,
                CreatedOn = model.CreatedOn,
                UpdatedOn = model.UpdatedOn
            };
        }

        public static List<TeamUserResponseVM> ToViewModelList(List<MTeamUser> models)
        {
            return models.Select(m => ToViewModel(m)).ToList();
        }
    }
}
