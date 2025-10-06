using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeePackage;
using System.Collections.Generic;
using System.Net;

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

        // 🔹 GET: api/FeePackage/{tenantId}/{branchId}
        [HttpGet("{tenantId:int}/{branchId:int}")]
        public ResponseResult<List<FeePackageResponseVM>> GetAll(int tenantId, int branchId)
        {
            var packages = _feePackageService.GetAll(tenantId, branchId);
            return new ResponseResult<List<FeePackageResponseVM>>(HttpStatusCode.OK, packages, "Fetched all fee packages");
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

            var id = _feePackageService.Create(vm);
            return new ResponseResult<int>(HttpStatusCode.Created, id, "Fee package created successfully");
        }

        // 🔹 PUT: api/FeePackage/{id}
        [HttpPut("{id:int}")]
        public ResponseResult<string> Update(int id, [FromBody] FeePackageRequestVM vm)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<string>(HttpStatusCode.BadRequest, null, "Invalid data");

            var updated = _feePackageService.Update(id, vm);
            if (!updated)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Fee package not found or could not be updated");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Fee package updated successfully");
        }

        // 🔹 DELETE: api/FeePackage/{id}/{tenantId}/{branchId}
        [HttpDelete("{id:int}/{tenantId:int}/{branchId:int}")]
        public ResponseResult<string> Delete(int id, int tenantId, int branchId)
        {
            var deleted = _feePackageService.Delete(id, tenantId, branchId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Fee package not found or already deleted");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Fee package deleted successfully");
        }
    }
}
