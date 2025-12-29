using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using NeuroPi.UserManagment.Response;
using SchoolManagement.ViewModel.Course;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // GET api/course/by-tenant?tenantId=1
        [HttpGet("by-tenant")]
        public ResponseResult<List<CourseVM>> GetCoursesByTenant([FromQuery] int tenantId)
        {
            var courses = _courseService.GetCoursesByTenantId(tenantId);

            if (courses != null && courses.Count > 0)
                return new ResponseResult<List<CourseVM>>(HttpStatusCode.OK, courses, "Courses fetched successfully");

            return new ResponseResult<List<CourseVM>>(HttpStatusCode.NotFound, null, "No courses found");
        }

        // GET api/course/{id}?tenantId=1
        [HttpGet("{id}")]
        public ResponseResult<CourseVM> GetCourseById(int id, [FromQuery] int tenantId)
        {
            var course = _courseService.GetCourseById(id, tenantId);

            if (course != null)
                return new ResponseResult<CourseVM>(HttpStatusCode.OK, course, "Course fetched successfully");

            return new ResponseResult<CourseVM>(HttpStatusCode.NotFound, null, "Course not found");
        }
    }
}
