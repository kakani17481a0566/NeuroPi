using Microsoft.AspNetCore.Mvc;
using System.Net;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.Services.Interfaces;
using NeuroPi.ViewModel.Team;

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
        public IActionResult GetAll()
        {
            var teams = _teamService.GetAllTeams();

            var result = teams.Select(t => new MTeamVM
            {
                TeamId = t.TeamId,
                Name = t.Name,
                TenantId = t.TenantId
            }).ToList();

            return Ok(ResponseResult<List<MTeamVM>>.SuccessResponse(HttpStatusCode.OK, result, "Teams fetched"));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
                return NotFound(ResponseResult<MTeamVM>.FailResponse(HttpStatusCode.NotFound, "Team not found"));

            var result = new MTeamVM
            {
                TeamId = team.TeamId,
                Name = team.Name,
                TenantId = team.TenantId
            };

            return Ok(ResponseResult<MTeamVM>.SuccessResponse(HttpStatusCode.OK, result, "Team found"));
        }

        [HttpPost]
        public IActionResult Create([FromBody] MTeamInsertVM vm)
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

            return Ok(ResponseResult<object>.SuccessResponse(HttpStatusCode.Created, result, "Team created"));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MTeamUpdateVM vm)
        {
            var update = new MTeam
            {
                Name = vm.Name,
                TenantId = vm.TenantId,
                UpdatedBy = vm.UpdatedBy
            };

            var updated = _teamService.UpdateTeam(id, update);
            if (updated == null)
                return NotFound(ResponseResult<MTeamVM>.FailResponse(HttpStatusCode.NotFound, "Team not found"));

            var result = new MTeamVM
            {
                TeamId = updated.TeamId,
                Name = updated.Name,
                TenantId = updated.TenantId
            };

            return Ok(ResponseResult<MTeamVM>.SuccessResponse(HttpStatusCode.OK, result, "Team updated"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _teamService.SoftDeleteTeam(id);
            if (!success)
                return NotFound(ResponseResult<string>.FailResponse(HttpStatusCode.NotFound, "Team not found"));

            return Ok(ResponseResult<string>.SuccessResponse(HttpStatusCode.OK, null, "Team deleted"));
        }
    }
}
