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
    public class AssessmentSkillController : ControllerBase
    {
        private readonly IAssessmentSkillService _service;

        public AssessmentSkillController(IAssessmentSkillService service)
        {
            _service = service;
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<AssessmentSkillResponseVM>> GetAll(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<AssessmentSkillResponseVM>>(HttpStatusCode.OK, result, "Assessment skills fetched successfully");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<AssessmentSkillResponseVM> GetById(int id, int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            return result == null
                ? new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.NotFound, null, "Assessment skill not found")
                : new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.OK, result, "Assessment skill fetched successfully");
        }

        [HttpGet("subject/{subjectId}/tenant/{tenantId}")]
        public ResponseResult<List<AssessmentSkillResponseVM>> GetBySubjectId(int subjectId, int tenantId)
        {
            var result = _service.GetBySubjectId(subjectId, tenantId);
            return new ResponseResult<List<AssessmentSkillResponseVM>>(HttpStatusCode.OK, result, "Assessment skills fetched successfully");
        }

        [HttpPost]
        public ResponseResult<AssessmentSkillResponseVM> Create([FromBody] AssessmentSkillRequestVM request)
        {
            if (request == null)
                return new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data");

            var result = _service.Create(request);
            return new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.Created, result, "Assessment skill created successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<AssessmentSkillResponseVM> Update(int id, int tenantId, [FromBody] AssessmentSkillUpdateVM request)
        {
            if (request == null)
                return new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data");

            var result = _service.Update(id, tenantId, request);
            return result == null
                ? new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.NotFound, null, "Assessment skill not found")
                : new ResponseResult<AssessmentSkillResponseVM>(HttpStatusCode.OK, result, "Assessment skill updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var result = _service.Delete(id, tenantId);
            return result
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Assessment skill deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Assessment skill not found");
        }
    }
}
