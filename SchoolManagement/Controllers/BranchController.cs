using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Branch;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
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

        [HttpGet("get-all")]
        public IActionResult GetAllBranches([FromQuery] int tenantId)
        {
            var data = _branchService.GetAllBranches(tenantId);
            return new ResponseResult<List<BranchResponseVM>>(HttpStatusCode.OK, data, "Branches fetched successfully");
        }
    }
}
