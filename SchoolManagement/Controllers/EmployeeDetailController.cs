using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EmployeeDetails;
using SchoolManagement.ViewModel.EmployeeProgress;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailController : ControllerBase
    {
        private readonly IEmployeeDetailService employeeDetailService;
        public EmployeeDetailController(IEmployeeDetailService _employeeDetailService)
        {
            employeeDetailService= _employeeDetailService;
        }

        [HttpGet("employeeDetails/{tenantId}")]
        public ResponseResult<List<EmployeeDetailsVM>> GetEmployees(int tenantId)
        {
            var result=employeeDetailService.GetAllEmployees(tenantId);
            if (result!=null) 
                return new ResponseResult<List<EmployeeDetailsVM>>(HttpStatusCode.OK, result,"Employee Details fetched Successfully");
            return new ResponseResult<List<EmployeeDetailsVM>>(HttpStatusCode.NotFound, result,"Employee Details Not Found");
        }

        [HttpPost]
        public ResponseResult<EmployeeDetailsVM> CreateEmployeeDetail([FromBody] EmployeeDetailRequestVM request)
        {
            var result = employeeDetailService.CreateEmployeeDetail(request);
            if (result != null)
                return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.OK, result, "Employee Detail created successfully");
            return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.BadRequest, result, "Failed to create Employee Detail. Contact not found or invalid tenant.");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<EmployeeDetailsVM> UpdateEmployeeDetail(int id, int tenantId, [FromBody] EmployeeDetailUpdateVM request)
        {
            var result = employeeDetailService.UpdateEmployeeDetail(id, tenantId, request);
            if (result != null)
                return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.OK, result, "Employee Detail updated successfully");
            return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.NotFound, result, "Employee Detail not found or contact validation failed.");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<EmployeeDetailsVM> DeleteEmployeeDetail(int id, int tenantId)
        {
            var result = employeeDetailService.DeleteEmployeeDetail(id, tenantId);
            if (result != null)
                return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.OK, result, "Employee Detail deleted successfully");
            return new ResponseResult<EmployeeDetailsVM>(HttpStatusCode.NotFound, result, "Employee Detail not found.");
        }

        [HttpGet("employee-progress/{id}/tenant/{tenantId}")]
        public ResponseResult<EmployeeProgressVm> GetEmployeeProgress(int id, int tenantId)
        {
            var result = employeeDetailService.GetEmployeeProgress(id, tenantId);
            if (result == null)
                return new ResponseResult<EmployeeProgressVm>(HttpStatusCode.NotFound, null, "Employee progress not found");
            return new ResponseResult<EmployeeProgressVm>(HttpStatusCode.OK, result, "Employee progress fetched successfully");
        }
    }
}
