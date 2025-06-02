using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Implementation;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Subject;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public IActionResult GetAllSubjectsGlobal()
        {
            var subjects = _subjectService.GetAllSubjects();
            return new ResponseResult<List<SubjectResponseVM>>(HttpStatusCode.OK, subjects);
        }

        [HttpGet("subject/{id}")]
        public IActionResult GetSubjectByIdGlobal(int id)
        {
            var subject = _subjectService.GetSubjectById(id);
            if (subject == null)
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found");

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, subject);
        }

        [HttpGet("{tenantId}")]
        public IActionResult GetAllSubjects(int tenantId)
        {
            var subjects = _subjectService.GetAllSubjects(tenantId);
            return new ResponseResult<List<SubjectResponseVM>>(HttpStatusCode.OK, subjects);
        }

        [HttpGet("{tenantId}/{id}")]
        public IActionResult GetSubjectById(int tenantId, int id)
        {
            var subject = _subjectService.GetSubjectById(id, tenantId);
            if (subject == null)
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found");

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, subject);
        }

        [HttpPost]
        public IActionResult CreateSubject([FromBody] SubjectRequestVM subject)
        {
            if (subject == null)
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid subject data");

            var created = _subjectService.CreateSubject(subject);
            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.Created, created, "Subject created successfully");
        }

        [HttpPut("{tenantId}/{id}")]
        public IActionResult UpdateSubject(int tenantId, int id, [FromBody] SubjectUpdateVM subject)
        {
            if (subject == null)
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid subject data");

            var updated = _subjectService.UpdateSubject(id, tenantId, subject);
            if (updated == null)
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found");

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, updated, "Subject updated successfully");
        }

        [HttpDelete("{tenantId}/{id}")]
        public IActionResult DeleteSubject(int tenantId, int id)
        {
            var deleted = _subjectService.DeleteSubject(id, tenantId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Subject not found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Subject deleted successfully");
        }
    }
}
