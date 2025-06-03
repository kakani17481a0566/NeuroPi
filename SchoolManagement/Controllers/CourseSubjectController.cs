using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseSubject;
using System.Collections.Generic;
using System.Net;

// Devloped BY Mohith 

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSubjectController : ControllerBase
    {
        private readonly ICourseSubjectService _service;

        public CourseSubjectController(ICourseSubjectService service)
        {
            _service = service;
        }

        // Get By Tenant ID
        // HTTP GET: api/coursesubject/tenant/{tenantId}
        // Parameter: int tenantId
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<CourseSubjectResponseVM>> GetAll(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<CourseSubjectResponseVM>>(HttpStatusCode.OK, result, "Course subjects fetched successfully");
        }

        // Get By Tenant ID and Course Subject ID
        // HTTP GET: api/coursesubject/{id}/tenant/{tenantId}
        // Parameter: int id, int tenantId
        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<CourseSubjectResponseVM> GetById(int id, int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.NotFound, null, "CourseSubject not found");

            return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.OK, result, "CourseSubject fetched successfully");
        }

        // Get By Course Subject ID
        // HTTP GET: api/coursesubject/{id}
        // Parameter: int id
        [HttpGet("{id}")]
        public ResponseResult<CourseSubjectResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.NotFound, null, "CourseSubject not found");

            return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.OK, result, "CourseSubject fetched successfully");
        }

        // Create a new CourseSubject
        // HTTP POST: api/coursesubject
        // Parameter: CourseSubjectRequestVM request
        [HttpPost]
        public ResponseResult<CourseSubjectResponseVM> Create([FromBody] CourseSubjectRequestVM request)
        {
            if (request == null)
                return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request data");

            var created = _service.Create(request);
            return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.Created, created, "CourseSubject created successfully");
        }

        // Update an existing CourseSubject
        // HTTP PUT: api/coursesubject/{id}/tenant/{tenantId}
        // Parameter: int id, int tenantId, CourseSubjectUpdateVM request
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<CourseSubjectResponseVM> Update(int id, int tenantId, [FromBody] CourseSubjectUpdateVM request)
        {
            if (request == null)
                return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request data");

            var updated = _service.Update(id, tenantId, request);
            if (updated == null)
                return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.NotFound, null, "CourseSubject not found");

            return new ResponseResult<CourseSubjectResponseVM>(HttpStatusCode.OK, updated, "CourseSubject updated successfully");
        }

        // Delete a CourseSubject
        // HTTP DELETE: api/coursesubject/{id}/tenant/{tenantId}
        // Parameter: int id, int tenantId
        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var deleted = _service.Delete(id, tenantId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "CourseSubject not found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "CourseSubject deleted successfully");
        }
    }
}
