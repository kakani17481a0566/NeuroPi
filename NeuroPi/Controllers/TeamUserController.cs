using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.TeamUser;
using NeuroPi.UserManagment.Response;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUserController : ControllerBase
    {
        private readonly ITeamUserService _teamUserService;

        public TeamUserController(ITeamUserService teamUserService)
        {
            _teamUserService = teamUserService;
        }

        [HttpGet]
        public ResponseResult<List<TeamUserResponseVM>> GetAllTeamUsers()
        {
            var teamUsers = _teamUserService.GetTeamUsers();
            if (!teamUsers.Any())
                return new ResponseResult<List<TeamUserResponseVM>>(HttpStatusCode.NotFound, null, "No team users found.");

            return new ResponseResult<List<TeamUserResponseVM>>(HttpStatusCode.OK, teamUsers);
        }

        [HttpGet("{id}")]
        public ResponseResult<TeamUserResponseVM> GetTeamUserById(int id)
        {
            var teamUser = _teamUserService.GetTeamUserById(id);
            if (teamUser == null)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.NotFound, null, $"Team user with ID {id} not found.");

            return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.OK, teamUser);
        }

        [HttpPost]
        public ResponseResult<TeamUserResponseVM> CreateTeamUser([FromBody] TeamUserRequestVM teamUserRequest)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request body");

            var created = _teamUserService.AddTeamUser(teamUserRequest);
            if (created == null)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.InternalServerError, null, "Error creating team user");

            return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.Created, created, "Team user created successfully");
        }

        [HttpPut("{tenantId}/{id}")]
        public ResponseResult<TeamUserResponseVM> UpdateTeamUser(int tenantId, int id, [FromBody] TeamUserUpdateVM updateModel)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request body");

            var updated = _teamUserService.UpdateTeamUser(id, tenantId, updateModel);
            if (updated == null)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.NotFound, null, $"Team user with ID {id} and tenant ID {tenantId} not found.");

            return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.OK, updated, "Team user updated successfully");
        }

        [HttpDelete("{tenantId}/{id}")]
        public ResponseResult<string> DeleteTeamUser(int tenantId, int id)
        {
            var deleted = _teamUserService.DeleteTeamUserByTenantIdAndId(tenantId, id);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Team user not found");

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Team user deleted successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TeamUserResponseVM>> GetTeamUsersByTenantId(int tenantId)
        {
            var teamUsers = _teamUserService.GetTeamUsersByTenantId(tenantId);
            if (!teamUsers.Any())
                return new ResponseResult<List<TeamUserResponseVM>>(HttpStatusCode.NotFound, null, $"No team users found for tenant ID {tenantId}");

            return new ResponseResult<List<TeamUserResponseVM>>(HttpStatusCode.OK, teamUsers);
        }

        [HttpGet("tenant/{tenantId}/{id}")]
        public ResponseResult<TeamUserResponseVM> GetTeamUserByTenantIdAndId(int tenantId, int id)
        {
            var teamUser = _teamUserService.GetTeamUserByTenantIdAndId(tenantId, id);
            if (teamUser == null)
                return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.NotFound, null, $"Team user not found for tenant ID {tenantId} and ID {id}");

            return new ResponseResult<TeamUserResponseVM>(HttpStatusCode.OK, teamUser);
        }
    }
}
