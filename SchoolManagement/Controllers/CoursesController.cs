using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Courses;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<CoursesResponseVM>> GetAllByTenantId(int tenantId)
        {
            var result = _coursesService.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<CoursesResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "Courses Not Found");
            }
            return new ResponseResult<List<CoursesResponseVM>>(System.Net.HttpStatusCode.OK, result, "Courses fetched successfully");
        }
    }
}
