using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyPlanSteps;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyPlanStepsController : ControllerBase
    {
        private readonly IStudyPlanStepsService _studyPlanStepsService;

        public StudyPlanStepsController(IStudyPlanStepsService studyPlanStepsService)
        {
            _studyPlanStepsService = studyPlanStepsService;
        }

        [HttpPost]
        public ResponseResult<StudyPlanStepsVm> Create([FromBody] StudyPlanStepsCreateVm vm)
        {
            var result = _studyPlanStepsService.CreateStudyPlanStep(vm);
            return new ResponseResult<StudyPlanStepsVm>(HttpStatusCode.Created, result, "Study plan step created successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudyPlanStepsVm>> GetAll(int tenantId)
        {
            var result = _studyPlanStepsService.GetAllStudyPlanSteps(tenantId);
            return new ResponseResult<List<StudyPlanStepsVm>>(HttpStatusCode.OK, result, "Study plan steps fetched successfully");
        }

        [HttpGet("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyPlanStepsVm> GetById(int id, int tenantId)
        {
            var result = _studyPlanStepsService.GetStudyPlanStepById(id, tenantId);
            if (result == null)
                return new ResponseResult<StudyPlanStepsVm>(HttpStatusCode.NotFound, null, "Study plan step not found");
            return new ResponseResult<StudyPlanStepsVm>(HttpStatusCode.OK, result, "Study plan step fetched successfully");
        }

        [HttpGet("by-plan/{studyPlanId}/tenant/{tenantId}")]
        public ResponseResult<List<StudyPlanStepsVm>> GetByPlanId(int studyPlanId, int tenantId)
        {
            var result = _studyPlanStepsService.GetStudyPlanStepsByPlanId(studyPlanId, tenantId);
            return new ResponseResult<List<StudyPlanStepsVm>>(HttpStatusCode.OK, result, "Study plan steps by plan fetched successfully");
        }

        [HttpPut("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyPlanStepsVm> Update(int id, int tenantId, [FromBody] StudyPlanStepsUpdateVm vm)
        {
            var result = _studyPlanStepsService.UpdateStudyPlanStep(id, tenantId, vm);
            if (result == null)
                return new ResponseResult<StudyPlanStepsVm>(HttpStatusCode.NotFound, null, "Study plan step not found");
            return new ResponseResult<StudyPlanStepsVm>(HttpStatusCode.OK, result, "Study plan step updated successfully");
        }

        [HttpDelete("{id:int}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _studyPlanStepsService.DeleteStudyPlanStep(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Study plan step not found");
            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Study plan step deleted successfully");
        }
    }
}
