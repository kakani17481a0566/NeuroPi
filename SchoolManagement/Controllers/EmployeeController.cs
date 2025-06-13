using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Employee;
using SchoolManagement.ViewModel.Master;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public ResponseResult<List<EmployeeResponseVM>> GetAllMasterTypes()
        {
            var response = _employeeService.GetAll();
            if (response == null)
            {
                return new ResponseResult<List<EmployeeResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<EmployeeResponseVM>>(HttpStatusCode.OK, response, "Employees fetched successfully");
        }

        [HttpGet("employee/tenant/{id}")]
        public ResponseResult<List<EmployeeResponseVM>> GetAllEmployees([FromRoute] int id)
        {
            var response = _employeeService.GetAllByTenantId(id);

            if (response == null)
            {
                return new ResponseResult<List<EmployeeResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<EmployeeResponseVM>>(HttpStatusCode.OK, response, "Employees fetched successfully");
        }
        [HttpPost]
        public ResponseResult<EmployeeResponseVM> CreateEmployee([FromBody] EmployeeRequestVM employeeRequest)
        {
            var response = _employeeService.CreateEmployee(employeeRequest);

            if (response == null)
            {
                return new ResponseResult<EmployeeResponseVM>(HttpStatusCode.BadGateway, response, "No data Found");
            }
            return new ResponseResult<EmployeeResponseVM>(HttpStatusCode.OK, response, "Employee created  successfully");
        }



    }
}
