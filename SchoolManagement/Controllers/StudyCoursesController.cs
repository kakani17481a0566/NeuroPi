using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyCourses;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyCoursesController : ControllerBase
    {
        private readonly IStudyCoursesService _studyCoursesService;

        public StudyCoursesController(IStudyCoursesService studyCoursesService)
        {
            _studyCoursesService = studyCoursesService;
        }

        [HttpPost]
        public ResponseResult<StudyCoursesVm> Create([FromBody] StudyCoursesCreateVm vm)
        {
            var result = _studyCoursesService.CreateStudyCourse(vm);
            return new ResponseResult<StudyCoursesVm>(HttpStatusCode.Created, result, "Study course created successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudyCoursesVm>> GetAll(int tenantId)
        {
            var result = _studyCoursesService.GetAllStudyCourses(tenantId);
            return new ResponseResult<List<StudyCoursesVm>>(HttpStatusCode.OK, result, "Study courses fetched successfully");
        }

        [HttpGet("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyCoursesVm> GetById(int id, int tenantId)
        {
            var result = _studyCoursesService.GetStudyCourseById(id, tenantId);
            if (result == null)
                return new ResponseResult<StudyCoursesVm>(HttpStatusCode.NotFound, null, "Study course not found");
            return new ResponseResult<StudyCoursesVm>(HttpStatusCode.OK, result, "Study course fetched successfully");
        }

        [HttpPut("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyCoursesVm> Update(int id, int tenantId, [FromBody] StudyCoursesUpdateVm vm)
        {
            var result = _studyCoursesService.UpdateStudyCourse(id, tenantId, vm);
            if (result == null)
                return new ResponseResult<StudyCoursesVm>(HttpStatusCode.NotFound, null, "Study course not found");
            return new ResponseResult<StudyCoursesVm>(HttpStatusCode.OK, result, "Study course updated successfully");
        }

        [HttpDelete("{id:int}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _studyCoursesService.DeleteStudyCourse(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Study course not found");
            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Study course deleted successfully");
        }
    }
}
