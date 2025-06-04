using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Department;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;

        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }

        // GET api/departments
        [HttpGet]
        [Authorize]
        public ResponseResult<List<DepartmentResponseVM>> GetAll()
        {
            var data = _service.GetAllDepartments();
            return new ResponseResult<List<DepartmentResponseVM>>(
                data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                data,
                data.Count > 0 ? "Departments fetched" : "No departments found"
            );
        }

        // GET api/departments/{id}
        [HttpGet("{id}")]
        public ResponseResult<DepartmentResponseVM> GetById(int id)
        {
            var data = _service.GetDepartmentById(id);
            return new ResponseResult<DepartmentResponseVM>(
                data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                data,
                data != null ? "Department found" : "Not found"
            );
        }

        // GET api/departments/{id}/tenant/{tenantId}
        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<DepartmentResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var data = _service.GetDepartmentByIdAndTenantId(id, tenantId);
            return new ResponseResult<DepartmentResponseVM>(
                data != null ? HttpStatusCode.OK : HttpStatusCode.NotFound,
                data,
                data != null ? "Department found" : "Not found"
            );
        }

        // GET api/departments/tenant/{tenantId}
        [Authorize]

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<DepartmentResponseVM>> GetByTenantId(int tenantId)
        {
            var data = _service.GetDepartmentsByTenantId(tenantId);
            return new ResponseResult<List<DepartmentResponseVM>>(
                data.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NoContent,
                data,
                data.Count > 0 ? "Departments found" : "No departments found"
            );
        }

        // POST api/departments
        [HttpPost]
        [Authorize]

        public ResponseResult<DepartmentResponseVM> Add([FromBody] DepartmentCreateVM vm)
        {
            if (vm.CreatedBy <= 0)
                return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid CreatedBy");

            var result = _service.AddDepartment(vm);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                result,
                result != null ? "Department created" : "Creation failed"
            );
        }

        // PUT api/departments/{id}/tenant/{tenantId}
        [HttpPut("{id}/tenant/{tenantId}")]
        [Authorize]

        public ResponseResult<DepartmentResponseVM> Update(int id, int tenantId, [FromBody] DepartmentUpdateVM vm)
        {
            if (vm.UpdatedBy <= 0)
                return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid UpdatedBy");

            var result = _service.UpdateDepartment(id, tenantId, vm);
            return new ResponseResult<DepartmentResponseVM>(
                result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                result,
                result != null ? "Updated" : "Update failed"
            );
        }

        // DELETE api/departments/{id}/tenant/{tenantId}
        [HttpDelete("{id}/tenant/{tenantId}")]
        [Authorize]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var success = _service.DeleteById(id, tenantId, 0);
            return new ResponseResult<bool>(
                success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                success,
                success ? "Deleted" : "Delete failed"
            );
        }
    }
}
