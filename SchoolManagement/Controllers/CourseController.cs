using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Course;
using System.Collections.Generic;
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

        // Create a new course
        [HttpPost]
        public ActionResult<ResponseResult<CourseVm>> Create([FromBody] CourseCreateVm courseCreateVm)
        {
            var result = _courseService.CreateCourse(courseCreateVm);
            return new ResponseResult<CourseVm>(HttpStatusCode.Created, result, "Course created successfully");
        }

        // Get all courses
        [HttpGet]
        public ActionResult<ResponseResult<List<CourseVm>>> GetAll()
        {
            var result = _courseService.GetAllCourses();
            return new ResponseResult<List<CourseVm>>(HttpStatusCode.OK, result, "All courses fetched successfully");
        }

        // Get course by ID
        [HttpGet("{id}")]
        public ActionResult<ResponseResult<CourseVm>> GetById(int id)
        {
            var result = _courseService.GetCourseById(id);
            if (result == null)
            {
                return new ResponseResult<CourseVm>(HttpStatusCode.NotFound, null, "Course not found");
            }

            return new ResponseResult<CourseVm>(HttpStatusCode.OK, result, "Course fetched successfully");
        }

        // Get all courses for a tenant
        [HttpGet("tenant/{tenantId}")]
        public ActionResult<ResponseResult<List<CourseVm>>> GetByTenant(int tenantId)
        {
            var result = _courseService.GetCoursesByTenantId(tenantId);
            return new ResponseResult<List<CourseVm>>(HttpStatusCode.OK, result, "Courses by tenant fetched successfully");
        }

        // Update a course
        [HttpPut("{id}/tenant/{tenantId}")]
        public ActionResult<ResponseResult<CourseVm>> Update(int id, int tenantId, [FromBody] CourseUpdateVm courseUpdateVm)
        {
            var result = _courseService.UpdateCourse(id, tenantId, courseUpdateVm);
            if (result == null)
            {
                return new ResponseResult<CourseVm>(HttpStatusCode.NotFound, null, "Course not found or not updated");
            }

            return new ResponseResult<CourseVm>(HttpStatusCode.OK, result, "Course updated successfully");
        }

        // Delete a course
        [HttpDelete("{id}/tenant/{tenantId}")]
        public ActionResult<ResponseResult<string>> Delete(int id, int tenantId)
        {
            var success = _courseService.DeleteCourseByIdAndTenantId(id, tenantId);
            if (!success)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Course not found or already deleted");
            }

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Course deleted successfully");
        }
    }
}
