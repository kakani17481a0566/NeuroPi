using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Department;
using System.Collections.Generic;

namespace NeuroPi.Controllers
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

        [HttpGet]
        public ResponseResult<List<DepartmentResponseVM>> GetAllDepartments()
        {
            var result = _departmentService.GetAllDepartments();
            if (result != null && result.Count > 0)
            {
                return new ResponseResult<List<DepartmentResponseVM>>(HttpStatusCode.OK, result, "Fetched successfully");
            }
            return new ResponseResult<List<DepartmentResponseVM>>(HttpStatusCode.NoContent, null, "No data found");
        }

        [HttpGet("id")]
        public ResponseResult<DepartmentResponseVM> GetById(int id)
        {
            var result = _departmentService.GetDepartmentById(id);
            if (result != null)
            {
                return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.OK, result, "Found the data");
            }
            return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.NotFound, null, $"No data found with id {id}");
        }

        [HttpDelete("id")]
        public ResponseResult<bool> DeleteDepartmentById(int id)
        {
            var deleted = _departmentService.DeleteById(id);
            if (deleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Department deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotImplemented, false, "Department not deleted");
        }

        [HttpPost]
        public ResponseResult<DepartmentResponseVM> AddDepartment([FromBody] DepartmentRequestVM requestVM)
        {
            var result = _departmentService.AddDepartment(requestVM);
            if (result != null)
            {
                return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.Created, result, "Department added successfully");
            }
            return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.NotAcceptable, null, "Department not added");
        }

        [HttpPut("id")]
        public ResponseResult<DepartmentResponseVM> UpdateDepartmentById(int id, [FromBody] DepartmentRequestVM request)
        {
            var result = _departmentService.UpdateDepartment(id, request);
            if (result != null)
            {
                return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.OK, result, "Department updated successfully");
            }
            return new ResponseResult<DepartmentResponseVM>(HttpStatusCode.NotAcceptable, null, "Department not updated");
        }
    }
}
