using System;
using System.ComponentModel.DataAnnotations.Schema;
using NeuroPi.UserManagment.Model;

namespace NeuroPi.UserManagment.ViewModel.TeamUser
{
    public class TeamUserRequestVM
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
        public int TenantId { get; set; }

        public int CreatedBy { get; set; }

        public static MTeamUser ToModel(TeamUserRequestVM vm)
        {
            return new MTeamUser
            {
                TeamId = vm.TeamId,
                UserId = vm.UserId,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
               
            };
        }

        public static MTeamUser ToUpdateModel(TeamUserRequestVM vm, MTeamUser existingModel)
        {
            existingModel.TeamId = vm.TeamId;
            existingModel.UserId = vm.UserId;
            existingModel.UpdatedOn = DateTime.UtcNow;
            return existingModel;
        }
    }
}
