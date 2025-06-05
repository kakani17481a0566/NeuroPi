using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Student;

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

        [HttpGet("GetAllStudents")]
        public ResponseResult<List<StudentResponseVM>> GetAllStudents()
        {
            var students = _studentService.GetAllStudents();
            return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.OK, students, "All students retrieved successfully");
        }

        [HttpGet("GetStudentsByTenantId/{tenantId}")]
        public ResponseResult<List<StudentResponseVM>> GetStudentsByTenantId(int tenantId)
        {
            var students = _studentService.GetStudentsByTenantId(tenantId);
            return new ResponseResult<List<StudentResponseVM>>(HttpStatusCode.OK, students, "Students retrieved successfully for the specified tenant");
        }

        [HttpGet("GetStudentById/{id}")]
        public ResponseResult<StudentResponseVM> GetStudentById(int id)
        {
            var student = _studentService.GetStudentById(id);
            return student == null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Student not found")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, student, "Student retrieved successfully");
        }

        [HttpGet("GetStudentByTenantIdAndId/{tenantId}/{id}")]
        public ResponseResult<StudentResponseVM> GetStudentByTenantIdAndId(int tenantId, int id)
        {
            var student = _studentService.GetStudentByTenantIdAndId(tenantId, id);
            return student == null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Student not found for the specified tenant")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, student, "Student retrieved successfully");
        }

        [HttpPost("AddStudent")]
        public ResponseResult<StudentResponseVM> AddStudent([FromBody] StudentRequestVM studentRequestVM)
        {
            if (studentRequestVM == null)
            {
                return new ResponseResult<StudentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid student data");
            }
            var newStudent = _studentService.AddStudent(studentRequestVM);
            return new ResponseResult<StudentResponseVM>(HttpStatusCode.Created, newStudent, "Student added successfully");
        }

        [HttpPut("UpdateStudent/{id}/{tenantId}")]
        public ResponseResult<StudentResponseVM> UpdateStudent(int id, int tenantId, [FromBody] StudentUpdateVM updateVM)
        {
            if (updateVM == null)
            {
                return new ResponseResult<StudentResponseVM>(HttpStatusCode.BadRequest, null, "Invalid update data");
            }
            var updatedStudent = _studentService.UpdateStudent(id, tenantId, updateVM);
            return updatedStudent == null
                ? new ResponseResult<StudentResponseVM>(HttpStatusCode.NotFound, null, "Student not found for the specified ID and tenant")
                : new ResponseResult<StudentResponseVM>(HttpStatusCode.OK, updatedStudent, "Student updated successfully");
        }
        [HttpDelete("DeleteStudent/{id}/{tenantId}")]
        public ResponseResult<bool> DeleteStudent(int id, int tenantId)
        {
            var isDeleted = _studentService.DeleteStudent(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Student deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Student not found or already deleted");

        }
    }
}
