using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.EmployeeStudyPlan;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeStudyPlanController : ControllerBase
    {
        private readonly IEmployeeStudyPlanService _employeeStudyPlanService;

        public EmployeeStudyPlanController(IEmployeeStudyPlanService employeeStudyPlanService)
        {
            _employeeStudyPlanService = employeeStudyPlanService;
        }

        [HttpPost]
        public ResponseResult<EmployeeStudyPlanVm> Create([FromBody] EmployeeStudyPlanCreateVm vm)
        {
            var result = _employeeStudyPlanService.CreateEmployeeStudyPlan(vm);
            return new ResponseResult<EmployeeStudyPlanVm>(HttpStatusCode.Created, result, "Employee study plan created successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<EmployeeStudyPlanVm>> GetAll(int tenantId)
        {
            var result = _employeeStudyPlanService.GetAllEmployeeStudyPlans(tenantId);
            return new ResponseResult<List<EmployeeStudyPlanVm>>(HttpStatusCode.OK, result, "Employee study plans fetched successfully");
        }

        [HttpGet("{employeeDetailsId:int}/tenant/{tenantId}")]
        public ResponseResult<List<EmployeeStudyPlanDetailVm>> GetByEmployeeId(int employeeDetailsId, int tenantId)
        {
            var result = _employeeStudyPlanService.GetEmployeeStudyPlansByEmployeeId(employeeDetailsId, tenantId);
            return new ResponseResult<List<EmployeeStudyPlanDetailVm>>(HttpStatusCode.OK, result, "Employee study plans fetched successfully");
        }

        [HttpGet("by-plan/{studyPlanId}/tenant/{tenantId}")]
        public ResponseResult<List<EmployeeStudyPlanVm>> GetByPlanId(int studyPlanId, int tenantId)
        {
            var result = _employeeStudyPlanService.GetEmployeeStudyPlansByPlanId(studyPlanId, tenantId);
            return new ResponseResult<List<EmployeeStudyPlanVm>>(HttpStatusCode.OK, result, "Employee study plans by plan fetched successfully");
        }

        [HttpDelete("{id:int}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _employeeStudyPlanService.DeleteEmployeeStudyPlan(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Employee study plan not found");
            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Employee study plan deleted successfully");
        }
    }
}
