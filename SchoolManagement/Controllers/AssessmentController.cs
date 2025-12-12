using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Assessment;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _service;

        public AssessmentController(IAssessmentService service)
        {
            _service = service;
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<AssessmentResponseVM>> GetAll(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<AssessmentResponseVM>>(HttpStatusCode.OK, result, "Assessments fetched successfully");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<AssessmentResponseVM> GetById(int id, int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            return result == null
                ? new ResponseResult<AssessmentResponseVM>(HttpStatusCode.NotFound, null, "Assessment not found")
                : new ResponseResult<AssessmentResponseVM>(HttpStatusCode.OK, result, "Assessment fetched successfully");
        }

        [HttpGet("skill/{skillId}/tenant/{tenantId}")]
        public ResponseResult<List<AssessmentResponseVM>> GetBySkillId(int skillId, int tenantId)
        {
            var result = _service.GetBySkillId(skillId, tenantId);
            return new ResponseResult<List<AssessmentResponseVM>>(HttpStatusCode.OK, result, "Assessments fetched successfully");
        }

        [HttpPost]
        public ResponseResult<AssessmentResponseVM> Create([FromBody] AssessmentRequestVM request)
        {
            if (request == null)
                return new ResponseResult<AssessmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data");

            var result = _service.Create(request);
            return new ResponseResult<AssessmentResponseVM>(HttpStatusCode.Created, result, "Assessment created successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<AssessmentResponseVM> Update(int id, int tenantId, [FromBody] AssessmentUpdateVM request)
        {
            if (request == null)
                return new ResponseResult<AssessmentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data");

            var result = _service.Update(id, tenantId, request);
            return result == null
                ? new ResponseResult<AssessmentResponseVM>(HttpStatusCode.NotFound, null, "Assessment not found")
                : new ResponseResult<AssessmentResponseVM>(HttpStatusCode.OK, result, "Assessment updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var result = _service.Delete(id, tenantId);
            return result
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Assessment deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Assessment not found");
        }
    }
}
