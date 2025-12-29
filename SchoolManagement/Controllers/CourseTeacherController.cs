using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using SchoolManagement.ViewModel.CourseTeacher;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseTeacherController : ControllerBase
    {
        private readonly ICourseTeacherService _courseTeacherService;

        public CourseTeacherController(ICourseTeacherService courseTeacherService)
        {
            _courseTeacherService = courseTeacherService;
        }

        // GET api/courseteacher/teacher/{teacherId}?tenantId=1
        [HttpGet("teacher/{teacherId}")]
        public ResponseResult<List<CourseTeacherVM>> GetCoursesByTeacher(int teacherId, [FromQuery] int tenantId)
        {
            var courses = _courseTeacherService.GetCoursesByTeacherId(teacherId, tenantId);

            if (courses != null && courses.Count > 0)
                return new ResponseResult<List<CourseTeacherVM>>(HttpStatusCode.OK, courses, "Courses fetched successfully");

            return new ResponseResult<List<CourseTeacherVM>>(HttpStatusCode.NotFound, null, "No courses found for this teacher");
        }

        // POST api/courseteacher
        [HttpPost]
        public ResponseResult<CourseTeacherVM> AssignCourse([FromBody] AssignCourseTeacherVM model)
        {
            try
            {
                var assignment = _courseTeacherService.AssignCourseToTeacher(model);

                if (assignment != null)
                    return new ResponseResult<CourseTeacherVM>(HttpStatusCode.Created, assignment, "Course assigned successfully");

                return new ResponseResult<CourseTeacherVM>(HttpStatusCode.BadRequest, null, "Failed to assign course");
            }
            catch (Exception ex)
            {
                return new ResponseResult<CourseTeacherVM>(HttpStatusCode.BadRequest, null, ex.Message);
            }
        }

        // DELETE api/courseteacher/{id}?tenantId=1
        [HttpDelete("{id}")]
        public ResponseResult<object> RemoveCourse(int id, [FromQuery] int tenantId)
        {
            var success = _courseTeacherService.RemoveCourseFromTeacher(id, tenantId);

            if (success)
                return new ResponseResult<object>(HttpStatusCode.OK, null, "Course assignment removed successfully");

            return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Course assignment not found");
        }
    }
}
