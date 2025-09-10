using System.Net;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("summary-structured")]
        public ResponseResult<StudentAttendanceStructuredSummaryVm> GetStructuredAttendanceSummary(
       [FromQuery] DateTime? date,
       [FromQuery] int tenantId,
       [FromQuery] int branchId,
       [FromQuery] int courseId
   )
        {
            if (!date.HasValue)
            {
                return new ResponseResult<StudentAttendanceStructuredSummaryVm>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Date is required."
                );
            }

            var dateOnly = DateOnly.FromDateTime(date.Value);

            var summary = _studentAttendanceService.GetAttendanceSummary(dateOnly, tenantId, branchId);

            // ✅ Apply course filter to records
            summary.Records = summary.Records
                .Where(r => courseId == -1 || r.CourseId == courseId)
                .ToList();

            return new ResponseResult<StudentAttendanceStructuredSummaryVm>(
                HttpStatusCode.OK,
                summary,
                "Attendance summary with course list retrieved successfully"
            );
        }




        [HttpPost("mark-attendance")]
        public IActionResult MarkAttendance([FromBody] SaveAttendanceRequestVm request)
        {
            var result = _studentAttendanceService.SaveAttendance(request);
            return Ok(new { success = result });
        }


        [HttpGet("graph")]
        public IActionResult GetStudentAttendanceGraph(int studentId, int tenantId, int? branchId, int days = 7)
        {
            var data = _studentAttendanceService.GetStudentAttendanceGraph(studentId, tenantId, branchId, days);
            return Ok(new
            {
                statusCode = 200,
                message = "Attendance graph data fetched successfully.",
                data = data
            });
        }

        [HttpGet("30daysGraph")]
        public IActionResult GetLast30DaysGraph(int studentId, int tenantId, int? branchId, string selectedDate)
        {
            var data = _studentAttendanceService.GetLast30DaysGraph(studentId, tenantId, branchId, selectedDate);
            return Ok(new {

                statusCode = 200,
                message = $"Last 30 days attendance ending on {selectedDate} fetched successfully.",
                data = data
            });

        }

        [HttpGet("GraphDateRange")]
        public IActionResult GetAttendanceDateRange(int studentId, int tenantId, int? branchId, string fromDatestr, string toDatestr)
        {
            var data =_studentAttendanceService.GetAttendanceDateRange(studentId, tenantId,branchId,fromDatestr,toDatestr);
            return Ok(new { 
                statusCode = 200,
                message = $"Graph Fetched successfully from date {fromDatestr} to date {toDatestr}",
                data = data
            });


        }


    }
}
