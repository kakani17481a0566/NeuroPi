using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseTeacher;
using SchoolManagement.ViewModel.ParentStudents;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParentStudentsController : ControllerBase
    {
        private readonly IParentStudentsService _service;

        public ParentStudentsController(IParentStudentsService service)
        {
            _service = service;
        }

        // -----------------------
        // CRUD: Parent ↔ Student link
        // -----------------------

        [HttpPost]
        public ResponseResult<ParentStudentResponseVM> Create([FromBody] ParentStudentRequestVM request)
        {
            var result = _service.Create(request);
            return new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Link created successfully");
        }

        [HttpGet]
        public ResponseResult<List<ParentStudentResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result, "Fetched all links successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<ParentStudentResponseVM>> GetAllByTenant(int tenantId)
        {
            var result = _service.GetAllByTenantId(tenantId);
            return new ResponseResult<List<ParentStudentResponseVM>>(HttpStatusCode.OK, result, "Fetched all links for tenant successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<ParentStudentResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Link not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Fetched link successfully");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> GetByIdAndTenant(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Link not found for tenant")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Fetched link successfully");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> Update(int id, int tenantId, [FromBody] ParentStudentUpdateVM request)
        {
            var result = _service.UpdateByIdAndTenantId(id, tenantId, request);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Update failed: link not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Updated link successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<ParentStudentResponseVM> Delete(int id, int tenantId)
        {
            var result = _service.DeleteByIdAndTenantId(id, tenantId);
            return result == null
                ? new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.NotFound, null, "Delete failed: link not found")
                : new ResponseResult<ParentStudentResponseVM>(HttpStatusCode.OK, result, "Deleted link successfully");
        }

        // -----------------------
        // Custom Queries
        // -----------------------

        /// <summary>
        /// Get basic parent details (branch + courses + term/week context).
        /// </summary>
        [HttpGet("user/{userId}/tenant/{tenantId}/courses")]
        public ResponseResult<CourseTeacherVM> GetParentCourses(int userId, int tenantId)
        {
            var result = _service.GetParentDetails(userId, tenantId);
            return result == null
                ? new ResponseResult<CourseTeacherVM>(HttpStatusCode.NotFound, null, "No courses found for this parent")
                : new ResponseResult<CourseTeacherVM>(HttpStatusCode.OK, result, "Fetched parent courses successfully");
        }

        /// <summary>
        /// Get parent with all linked students (full details).
        /// </summary>
        [HttpGet("user/{userId}/tenant/{tenantId}/full-details")]
        public ResponseResult<ParentWithStudentsResponseVM> GetParentWithStudents(int userId, int tenantId)
        {
            var result = _service.GetFullParentDetailsByUserId(userId, tenantId);
            return result == null
                ? new ResponseResult<ParentWithStudentsResponseVM>(HttpStatusCode.NotFound, null, "Parent not found for given user and tenant")
                : new ResponseResult<ParentWithStudentsResponseVM>(HttpStatusCode.OK, result, "Fetched parent and linked students successfully");
        }
    }
}
