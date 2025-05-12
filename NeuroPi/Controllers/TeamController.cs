using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.ViewModel.Team;
using NeuroPi.Services.Interface;

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

        // GET: api/Team
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

            return new ResponseResult<List<MTeamVM>>(
                HttpStatusCode.OK,
                result,
                "Teams fetched");
        }

        // GET: api/Team/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null)
            {
                return new ResponseResult<MTeamVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }

            var result = new MTeamVM
            {
                TeamId = team.TeamId,
                Name = team.Name,
                TenantId = team.TenantId
            };

            return new ResponseResult<MTeamVM>(
                HttpStatusCode.OK,
                result,
                "Team found");
        }

        // POST: api/Team
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

            return new ResponseResult<object>(
                HttpStatusCode.Created,
                result,
                "Team created");
        }

        // PUT: api/Team/{id}
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
            {
                return new ResponseResult<MTeamVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }

            var result = new MTeamVM
            {
                TeamId = updated.TeamId,
                Name = updated.Name,
                TenantId = updated.TenantId
            };

            return new ResponseResult<MTeamVM>(
                HttpStatusCode.OK,
                result,
                "Team updated");
        }

        // DELETE: api/Team/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool success = _teamService.SoftDeleteTeam(id);
            if (!success)
            {
                return new ResponseResult<string>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }

            return new ResponseResult<string>(
                HttpStatusCode.OK,
                null,
                "Team deleted");
        }
    }
}
