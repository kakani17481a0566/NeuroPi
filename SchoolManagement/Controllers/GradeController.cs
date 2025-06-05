using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Grade;

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

        [HttpGet]
        public ResponseResult<List<GradeResponseVM>> GetAllGrades()
        {
            var grades = _gradeService.GetAllGrades();
            return new ResponseResult<List<GradeResponseVM>>(HttpStatusCode.OK, grades, "All grades retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<GradeResponseVM> GetGradeById(int id)
        {
            var grade = _gradeService.GetGradeById(id);
            return grade == null
                ? new ResponseResult<GradeResponseVM>(HttpStatusCode.NotFound, null, "Grade not found")
                : new ResponseResult<GradeResponseVM>(HttpStatusCode.OK, grade, "Grade retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<GradeResponseVM>> GetGradesByTenantId(int tenantId)
        {
            var grades = _gradeService.GetAllGradesByTenantId(tenantId);
            return grades == null
                ? new ResponseResult<List<GradeResponseVM>>(HttpStatusCode.NotFound, null, "No grades found for the specified tenant")
                : new ResponseResult<List<GradeResponseVM>>(HttpStatusCode.OK, grades, "Grades retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<GradeResponseVM> GetGradeByIdAndTenantId(int id, int tenantId)
        {
            var grade = _gradeService.GetGradeByIdAndTenantId(id, tenantId);
            return grade == null
                ? new ResponseResult<GradeResponseVM>(HttpStatusCode.NotFound, null, "Grade not found for the specified ID and Tenant ID")
                : new ResponseResult<GradeResponseVM>(HttpStatusCode.OK, grade, "Grade retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<GradeResponseVM> CreateGrade(GradeRequestVM gradeRequest)
        {
            var createdGrade = _gradeService.CreateGrade(gradeRequest);
            return new ResponseResult<GradeResponseVM>(HttpStatusCode.Created, createdGrade, "Grade created successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<GradeResponseVM> UpdateGrade(int id, int tenantId, GradeUpdateVM gradeUpdate)
        {
            var updatedGrade = _gradeService.UpdateGrade(id, gradeUpdate);
            return updatedGrade == null
                ? new ResponseResult<GradeResponseVM>(HttpStatusCode.NotFound, null, "Grade not found for the specified ID and Tenant ID")
                : new ResponseResult<GradeResponseVM>(HttpStatusCode.OK, updatedGrade, "Grade updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteGrade(int id, int tenantId)
        {
            var isDeleted = _gradeService.DeleteGrade(id);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Grade deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Grade not found or could not be deleted");


        }
    }
}