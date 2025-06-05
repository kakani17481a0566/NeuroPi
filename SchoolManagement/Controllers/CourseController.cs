using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Course;
using System.Net;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpPost]
    public ResponseResult<CourseVm> Create([FromBody] CourseCreateVm courseCreateVm)
    {
        var result = _courseService.CreateCourse(courseCreateVm);
        return new ResponseResult<CourseVm>(HttpStatusCode.Created, result, "Course created successfully");
    }

    [HttpGet]
    public ResponseResult<List<CourseVm>> GetAll()
    {
        var result = _courseService.GetAllCourses();
        return new ResponseResult<List<CourseVm>>(HttpStatusCode.OK, result, "All courses fetched successfully");
    }

    [HttpGet("{id}")]
    public ResponseResult<CourseVm> GetById(int id)
    {
        var result = _courseService.GetCourseById(id);
        if (result == null)
            return new ResponseResult<CourseVm>(HttpStatusCode.NotFound, null, "Course not found");

        return new ResponseResult<CourseVm>(HttpStatusCode.OK, result, "Course fetched successfully");
    }

    [HttpGet("tenant/{tenantId}")]
    public ResponseResult<List<CourseVm>> GetByTenant(int tenantId)
    {
        var result = _courseService.GetCoursesByTenantId(tenantId);
        return new ResponseResult<List<CourseVm>>(HttpStatusCode.OK, result, "Courses by tenant fetched successfully");
    }

    [HttpPut("{id}/tenant/{tenantId}")]
    public ResponseResult<CourseVm> Update(int id, int tenantId, [FromBody] CourseUpdateVm courseUpdateVm)
    {
        var result = _courseService.UpdateCourse(id, tenantId, courseUpdateVm);
        if (result == null)
            return new ResponseResult<CourseVm>(HttpStatusCode.NotFound, null, "Course not found or not updated");

        return new ResponseResult<CourseVm>(HttpStatusCode.OK, result, "Course updated successfully");
    }

    [HttpDelete("{id}/tenant/{tenantId}")]
    public ResponseResult<string> Delete(int id, int tenantId)
    {
        var success = _courseService.DeleteCourseByIdAndTenantId(id, tenantId);
        if (!success)
            return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Course not found or already deleted");

        return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Course deleted successfully");
    }
}
