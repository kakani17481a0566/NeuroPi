using System;
using System.Collections.Generic;
using System.Linq;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.ViewModel.TeamUser;
using Microsoft.EntityFrameworkCore;

namespace NeuroPi.UserManagment.Services.Implementation
{
    public class TeamUserServiceImpl : ITeamUserService
    {
        private readonly NeuroPiDbContext _context;

        public TeamUserServiceImpl(NeuroPiDbContext context)
        {
            _context = context;
        }

        public TeamUserResponseVM AddTeamUser(TeamUserRequestVM teamUserRequest)
        {
            var teamUserModel = TeamUserRequestVM.ToModel(teamUserRequest);
            _context.TeamUsers.Add(teamUserModel);
            var result = _context.SaveChanges();

            return result > 0 ? TeamUserResponseVM.ToViewModel(teamUserModel) : null;
        }

        public bool DeleteTeamUserByTenantIdAndId(int tenantId, int id)
        {
            var teamUser = _context.TeamUsers
                .FirstOrDefault(t => t.TeamUserId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (teamUser == null) return false;

            teamUser.IsDeleted = true;
            teamUser.UpdatedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return true;
        }

        public TeamUserResponseVM GetTeamUserById(int id)
        {
            var teamUser = _context.TeamUsers
                .FirstOrDefault(t => t.TeamUserId == id && !t.IsDeleted);

            return teamUser != null ? TeamUserResponseVM.ToViewModel(teamUser) : null;
        }

        public List<TeamUserResponseVM> GetTeamUsers()
        {
            var teamUsers = _context.TeamUsers
                .Where(t => !t.IsDeleted)
                .ToList();

            return teamUsers.Any() ? TeamUserResponseVM.ToViewModelList(teamUsers) : new List<TeamUserResponseVM>();
        }

        public TeamUserResponseVM UpdateTeamUser(int id, int tenantId, TeamUserUpdateVM updateModel)
        {
            var existingTeamUser = _context.TeamUsers
                .FirstOrDefault(t => t.TeamUserId == id && t.TenantId == tenantId && !t.IsDeleted);

            if (existingTeamUser == null) return null;

            existingTeamUser.TeamId = updateModel.TeamId;
            existingTeamUser.UserId = updateModel.UserId;
            existingTeamUser.UpdatedOn = DateTime.UtcNow;
            existingTeamUser.UpdatedBy = updateModel.UpdatedBy;

            _context.SaveChanges();

            return TeamUserResponseVM.ToViewModel(existingTeamUser);
        }

        public List<TeamUserResponseVM> GetTeamUsersByTenantId(int tenantId)
        {
            var teamUsers = _context.TeamUsers
                .Where(t => t.TenantId == tenantId && !t.IsDeleted)
                .ToList();

            return teamUsers.Any() ? TeamUserResponseVM.ToViewModelList(teamUsers) : new List<TeamUserResponseVM>();
        }

        public TeamUserResponseVM GetTeamUserByTenantIdAndId(int tenantId, int id)
        {
            var teamUser = _context.TeamUsers
                .FirstOrDefault(t => t.TenantId == tenantId && t.TeamUserId == id && !t.IsDeleted);

            return teamUser != null ? TeamUserResponseVM.ToViewModel(teamUser) : null;
        }
    }
}
