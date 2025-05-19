using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Team;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public ResponseResult<List<MTeamVM>> GetAll()
        {
            return _teamService.GetAllTeamsVM();
        }

        [HttpGet("{id}")]
        public ResponseResult<MTeamVM> GetById(int id)
        {
            return _teamService.GetTeamVMById(id);
        }

        [HttpPost]
        public ResponseResult<MTeamVM> Create([FromBody] MTeamInsertVM vm)
        {
            return _teamService.CreateTeamVM(vm);
        }

        [HttpPut("{id}")]
        public ResponseResult<MTeamVM> Update(int id, int tenantId, [FromBody] MTeamUpdateVM vm)
        {
            return _teamService.UpdateTeamVM(id, tenantId, vm);
        }

        [HttpDelete("{id}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            return _teamService.DeleteTeamVM(id, tenantId);
        }

        [HttpGet("tenantId")]
        public ResponseResult<List<MTeamVM>> GetByTenantId(int id)
        {
            return _teamService.GetAllTeamsByTenantIdVM(id);
        }

        [HttpGet("id")]
        public ResponseResult<MTeamVM> GetByIdAndTenantId(int id, int tenantId)
        {
            return _teamService.GetByIdAndTenantIdVM(id, tenantId);
        }
    }
}
