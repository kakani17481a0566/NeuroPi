using Microsoft.AspNetCore.Mvc;
using System.Net;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.ViewModel.Team;
using NeuroPi.Services.Interface;
using System.Threading.Tasks;

namespace NeuroPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ResponseResult<List<MTeamVM>>> GetAll()
        {
            var teams = _teamService.GetAllTeams();

            var result = teams.Select(t => new MTeamVM
            {
                TeamId = t.TeamId,
                Name = t.Name,
                TenantId = t.TenantId
            }).ToList();

            return ResponseResult<List<MTeamVM>>.SuccessResponse(HttpStatusCode.OK, result, "Teams fetched");
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult<MTeamVM>> GetById(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
                return ResponseResult<MTeamVM>.FailResponse(HttpStatusCode.NotFound, "Team not found");

            var result = new MTeamVM
            {
                TeamId = team.TeamId,
                Name = team.Name,
                TenantId = team.TenantId
            };

            return ResponseResult<MTeamVM>.SuccessResponse(HttpStatusCode.OK, result, "Team found");
        }

        [HttpPost]
        public async Task<ResponseResult<object>> Create([FromBody] MTeamInsertVM vm)
        {
            var team = new MTeam
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                CreatedBy = vm.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            var created = _teamService.CreateTeam(team);
            var result = new { created.TeamId };

            return ResponseResult<object>.SuccessResponse(HttpStatusCode.Created, result, "Team created");
        }

        [HttpPut("{id}")]
        public async Task<ResponseResult<MTeamVM>> Update(int id, [FromBody] MTeamUpdateVM vm)
        {
            var update = new MTeam
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                UpdatedBy = vm.UpdatedBy
            };

            var updated = _teamService.UpdateTeam(id, update);
            if (updated == null)
                return ResponseResult<MTeamVM>.FailResponse(HttpStatusCode.NotFound, "Team not found");

            var result = new MTeamVM
            {
                TeamId = updated.TeamId,
                Name = updated.Name,
                TenantId = updated.TenantId
            };

            return ResponseResult<MTeamVM>.SuccessResponse(HttpStatusCode.OK, result, "Team updated");
        }

        [HttpDelete("{id}")]
        public async Task<ResponseResult<string>> Delete(int id)
        {
            bool success = _teamService.SoftDeleteTeam(id);
            if (!success)
                return ResponseResult<string>.FailResponse(HttpStatusCode.NotFound, "Team not found");

            return ResponseResult<string>.SuccessResponse(HttpStatusCode.OK, null, "Team deleted");
        }
    }
}
