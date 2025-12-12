using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Subject;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        // Global endpoints for subjects
        // HTTP GET: api/subject
        [HttpGet]
        public ResponseResult<List<SubjectResponseVM>> GetAllSubjectsGlobal()
        {
            var subjects = _subjectService.GetAllSubjects();
            return new ResponseResult<List<SubjectResponseVM>>(HttpStatusCode.OK, subjects, "All global subjects fetched successfully");
        }

        // Get subject by ID globally
        // HTTP GET: api/subject/subject/{id}
        // Parameter: int id
        [HttpGet("subject/{id}")]
        public ResponseResult<SubjectResponseVM> GetSubjectByIdGlobal(int id)
        {
            var subject = _subjectService.GetSubjectById(id);
            if (subject == null)
            {
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found");
            }

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, subject, "Subject fetched successfully");
        }

        // Get all subjects by tenant ID
        // HTTP GET: api/subject/{tenantId}
        // Parameter: int tenantId
        [HttpGet("{tenantId}")]
        public ResponseResult<List<SubjectResponseVM>> GetAllSubjects(int tenantId)
        {
            var subjects = _subjectService.GetAllSubjects(tenantId);
            return new ResponseResult<List<SubjectResponseVM>>(HttpStatusCode.OK, subjects, "Tenant subjects fetched successfully");
        }

        // Get subject by ID and tenant ID
        // HTTP GET: api/subject/{tenantId}/{id}
        // Parameter: int tenantId, int id
        [HttpGet("{tenantId}/{id}")]
        public ResponseResult<SubjectResponseVM> GetSubjectById(int tenantId, int id)
        {
            var subject = _subjectService.GetSubjectById(id, tenantId);
            if (subject == null)
            {
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found");
            }

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, subject, "Subject fetched successfully");
        }

        // Creat a new subject
        // HTTP POST: api/subject
        // Parameter: SubjectRequestVM subject
        [HttpPost]
        public ResponseResult<SubjectResponseVM> CreateSubject([FromBody] SubjectRequestVM subject)
        {
            if (subject == null)
            {
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid subject data");
            }

            var created = _subjectService.CreateSubject(subject);
            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.Created, created, "Subject created successfully");
        }

        // Update an existing subject
        // HTTP PUT: api/subject/{tenantId}/{id}
        // Parameter: int tenantId, int id, SubjectUpdateVM subject
        [HttpPut("{tenantId}/{id}")]
        public ResponseResult<SubjectResponseVM> UpdateSubject(int tenantId, int id, [FromBody] SubjectUpdateVM subject)
        {
            if (subject == null)
            {
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid subject data");
            }

            var updated = _subjectService.UpdateSubject(id, tenantId, subject);
            if (updated == null)
            {
                return new ResponseResult<SubjectResponseVM>(HttpStatusCode.NotFound, null, "Subject not found or not updated");
            }

            return new ResponseResult<SubjectResponseVM>(HttpStatusCode.OK, updated, "Subject updated successfully");
        }

        // Delete a subject
        // HTTP DELETE: api/subject/{tenantId}/{id}
        // Parameter: int tenantId, int id
        [HttpDelete("{tenantId}/{id}")]
        public ResponseResult<string> DeleteSubject(int tenantId, int id)
        {
            var deleted = _subjectService.DeleteSubject(id, tenantId);
            if (!deleted)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Subject not found or already deleted");
            }

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Subject deleted successfully");
        }

        [HttpGet("subjectsByCourseId/{courseId}/{tenantId}")]
        public ResponseResult<List<SubjectResponseVM>> GetSubjectsByCourseId(int courseId, int tenantId)
        {
            var subjects = _subjectService.GetSubjectsByCourseIdAndTenantIt(courseId, tenantId);

            if (subjects == null || !subjects.Any())
            {
                return new ResponseResult<List<SubjectResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No subjects found for this course and tenant"
                );
            }

            return new ResponseResult<List<SubjectResponseVM>>(
                HttpStatusCode.OK,
                subjects,
                "Subjects fetched successfully"
            );
        }

        // Get subjects by course ID and tenant ID (alternative route pattern)
        // HTTP GET: api/subject/{courseId}/tenant/{tenantId}
        // Parameter: int courseId, int tenantId
        [HttpGet("{courseId}/tenant/{tenantId}")]
        public ResponseResult<List<SubjectResponseVM>> GetSubjectsByCourseAndTenant(int courseId, int tenantId)
        {
            var subjects = _subjectService.GetSubjectsByCourseIdAndTenantIt(courseId, tenantId);

            if (subjects == null || !subjects.Any())
            {
                return new ResponseResult<List<SubjectResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No subjects found for this course and tenant"
                );
            }

            return new ResponseResult<List<SubjectResponseVM>>(
                HttpStatusCode.OK,
                subjects,
                "Subjects fetched successfully"
            );
        }


    }
}
