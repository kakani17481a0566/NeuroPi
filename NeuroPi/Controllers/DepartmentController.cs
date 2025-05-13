using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Department;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("user_id");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            var result = _departmentService.GetAllDepartments();
            return new ResponseResult<List<DepartmentResponseVM>>(
                result.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                result,
                result.Count > 0 ? "Fetched successfully" : "No data found"
            );
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = _departmentService.GetDepartmentById(id);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                result,
                result != null ? "Found the data" : $"No data found with id {id}"
            );
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public IActionResult GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _departmentService.GetDepartmentByIdAndTenantId(id, tenantId);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                result,
                result != null ? "Found the department" : $"No department found with id {id} and tenantId {tenantId}"
            );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDepartmentById(int id)
        {
            var deleted = _departmentService.DeleteById(id, GetCurrentUserId());
            return new ResponseResult<bool>(
                deleted ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                deleted,
                deleted ? "Department deleted successfully" : "Department not deleted"
            );
        }

        [HttpPost]
        public IActionResult AddDepartment([FromBody] DepartmentCreateVM requestVM)
        {
            requestVM.CreatedBy = GetCurrentUserId();
            var result = _departmentService.AddDepartment(requestVM);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                result,
                result != null ? "Department added successfully" : "Department not added"
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDepartmentById(int id, [FromBody] DepartmentUpdateVM request)
        {
            request.UpdatedBy = GetCurrentUserId();
            var result = _departmentService.UpdateDepartment(id, request);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                result,
                result != null ? "Department updated successfully" : "Department not updated"
            );
        }

        [HttpGet("tenant/{tenantId}")]
        public IActionResult GetByTenantId(int tenantId)
        {
            var result = _departmentService.GetDepartmentsByTenantId(tenantId);
            return new ResponseResult<List<DepartmentResponseVM>>(
                result.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                result,
                result.Count > 0 ? "Departments fetched by tenant ID" : $"No departments found for tenant ID {tenantId}"
            );
        }
    }
}
