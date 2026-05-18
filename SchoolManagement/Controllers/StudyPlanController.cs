using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyPlan;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyPlanController : ControllerBase
    {
        private readonly IStudyPlanService _studyPlanService;

        public StudyPlanController(IStudyPlanService studyPlanService)
        {
            _studyPlanService = studyPlanService;
        }

        [HttpPost]
        public ResponseResult<StudyPlanVm> Create([FromBody] StudyPlanCreateVm vm)
        {
            var result = _studyPlanService.CreateStudyPlan(vm);
            return new ResponseResult<StudyPlanVm>(HttpStatusCode.Created, result, "Study plan created successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudyPlanVm>> GetAll(int tenantId)
        {
            var result = _studyPlanService.GetAllStudyPlans(tenantId);
            return new ResponseResult<List<StudyPlanVm>>(HttpStatusCode.OK, result, "Study plans fetched successfully");
        }

        [HttpGet("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyPlanVm> GetById(int id, int tenantId)
        {
            var result = _studyPlanService.GetStudyPlanById(id, tenantId);
            if (result == null)
                return new ResponseResult<StudyPlanVm>(HttpStatusCode.NotFound, null, "Study plan not found");
            return new ResponseResult<StudyPlanVm>(HttpStatusCode.OK, result, "Study plan fetched successfully");
        }

        [HttpPut("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyPlanVm> Update(int id, int tenantId, [FromBody] StudyPlanUpdateVm vm)
        {
            var result = _studyPlanService.UpdateStudyPlan(id, tenantId, vm);
            if (result == null)
                return new ResponseResult<StudyPlanVm>(HttpStatusCode.NotFound, null, "Study plan not found");
            return new ResponseResult<StudyPlanVm>(HttpStatusCode.OK, result, "Study plan updated successfully");
        }

        [HttpDelete("{id:int}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _studyPlanService.DeleteStudyPlan(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Study plan not found");
            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Study plan deleted successfully");
        }
    }
}
