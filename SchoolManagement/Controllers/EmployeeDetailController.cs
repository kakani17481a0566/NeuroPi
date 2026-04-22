using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EmployeeDetails;
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

        [HttpGet("/employeeDetails/{tenantId}")]
        public ResponseResult<List<EmployeeDetailsVM>> GetEmployees(int tenantId)
        {
            var result=employeeDetailService.GetAllEmployees(tenantId);
            if (result!=null) 
                return new ResponseResult<List<EmployeeDetailsVM>>(HttpStatusCode.OK, result,"Employee Details fetched Successfully");
            return new ResponseResult<List<EmployeeDetailsVM>>(HttpStatusCode.NotFound, result,"Employee Details Not Found");
        }
    }
}
