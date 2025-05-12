using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.TeamUser;

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

        // GET: api/TeamUser
        [HttpGet]
        public IActionResult GetAllTeamUsers()
        {
            var result = _teamUserService.GetTeamUsers();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<TeamUserResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No data for team users");
            }

            return new ResponseResult<List<TeamUserResponseVM>>(
                HttpStatusCode.OK,
                result,
                "Team users fetched successfully");
        }

        // GET: api/TeamUser/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _teamUserService.GetTeamUserById(id);
            if (result == null)
            {
                return new ResponseResult<TeamUserResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "No data for team user");
            }

            return new ResponseResult<TeamUserResponseVM>(
                HttpStatusCode.OK,
                result,
                "Team user fetched successfully");
        }

        // POST: api/TeamUser
        [HttpPost]
        public IActionResult AddTeamUser([FromBody] TeamUserRequestVM teamUser)
        {
            var result = _teamUserService.AddTeamUser(teamUser);
            if (result == null)
            {
                return new ResponseResult<TeamUserResponseVM>(
                    HttpStatusCode.NotAcceptable,
                    null,
                    "Failed to create team user");
            }

            return new ResponseResult<TeamUserResponseVM>(
                HttpStatusCode.Created,
                result,
                "Team user created successfully");
        }

        // PUT: api/TeamUser/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTeamUserById(int id, [FromBody] TeamUserRequestVM teamUser)
        {
            var result = _teamUserService.UpdateTeamUser(id, teamUser);
            if (result == null)
            {
                return new ResponseResult<TeamUserResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team user not found");
            }

            return new ResponseResult<TeamUserResponseVM>(
                HttpStatusCode.OK,
                result,
                "Team user updated successfully");
        }

        // DELETE: api/TeamUser/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _teamUserService.DeleteTeamUser(id);

            return new ResponseResult<object>(
                HttpStatusCode.OK,
                null,
                "Team user deleted successfully");
        }
    }
}
