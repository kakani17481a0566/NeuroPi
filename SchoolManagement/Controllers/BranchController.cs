using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using SchoolManagement.ViewModel.Branch;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        // GET api/branch/by-tenant?tenantId=1
        [HttpGet("by-tenant")]
        public ResponseResult<List<BranchVM>> GetBranchesByTenant([FromQuery] int tenantId)
        {
            var branches = _branchService.GetBranchesByTenantId(tenantId);

            if (branches != null && branches.Count > 0)
                return new ResponseResult<List<BranchVM>>(HttpStatusCode.OK, branches, "Branches fetched successfully");

            return new ResponseResult<List<BranchVM>>(HttpStatusCode.NotFound, null, "No branches found");
        }

        // GET api/branch/{id}?tenantId=1
        [HttpGet("{id}")]
        public ResponseResult<BranchVM> GetBranchById(int id, [FromQuery] int tenantId)
        {
            var branch = _branchService.GetBranchById(id, tenantId);

            if (branch != null)
                return new ResponseResult<BranchVM>(HttpStatusCode.OK, branch, "Branch fetched successfully");

            return new ResponseResult<BranchVM>(HttpStatusCode.NotFound, null, "Branch not found");
        }
    }
}
