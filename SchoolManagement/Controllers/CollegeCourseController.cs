using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CollegeCourse;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeCourseController : ControllerBase
    {
        private readonly ICollegeCourseService _collegeCourseService;

        public CollegeCourseController(ICollegeCourseService collegeCourseService)
        {
            _collegeCourseService = collegeCourseService;
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<CollegeCourseResponseVM>> GetAllByTenantId(int tenantId)
        {
            var result = _collegeCourseService.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<CollegeCourseResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "College Courses Not Found");
            }
            return new ResponseResult<List<CollegeCourseResponseVM>>(System.Net.HttpStatusCode.OK, result, "College Courses fetched successfully");
        }
    }
}
