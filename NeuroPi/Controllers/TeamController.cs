using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Team;

namespace NeuroPi.UserManagment.Controllers
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
        public ResponseResult<List<MTeamVM>> GetAll()
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
        public ResponseResult<MTeamVM> GetById(int id)
        {
            var result = _teamService.GetTeamById(id);
            if (result == null)
            {
                return new ResponseResult<MTeamVM>(HttpStatusCode.NotFound,null,"Team not found");
            }

            return new ResponseResult<MTeamVM>(
                HttpStatusCode.OK,
                result,
                "Team found");
        }

        // POST: api/Team
        [HttpPost]
        public ResponseResult<object> Create([FromBody] MTeamInsertVM vm)
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
        public ResponseResult<MTeamVM> Update(int id,int tenantId, [FromBody] MTeamUpdateVM vm)
        {
            var result = _teamService.UpdateTeam(id,tenantId, vm);
            if (result == null)
            {
                return new ResponseResult<MTeamVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }

            return new ResponseResult<MTeamVM>(HttpStatusCode.OK, result, "Team updated Successfully");
        }

        // DELETE: api/Team/{id}
        [HttpDelete("{id}")]
        public ResponseResult<string> Delete(int id,int tenantId)
        {
            bool success = _teamService.DeleteTeam(id,tenantId);
            if (!success)
            {
                return new ResponseResult<string>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }

            return new ResponseResult<string>(HttpStatusCode.OK,null,"Team deleted");
        }
        [HttpGet("tenantId")]
        public ResponseResult<List<MTeamVM>> GetByTenantId(int id)
        {
            var result = _teamService.GetAllTeamsByTenantId(id);
            if (result == null)
            {
                return new ResponseResult<List<MTeamVM>>(HttpStatusCode.NotFound,null,"Team not found");
            }
            return new ResponseResult<List<MTeamVM>>(HttpStatusCode.OK,result,"Team fetched Successfully");
        }
        [HttpGet("id")]
        public ResponseResult<MTeamVM> GetByIdAndTenantId(int id,int tenantId)
        {
            var result = _teamService.GetByIdAndTenantId(id,tenantId);
            if (result == null)
            {
                return new ResponseResult<MTeamVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Team not found");
            }
            return new ResponseResult<MTeamVM>(HttpStatusCode.OK,result,"Team found successfully");
        }
    }
}
