using System;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.TeamUser
{
    public class TeamUserUpdateVM
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        //public int TenantId { get; set; }



        [Column("updated_by")]
        public int UpdatedBy { get; set; }

        // Convert Update ViewModel to Model
        public static MTeamUser ToUpdateModel(TeamUserUpdateVM teamUser, MTeamUser existingModel)
        {
            existingModel.TeamId = teamUser.TeamId;
            existingModel.UserId = teamUser.UserId;
            //existingModel.TenantId = teamUser.TenantId;

            existingModel.UpdatedBy = teamUser.UpdatedBy;

            return existingModel;
        }
    }
}
