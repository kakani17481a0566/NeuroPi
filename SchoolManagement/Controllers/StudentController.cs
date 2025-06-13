// StudentController.cs
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;

using SchoolManagement.ViewModel.Students;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET: api/student
        [HttpGet]
        public ResponseResult<List<StudentResponseVM>> GetAll()
        {
            var result = _studentService.GetAll();
            return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        // GET: api/student/{id}
        [HttpGet("{id}")]
        public ResponseResult<StudentResponseVM> GetById(int id)
        {
            var result = _studentService.GetById(id);
            return result != null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, result, "Fetched successfully")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Not found");
        }

        // GET: api/student/tenant/{tenantId}/branch/{branchId}
        [HttpGet("tenant/{tenantId}/branch/{branchId}")]
        public ResponseResult<List<StudentResponseVM>> GetByTenantAndBranch(int tenantId, int branchId)
        {
            var result = _studentService.GetByTenantAndBranch(tenantId, branchId);
            return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.OK, result, "Fetched successfully");
        }

        // POST: api/student
        [HttpPost]
        public ResponseResult<StudentResponseVM> Create([FromBody] StudentRequestVM request)
        {
            var result = _studentService.Create(request);
            return new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, result, "Created successfully");
        }

        // PUT: api/student/{id}
        [HttpPut("{id}")]
        public ResponseResult<StudentResponseVM> Update(int id, [FromBody] StudentRequestVM request)
        {
            var result = _studentService.Update(id, request);
            return result != null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, result, "Updated successfully")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Not found");
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}")]
        public ResponseResult<StudentResponseVM> Delete(int id)
        {
            var result = _studentService.Delete(id);
            return result != null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, result, "Deleted successfully")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Not found");
        }

        [HttpGet("by-tenant-course-branch")]
        public ResponseResult<List<StudentResponseVM>> GetByTenantCourseBranch([FromQuery] int tenantId, [FromQuery] int courseId, [FromQuery] int branchId)
        {
            var data = _studentService.GetByTenantCourseBranch(tenantId, courseId, branchId);
            if (data == null || data.Count == 0)
                return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.NotFound, data, "No students found for given filters.");

            return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.OK, data, "Filtered students fetched successfully.");
        }


    }
}
