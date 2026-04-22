using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CollegeDetail;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeDetailController : ControllerBase
    {
        private readonly ICollegeDetailService _collegeDetailService;

        public CollegeDetailController(ICollegeDetailService collegeDetailService)
        {
            _collegeDetailService = collegeDetailService;
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<CollegeDetailResponseVM>> GetAllByTenantId(int tenantId)
        {
            var result = _collegeDetailService.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<CollegeDetailResponseVM>>(System.Net.HttpStatusCode.NotFound, result, "College Details Not Found");
            }
            return new ResponseResult<List<CollegeDetailResponseVM>>(System.Net.HttpStatusCode.OK, result, "College Details fetched successfully");
        }
    }
}
