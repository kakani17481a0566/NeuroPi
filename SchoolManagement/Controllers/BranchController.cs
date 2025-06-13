using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Branch;

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

        [HttpGet]
        public ResponseResult<List<BranchResponseVM>> GetAllBranches()
        {
            var branches = _branchService.GetAllBranches();
            return new ResponseResult<List<BranchResponseVM>>(HttpStatusCode.OK, branches, "All branches retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<BranchResponseVM> GetBranchById(int id)
        {
            var branch = _branchService.GetBranchById(id);
            return branch == null
                ? new ResponseResult<BranchResponseVM>(HttpStatusCode.NotFound, null, "Branch not found")
                : new ResponseResult<BranchResponseVM>(HttpStatusCode.OK, branch, "Branch retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<BranchResponseVM>> GetBranchesByTenantId(int tenantId)
        {
            var branches = _branchService.GetBranchesByTenantId(tenantId);
            return branches == null
                ? new ResponseResult<List<BranchResponseVM>>(HttpStatusCode.NotFound, null, "No branches found for the specified tenant")
                : new ResponseResult<List<BranchResponseVM>>(HttpStatusCode.OK, branches, "Branches retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<BranchResponseVM> GetBranchByIdAndTenantId(int id, int tenantId)
        {
            var branch = _branchService.GetBranchByIdAndTenantId(id, tenantId);
            return branch == null
                ? new ResponseResult<BranchResponseVM>(HttpStatusCode.NotFound, null, "Branch not found for the specified tenant")
                : new ResponseResult<BranchResponseVM>(HttpStatusCode.OK, branch, "Branch retrieved successfully");
        }
        [HttpPost]
        public ResponseResult<BranchResponseVM> CreateBranch([FromBody] BranchRequestVM branchRequest)
        {
            if (branchRequest == null)
            {
                return new ResponseResult<BranchResponseVM>(HttpStatusCode.BadRequest, null, "Invalid branch data");
            }
            var createdBranch = _branchService.AddBranch(branchRequest);
            return new ResponseResult<BranchResponseVM>(HttpStatusCode.Created, createdBranch, "Branch created successfully");
        }
        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<BranchResponseVM> UpdateBranch(int id, int tenantId, [FromBody] BranchUpdateVM branchUpdate)
        {
            if (branchUpdate == null)
            {
                return new ResponseResult<BranchResponseVM>(HttpStatusCode.BadRequest, null, "Invalid branch update data");
            }
            var updatedBranch = _branchService.UpdateBranch(id, tenantId, branchUpdate);
            return updatedBranch == null
                ? new ResponseResult<BranchResponseVM>(HttpStatusCode.NotFound, null, "Branch not found for the specified ID and tenant")
                : new ResponseResult<BranchResponseVM>(HttpStatusCode.OK, updatedBranch, "Branch updated successfully");
        }
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteBranch(int id, int tenantId)
        {
            var isDeleted = _branchService.DeleteBranch(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Branch deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Branch not found or already deleted");



        }
    }
}