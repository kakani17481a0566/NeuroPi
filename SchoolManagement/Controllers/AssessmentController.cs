using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Assessment;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;
        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet]
        public ResponseResult<List<AssessmentResponseVM>> GetAllAssessments()
        {
            var assessments = _assessmentService.GetAllAssessments();
            return new ResponseResult<List<AssessmentResponseVM>>(HttpStatusCode.OK, assessments, "All assessments retrieved successfully");

        }

        [HttpGet("{id}")]
        public ResponseResult<AssessmentResponseVM> GetAssessmentById(int id)
        {
            var assessment = _assessmentService.GetAssessmentById(id);
            return assessment == null
                ? new ResponseResult<AssessmentResponseVM>(HttpStatusCode.NotFound, null, "Assessment not found")
                : new ResponseResult<AssessmentResponseVM>(HttpStatusCode.OK, assessment, "Assessment retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<AssessmentResponseVM>> GetAssessmentsByTenantId(int tenantId)
        {
            var assessments = _assessmentService.GetAssessmentsByTenantId(tenantId);
            return assessments == null
                ? new ResponseResult<List<AssessmentResponseVM>>(HttpStatusCode.NotFound, null, "No assessments found for the specified tenant")
                : new ResponseResult<List<AssessmentResponseVM>>(HttpStatusCode.OK, assessments, "Assessments retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<AssessmentResponseVM> GetAssessmentByIdAndTenantId(int id, int tenantId)
        {
            var assessment = _assessmentService.GetAssessmentByIdAndTenantId(id, tenantId);
            return assessment == null
                ? new ResponseResult<AssessmentResponseVM>(HttpStatusCode.NotFound, null, "Assessment not found for the specified ID and Tenant ID")
                : new ResponseResult<AssessmentResponseVM>(HttpStatusCode.OK, assessment, "Assessment retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<AssessmentResponseVM> CreateAssessment(AssessmentRequestVM assessment)
        {
            var createdAssessment = _assessmentService.CreateAssessment(assessment);
            return new ResponseResult<AssessmentResponseVM>(HttpStatusCode.Created, createdAssessment, "Assessment created successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteAssessmentByIdAndTenantId(int id, int tenantId)
        {
            var isDeleted = _assessmentService.DeleteAssessmentByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Assessment deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Assessment not found or already deleted");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteAssessment(int id, int tenantId)
        {
            var isDeleted = _assessmentService.DeleteAssessmentByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Assessment deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Assessment not found or already deleted");

        }
    }
}
