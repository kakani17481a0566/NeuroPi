// StudentController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Student;
using SchoolManagement.ViewModel.Students;
using SchoolManagement.ViewModel.Subject;
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


        [Authorize]
        [HttpGet("by-tenant-course-branch")]
        public ResponseResult<StudentVM> GetByTenantCourseBranch([FromQuery] int tenantId, [FromQuery] int courseId, [FromQuery] int branchId)
        {
            var data = _studentService.GetByTenantCourseBranch(tenantId, courseId, branchId);
            if (data == null )
                return new ResponseResult<StudentVM>(HttpStatusCode.NotFound, data, "No students found for given filters.");

            return new ResponseResult<StudentVM>(HttpStatusCode.OK, data, "Filtered students fetched successfully.");
        }

        [HttpGet("students/{courseId}/branch/{branchId}")]
        public ResponseResult< StudentsData> GetStudentDetails(int courseId,int branchId, DateTime dateTime, int tenantId)
        {
            var date = DateOnly.FromDateTime(dateTime);
            var response =_studentService.GetStudentDetails(courseId,branchId,date, tenantId);
            if (response == null)
            {
                return new ResponseResult<StudentsData>(HttpStatusCode.NotFound, response, " No Student Details Fetched SuccessFully");
            }
            return new ResponseResult<StudentsData>(HttpStatusCode.OK, response, "Student Details Fetched SuccessFully");

        }

        [HttpGet("performance")]
        public ResponseResult<List<VStudentPerformanceVM>> GetStudentPerformance([FromQuery] int tenantId, [FromQuery] int courseId, [FromQuery] int branchId)
        {
            var data = _studentService.GetStudentPerformance(tenantId, courseId, branchId);
            if (data == null || !data.Any())
                return new ResponseResult<List<VStudentPerformanceVM>>(HttpStatusCode.NotFound, null, "No performance data found.");

            return new ResponseResult<List<VStudentPerformanceVM>>(HttpStatusCode.OK, data, "Student performance data fetched successfully.");
        }


        [HttpGet("chart-performance")]
        public ResponseResult<VStudentPerformanceChartVM> GetStudentChartData([FromQuery] int tenantId, [FromQuery] int courseId, [FromQuery] int branchId)
        {
            var result = _studentService.GetStudentPerformanceChartData(tenantId, courseId, branchId);
            if (result == null || !result.Headers.Any())
                return new ResponseResult<VStudentPerformanceChartVM>(HttpStatusCode.NotFound, null, "No chart data found");

            return new ResponseResult<VStudentPerformanceChartVM>(HttpStatusCode.OK, result, "Chart data fetched successfully");
        }


        // GET: api/student/dropdown-options?tenantId=1&courseId=1&branchId=1
        [HttpGet("dropdown-options-students")]
        public ResponseResult<List<StudentCourseTenantVm>> GetStudentDropDownOptions(
            [FromQuery] int tenantId,
            [FromQuery] int courseId,
            [FromQuery] int branchId)
        {
            var data = _studentService.GetStudentDropDownOptions(tenantId, courseId, branchId) ?? new List<StudentCourseTenantVm>();
            return new ResponseResult<List<StudentCourseTenantVm>>(
                HttpStatusCode.OK,
                data,
                "Student dropdown options retrieved successfully"
            );
        }

        // POST: api/student/register
        [HttpPost("register")]
        public ResponseResult<SRStudentRegistrationResponseVM> Register([FromBody] SRStudentRegistrationRequestVM request)
        {
            try
            {
                var result = _studentService.Register(request);

                return new ResponseResult<SRStudentRegistrationResponseVM>(
                    HttpStatusCode.OK,
                    result,
                    "Student registered successfully."
                );
            }
            catch (InvalidOperationException ex) // thrown when duplicate detected
            {
                return new ResponseResult<SRStudentRegistrationResponseVM>(
                    HttpStatusCode.Conflict,
                    null,
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<SRStudentRegistrationResponseVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    $"Registration failed: {ex.Message}"
                );
            }
        }

        [HttpGet("students-by-tenant-course-branch")]
        public ResponseResult<List<StudentListVM>> GetStudentsByTenantCourseBranch(
    [FromQuery] int tenantId,
    [FromQuery] int courseId,
    [FromQuery] int branchId)
        {
            var result = _studentService.GetStudentsByTenantCourseBranch(tenantId, courseId, branchId);

            if (result == null || !result.Any())
            {
                return new ResponseResult<List<StudentListVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No students found for the given tenant, course, and branch."
                );
            }

            return new ResponseResult<List<StudentListVM>>(
                HttpStatusCode.OK,
                result,
                "Students fetched successfully."
            );
        }
        [HttpGet("search/{name}")]
        public ResponseResult<List<StudentFilterResponseVM>> GetAllStudentByNames(string name, [FromQuery] DateOnly? dob = null)
        {
            var result = _studentService.GetAllStudentsByName(name, dob);
            if(result!=null && result.Count > 0)
            {
                return new ResponseResult<List<StudentFilterResponseVM>>(HttpStatusCode.OK, result, "Students Fetched Successfully");
            }
            return new ResponseResult<List<StudentFilterResponseVM>>(HttpStatusCode.NotFound,result);

        }

        [HttpGet("by-parent-user/{userId}")]
        public ResponseResult<List<StudentFilterResponseVM>> GetStudentsByParentUserId(int userId)
        {
            var result = _studentService.GetStudentsByParentUserId(userId);
            if (result != null && result.Count > 0)
            {
                return new ResponseResult<List<StudentFilterResponseVM>>(HttpStatusCode.OK, result, "Students Fetched Successfully");
            }
            return new ResponseResult<List<StudentFilterResponseVM>>(HttpStatusCode.OK, new List<StudentFilterResponseVM>(), "No linked students found.");
        }
    }
 

}
