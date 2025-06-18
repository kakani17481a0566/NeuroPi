using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Grade;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;

        }   
        [HttpGet("{tenantId}")]
        public ResponseResult<List<GradeResponseVM>> GradesByTenantId(int tenantId)
        {
            var grades =_gradeService.GradesByTenantId(tenantId);
            return grades == null
                ? new ResponseResult<List<GradeResponseVM>>(HttpStatusCode.NotFound, null, "Grades not found With given Tenant Id")
                : new ResponseResult<List<GradeResponseVM>>(HttpStatusCode.OK, grades, "Grades returned successfully");


        }
    }
}
