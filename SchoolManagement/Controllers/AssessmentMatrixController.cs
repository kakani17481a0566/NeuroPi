using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.DailyAssessment;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentMatrixController : ControllerBase
    {
        private readonly IAssessmentMatrixService _assessmentMatrixService;

        public AssessmentMatrixController(IAssessmentMatrixService assessmentMatrixService)
        {
            _assessmentMatrixService = assessmentMatrixService;
        }

        [HttpGet("timetable/{timeTableId}/tenant/{tenantId}/course/{courseId}/branch/{branchId}")]
        public IActionResult GetAssessmentMatrix(int timeTableId, int tenantId, int courseId, int branchId)
        {
            var result = _assessmentMatrixService.GetMatrix(timeTableId, tenantId, courseId, branchId);
            return new ResponseResult<AssessmentMatrixResponse>(
                HttpStatusCode.OK,
                result,
                "Assessment matrix fetched successfully"
            );
        }
    }
}
