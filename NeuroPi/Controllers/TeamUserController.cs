using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.TeamUser;

namespace NeuroPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUserController : ControllerBase
    {
        private readonly ITeamUserService teamUserService;
        public TeamUserController(ITeamUserService userService)
        {
            teamUserService= userService;
            
        }
        [HttpGet]
        public ResponseResult<List<TeamUserResponseVM>> GetAllTeamUsers()
        {
            var result = teamUserService.GetTeamUsers();
            if (result == null)
            {
                return ResponseResult<List<TeamUserResponseVM>>.FailResponse(HttpStatusCode.NotFound, "No data for team users");
            }
            return ResponseResult<List<TeamUserResponseVM>>.SuccessResponse(HttpStatusCode.OK, result, "Team users fetched successfully");
        }
        [HttpGet("id")]
        public ResponseResult<TeamUserResponseVM> GetById(int id)
        {
            var result = teamUserService.GetTeamUserById(id);
            if (result == null)
            {
                return ResponseResult<TeamUserResponseVM>.FailResponse(HttpStatusCode.NotFound, "No data for team users");
            }
            return ResponseResult<TeamUserResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Team users fetched successfully");
        }

        [HttpPost]
        public ResponseResult<TeamUserResponseVM> AddTeamUser(TeamUserRequestVM teamuser)
        {
            var result = teamUserService.AddTeamUser(teamuser);
            if (result == null)
            {
                return ResponseResult<TeamUserResponseVM>.FailResponse(HttpStatusCode.NotAcceptable, "No data created for  team users");
            }
            return ResponseResult<TeamUserResponseVM>.SuccessResponse(HttpStatusCode.Created, result, "Team users fetched successfully");
        }
        [HttpPut("id")]

        public ResponseResult<TeamUserResponseVM> UpdtaeTeamUserById(int id,TeamUserRequestVM teamUser)
        {
            var result = teamUserService.UpdateTeamUser(id, teamUser);
            if (result == null)
            {
                return ResponseResult<TeamUserResponseVM>.FailResponse(HttpStatusCode.NotFound, "No data for team users");
            }
            return ResponseResult<TeamUserResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Team users Updated  successfully");
        }

        [HttpDelete("id")]
        public ResponseResult<Object> DeleteById(int id)
        {
             teamUserService.DeleteTeamUser(id);
            
            return ResponseResult<Object>.SuccessResponse(HttpStatusCode.OK, null, "Team users Deleted successfully");
        }




    }
    }
