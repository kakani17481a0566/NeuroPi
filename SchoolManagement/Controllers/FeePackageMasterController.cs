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
    public class FeePackageMasterController : ControllerBase
    {
        private readonly IFeePackageMaster _service;

        public FeePackageMasterController(IFeePackageMaster service)
        {
            _service = service;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                              ?? User.FindFirst("UserId")?.Value;
            return int.TryParse(userIdClaim, out var userId) ? userId : 0;
        }

        [HttpGet("{tenantId:int}/{branchId:int}")]
        public ResponseResult<List<FeePackageMasterResponseVM>> GetAll(int tenantId, int branchId)
        {
            var data = _service.GetAll(tenantId, branchId);
            return new ResponseResult<List<FeePackageMasterResponseVM>>(HttpStatusCode.OK, data, "Fetched all package masters");
        }

        [HttpGet("{id:int}/{tenantId:int}/{branchId:int}")]
        public ResponseResult<FeePackageMasterResponseVM> GetById(int id, int tenantId, int branchId)
        {
            var item = _service.GetById(id, tenantId, branchId);
            if (item == null)
                return new ResponseResult<FeePackageMasterResponseVM>(HttpStatusCode.NotFound, null, "Package master not found");

            return new ResponseResult<FeePackageMasterResponseVM>(HttpStatusCode.OK, item, "Package master retrieved");
        }

        [HttpPost]
        public ResponseResult<int> Create([FromBody] FeePackageMasterRequestVM vm)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<int>(HttpStatusCode.BadRequest, 0, "Invalid data");

            var id = _service.Create(vm, GetCurrentUserId());
            return new ResponseResult<int>(HttpStatusCode.Created, id, "Package master created successfully");
        }

        [HttpPut("{id:int}")]
        public ResponseResult<string> Update(int id, [FromBody] FeePackageMasterRequestVM vm)
        {
            if (!ModelState.IsValid)
                return new ResponseResult<string>(HttpStatusCode.BadRequest, null, "Invalid data");

            var updated = _service.Update(id, vm, GetCurrentUserId());
            if (!updated)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Package master not found or could not be updated");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Package master updated successfully");
        }

        [HttpDelete("{id:int}/{tenantId:int}/{branchId:int}")]
        public ResponseResult<string> Delete(int id, int tenantId, int branchId)
        {
            var deleted = _service.Delete(id, tenantId, branchId, GetCurrentUserId());
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Package master not found or already deleted");

            return new ResponseResult<string>(HttpStatusCode.OK, "OK", "Package master deleted successfully");
        }
    }
}
