using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentAttendance;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly IStudentAttendanceService _studentAttendanceService;
        public StudentAttendanceController(IStudentAttendanceService studentAttendanceService)
        {
            _studentAttendanceService = studentAttendanceService;
        }

        [HttpGet]
        public ResponseResult<List<StudentAttendanceResponseVm>> GetStudentAttendanceList()
        {
            var attendances = _studentAttendanceService.GetStudentAttendanceList();
            return new ResponseResult<List<StudentAttendanceResponseVm>>(HttpStatusCode.OK, attendances, "All student attendances retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<StudentAttendanceResponseVm> GetStudentAttendanceById(int id)
        {
            var attendance = _studentAttendanceService.GetStudentAttendanceById(id);
            return attendance == null
                ? new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.NotFound, null, "Student attendance not found")
                : new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.OK, attendance, "Student attendance retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudentAttendanceResponseVm>> GetStudentAttendanceByTenantId(int tenantId)
        {
            var attendances = _studentAttendanceService.GetStudentAttendanceByTenantId(tenantId);
            return attendances == null || !attendances.Any()
                ? new ResponseResult<List<StudentAttendanceResponseVm>>(HttpStatusCode.NotFound, null, "No student attendance found for the specified tenant")
                : new ResponseResult<List<StudentAttendanceResponseVm>>(HttpStatusCode.OK, attendances, "Student attendances retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<StudentAttendanceResponseVm> GetStudentAttendanceListByIdAndTenantId(int id, int tenantId)
        {
            var attendance = _studentAttendanceService.GetStudentAttendanceListByIdAndTenantId(id, tenantId);
            return attendance == null
                ? new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.NotFound, null, "Student attendance not found for the specified ID and tenant")
                : new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.OK, attendance, "Student attendance retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<StudentAttendanceResponseVm> AddStudentAttendance(StudentAttendanceRequestVM studentAttendanceRequestVm)
        {
            if (studentAttendanceRequestVm == null)
            {
                return new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.BadRequest, null, "Invalid request data");
            }
            var newAttendance = _studentAttendanceService.AddStudentAttendance(studentAttendanceRequestVm);
            return new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.Created, newAttendance, "Student attendance added successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<StudentAttendanceResponseVm> UpdateStudentAttendance(int id, int tenantId, StudentAttendanceUpdateVM studentAttendanceUpdateVm)
        {
            if (studentAttendanceUpdateVm == null)
            {
                return new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.BadRequest, null, "Invalid request data");
            }
            var updatedAttendance = _studentAttendanceService.UpdateStudentAttendance(id, tenantId, studentAttendanceUpdateVm);
            return updatedAttendance == null
                ? new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.NotFound, null, "Student attendance not found for the specified ID and tenant")
                : new ResponseResult<StudentAttendanceResponseVm>(HttpStatusCode.OK, updatedAttendance, "Student attendance updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteStudentAttendance(int id, int tenantId)
        {
            var isDeleted = _studentAttendanceService.DeleteStudentAttendance(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Student attendance deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Student attendance not found for the specified ID and tenant");

        }


        [HttpGet("summary-structured")]
        public IActionResult GetStructuredAttendanceSummary(
            [FromQuery] DateTime? date,
            [FromQuery] int tenantId,
            [FromQuery] int branchId,
            [FromQuery] int courseId // ✅ id as courseId
        )
        {
            if (!date.HasValue)
                return BadRequest("Date is required and must be in yyyy-MM-dd format.");

            var records = _studentAttendanceService
                .GetAttendanceSummary(date.Value, tenantId, branchId)
                .Where(r => r.CourseId == courseId) // ✅ filter by courseId
                .ToList();

            var headers = typeof(StudentAttendanceSummaryVm)
                .GetProperties()
                .Select(p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1))
                .ToList();

            var response = new
            {
                id = courseId, // ✅ direct from query
                headers,
                data = records,
            };

            return Ok(response);
        }


        [HttpPost("mark-attendance")]
        public IActionResult MarkAttendance([FromBody] SaveAttendanceRequestVm request)
        {
            var result = _studentAttendanceService.SaveAttendance(request);
            return Ok(new { success = result });
        }




    }
}
