using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeePackage;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeePackageController : ControllerBase
    {
        private readonly IFeePackage _feePackageService;

        public FeePackageController(IFeePackage feePackageService)
        {
            _feePackageService = feePackageService;
        }

        // 🔹 Helper: extract current user id from claims
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("UserId")?.Value;

            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        // 🔹 GET: api/FeePackage/{tenantId}/{branchId}
        [HttpGet("{tenantId:int}/{branchId:int}")]
        public ResponseResult<List<FeePackageResponseVM>> GetAll(int tenantId, int branchId)
        {
            var packages = _feePackageService.GetAll(tenantId, branchId);
            return new ResponseResult<List<FeePackageResponseVM>>(HttpStatusCode.OK, packages, "Fetched all fee packages");
        }

        // 🔹 GET: api/FeePackage/grouped/{tenantId}/{branchId}
        [HttpGet("grouped/{tenantId:int}/{branchId:int}")]
        public ResponseResult<List<FeePackageGroupVM>> GetGrouped(int tenantId, int branchId)
        {
            var grouped = _feePackageService.GetGroupedPackages(tenantId, branchId);
            return new ResponseResult<List<FeePackageGroupVM>>(HttpStatusCode.OK, grouped, "Fetched grouped fee packages");
        }

        // 🔹 GET: api/FeePackage/{id}/{tenantId}/{branchId}
        [HttpGet("{id:int}/{tenantId:int}/{branchId:int}")]
        public ResponseResult<FeePackageResponseVM> GetById(int id, int tenantId, int branchId)
        {
            var package = _feePackageService.GetById(id, tenantId, branchId);
            if (package == null)
                return new ResponseResult<FeePackageResponseVM>(HttpStatusCode.NotFound, null, "Fee package not found");

            return new ResponseResult<FeePackageResponseVM>(HttpStatusCode.OK, package, "Fee package retrieved");
        }

        // 🔹 POST: api/FeePackage
        [HttpPost]
        public ResponseResult<int> Create([FromBody] FeePackageRequestVM vm)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<int>(HttpStatusCode.BadRequest, 0, "Invalid data");

            var userId = GetCurrentUserId();
            var id = _feePackageService.Create(vm, userId);
            return new ResponseResult<int>(HttpStatusCode.Created, id, "Fee package created successfully");
        }

        // 🔹 PUT: api/FeePackage/{id}
        [HttpPut("{id:int}")]
        public ResponseResult<string> Update(int id, [FromBody] FeePackageRequestVM vm)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<string>(HttpStatusCode.BadRequest, null, "Invalid data");

            var userId = GetCurrentUserId();
            var updated = _feePackageService.Update(id, vm, userId);
            if (!updated)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Fee package not found or could not be updated");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Fee package updated successfully");
        }

        // 🔹 DELETE: api/FeePackage/{id}/{tenantId}/{branchId}
        [HttpDelete("{id:int}/{tenantId:int}/{branchId:int}")]
        public ResponseResult<string> Delete(int id, int tenantId, int branchId)
        {
            var userId = GetCurrentUserId();
            var deleted = _feePackageService.Delete(id, tenantId, branchId, userId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Fee package not found or already deleted");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Fee package deleted successfully");
        }
    }
}
