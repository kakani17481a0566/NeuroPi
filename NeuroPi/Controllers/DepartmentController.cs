using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Department;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
            var result=_departmentService.GetAllDepartments();
            if (result != null)
            {
                return ResponseResult <List<DepartmentResponseVM>>.SuccessResponse(HttpStatusCode.OK, result,"Fetched Successfyully");
            }
            return ResponseResult<List<DepartmentResponseVM>>.FailResponse(HttpStatusCode.NoContent,"No data found");

        }
        [HttpGet("id")]
        public ResponseResult<DepartmentResponseVM> GetById(int id)
        {
            var result = _departmentService.GetDepartmentById(id);
            if (result != null)
            {
                return ResponseResult<DepartmentResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Found the data");
            }
            return ResponseResult<DepartmentResponseVM>.FailResponse(HttpStatusCode.NotFound, $"No data found with id {id}");
        }
        [HttpDelete("id")]
        public ResponseResult<bool> DeleteDepartmentById(int id)
        {
            var response = _departmentService.DeleteById(id);
            if (response)
            {
                return ResponseResult<bool>.SuccessResponse(HttpStatusCode.OK, response, "Department Deleted Successfully");
            }
            return ResponseResult<bool>.FailResponse(HttpStatusCode.NotImplemented, "No Content has deleted");
        }
        [HttpPost]
        public ResponseResult<DepartmentResponseVM> AddDepartment([FromBody] DepartmentRequestVM requestVM)
        {
            var result=_departmentService.AddDepartment(requestVM);
            if (result != null)
            {
                return ResponseResult<DepartmentResponseVM>.SuccessResponse(HttpStatusCode.Created, result, "Department added successfully");
            }
            return ResponseResult<DepartmentResponseVM>.FailResponse(HttpStatusCode.NotAcceptable, "Department not added ");
        }
        [HttpPut("id")]
        public ResponseResult<DepartmentResponseVM> UpdateDepartmentById(int id, DepartmentRequestVM request)
        {
            var result=_departmentService.UpdateDepartment(id, request);
            if (result != null) {

                return ResponseResult<DepartmentResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Department updated  successfully");
            }
            return ResponseResult<DepartmentResponseVM>.FailResponse(HttpStatusCode.NotAcceptable, "Department not updated ");

        }


    }
}
