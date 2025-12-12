using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableAssessment;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableAssessmentController : ControllerBase
    {
        private readonly ITimeTableAssessmentService _timeTableAssessmentService;
        public TimeTableAssessmentController(ITimeTableAssessmentService timeTableAssessmentService)
        {
            _timeTableAssessmentService = timeTableAssessmentService;
        }

        [HttpGet("GetTimeTableAssessments")]
        public ResponseResult<List<TimeTableAssessmentResponseVM>> GetTimeTableAssessments()
        {
            var assessments = _timeTableAssessmentService.GetAllTimeTableAssessments();
            return new ResponseResult<List<TimeTableAssessmentResponseVM>>(HttpStatusCode.OK, assessments, "Time table assessments retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TimeTableAssessmentResponseVM>> GetTimeTableAssessmentsByTenantId(int tenantId)
        {
            var assessments = _timeTableAssessmentService.GetTimeTableAssessmentsByTenantId(tenantId);
            return new ResponseResult<List<TimeTableAssessmentResponseVM>>(HttpStatusCode.OK, assessments, "Time table assessments for tenant retrieved successfully");
        }

        [HttpGet("timetable/{timeTableId}/tenant/{tenantId}")]
        public ResponseResult<List<TimeTableAssessmentResponseVM>> GetTimeTableAssessmentsByTimeTableId(int timeTableId, int tenantId)
        {
            var assessments = _timeTableAssessmentService.GetTimeTableAssessmentsByTimeTableId(timeTableId, tenantId);
            return new ResponseResult<List<TimeTableAssessmentResponseVM>>(HttpStatusCode.OK, assessments, "Time table assessments for timetable retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<TimeTableAssessmentResponseVM> GetTimeTableAssessmentById(int id)
        {
            var assessment = _timeTableAssessmentService.GetTimeTableAssessmentById(id);
            return assessment == null
                ? new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.NotFound, null, "Time table assessment not found")
                : new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.OK, assessment, "Time table assessment retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}/{id}")]
        public ResponseResult<TimeTableAssessmentResponseVM> GetTimeTableAssessmentByTenantIdAndId(int tenantId, int id)
        {
            var assessment = _timeTableAssessmentService.GetTimeTableAssessmentByTenantIdAndId(tenantId, id);
            return assessment == null
                ? new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.NotFound, null, "Time table assessment not found for the specified tenant")
                : new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.OK, assessment, "Time table assessment retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<TimeTableAssessmentResponseVM> GetTimeTableAssessmentByIdAndTenantId(int id, int tenantId)
        {
            var assessment = _timeTableAssessmentService.GetTimeTableAssessmentByTenantIdAndId(id, tenantId);
            return assessment == null
                ? new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.NotFound, null, "Time table assessment not found for the specified tenant")
                : new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.OK, assessment, "Time table assessment retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<TimeTableAssessmentResponseVM> AddTimeTableAssessment([FromBody] TimeTableAssessmentRequestVM timeTableAssessment)
        {
            if (timeTableAssessment == null)
            {
                return new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid time table assessment data");
            }
            var assessment = _timeTableAssessmentService.AddTimeTableAssessment(timeTableAssessment);
            return new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.Created, assessment, "Time table assessment added successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<TimeTableAssessmentResponseVM> UpdateTimeTableAssessment(int id, int tenantId, [FromBody] TimeTableAssessmentUpdateVM timeTableAssessment)
        {
            if (timeTableAssessment == null)
            {
                return new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid time table assessment data");
            }
            var assessment = _timeTableAssessmentService.UpdateTimeTableAssessment(id, tenantId, timeTableAssessment);
            return assessment == null
                ? new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.NotFound, null, "Time table assessment not found for the specified ID and tenant")
                : new ResponseResult<TimeTableAssessmentResponseVM>(HttpStatusCode.OK, assessment, "Time table assessment updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteTimeTableAssessment(int id, int tenantId)
        {
            var isDeleted = _timeTableAssessmentService.DeleteTimeTableAssessment(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Time table assessment deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Time table assessment not found for the specified ID and tenant");
        }
    }
}