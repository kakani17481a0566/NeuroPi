using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
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

        [HttpGet]
        public ResponseResult<List<CourseTeacherResponseVM>> GetAllCourseTeachers()
        {
            var response = _courseTeacherService.GetAllCourseTeachers();
            if (response == null)
            {
                return new ResponseResult<List<CourseTeacherResponseVM>>(HttpStatusCode.NotFound, response, "No data Found");
            }
            return new ResponseResult<List<CourseTeacherResponseVM>>(HttpStatusCode.OK, response, "CourseTeachers fetched successfully");
        }

        [HttpGet("GetCourseTeachersByTenant/{tenantId}")]
        public ResponseResult<List<CourseTeacherResponseVM>> GetCourseTeachersByTenant([FromRoute] int tenantId)
        {
            var response = _courseTeacherService.GetCourseTeachersByTenant(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<CourseTeacherResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified tenant");
            }
            return new ResponseResult<List<CourseTeacherResponseVM>>(HttpStatusCode.OK, response, "CourseTeachers fetched successfully");
        }

        [HttpGet("GetCourseTeacherById/{id}")]
        public ResponseResult<CourseTeacherResponseVM> GetCourseTeacherById([FromRoute] int id)
        {
            var response = _courseTeacherService.GetCourseTeacherById(id);
            if (response != null)
            {
                return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.OK, response, "CourseTeacher is fetched successfully");
            }
            return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.BadGateway, response, $" CourseTeacher not found with id {id}");
        }

        [HttpGet("GetCourseTeacherByIdAndTenant/{id}/{tenantId}")]
        public ResponseResult<CourseTeacherResponseVM> GetCourseTeacherByIdAndTenantId([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = _courseTeacherService.GetCourseTeacherByIdAndTenant(id, tenantId);
            if (response != null)
            {
                return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.OK, response, "CourseTeacher is fetched successfully");
            }
            return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.BadGateway, response, $" CourseTeacher not found with id {id} for the specified tenant");
        }

        [HttpPost("CreateCourseTeacher")]
        public ResponseResult<CourseTeacherResponseVM> CreateCourseTeacher([FromBody] CourseTeacherRequestVM courseTeacherRequestVM)
        {
            var response = _courseTeacherService.CreateCourseTeacher(courseTeacherRequestVM);
            if (response != null)
            {
                return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.OK, response, "CourseTeacher created successfully");
            }
            return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.BadGateway, response, "Failed to create CourseTeacher");
        }

        [HttpPut("UpdateCourseTeacher/{id}/{tenantId}")]
        public ResponseResult<CourseTeacherResponseVM> UpdateCourseTeacher([FromRoute] int id, [FromRoute] int tenantId, [FromBody] CourseTeacherUpdateVM courseTeacherUpdateVM)
        {
            var response = _courseTeacherService.UpdateCourseTeacher(id, tenantId, courseTeacherUpdateVM);
            if (response != null)
            {
                return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.OK, response, "CourseTeacher updated successfully");
            }
            return new ResponseResult<CourseTeacherResponseVM>(HttpStatusCode.BadGateway, response, "Failed to update CourseTeacher");
        }

        [HttpDelete("DeleteCourseTeacherByIdAndTenant/{id}/{tenantId}")]
        public ResponseResult<bool> DeleteCourseTeacherByIdAndTenant([FromRoute] int id, [FromRoute] int tenantId)
        {
            var response = _courseTeacherService.DeleteCourseTeacherByIdAndTenant(id, tenantId);
            if (response)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, response, "CourseTeacher deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.BadGateway, response, "Failed to delete CourseTeacher");
        }
    }
}
