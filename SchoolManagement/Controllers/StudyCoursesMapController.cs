using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudyCoursesMap;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyCoursesMapController : ControllerBase
    {
        private readonly IStudyCoursesMapService _studyCoursesMapService;

        public StudyCoursesMapController(IStudyCoursesMapService studyCoursesMapService)
        {
            _studyCoursesMapService = studyCoursesMapService;
        }

        [HttpPost]
        public ResponseResult<StudyCoursesMapVm> Create([FromBody] StudyCoursesMapCreateVm vm)
        {
            var result = _studyCoursesMapService.CreateStudyCoursesMap(vm);
            return new ResponseResult<StudyCoursesMapVm>(HttpStatusCode.Created, result, "Study courses map created successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<StudyCoursesMapVm>> GetAll(int tenantId)
        {
            var result = _studyCoursesMapService.GetAllStudyCoursesMaps(tenantId);
            return new ResponseResult<List<StudyCoursesMapVm>>(HttpStatusCode.OK, result, "Study courses maps fetched successfully");
        }

        [HttpGet("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyCoursesMapVm> GetById(int id, int tenantId)
        {
            var result = _studyCoursesMapService.GetStudyCoursesMapById(id, tenantId);
            if (result == null)
                return new ResponseResult<StudyCoursesMapVm>(HttpStatusCode.NotFound, null, "Study courses map not found");
            return new ResponseResult<StudyCoursesMapVm>(HttpStatusCode.OK, result, "Study courses map fetched successfully");
        }

        [HttpGet("by-plan/{studyPlanId}/tenant/{tenantId}")]
        public ResponseResult<List<StudyCoursesMapVm>> GetByPlanId(int studyPlanId, int tenantId)
        {
            var result = _studyCoursesMapService.GetStudyCoursesMapsByPlanId(studyPlanId, tenantId);
            return new ResponseResult<List<StudyCoursesMapVm>>(HttpStatusCode.OK, result, "Study courses maps by plan fetched successfully");
        }

        [HttpPut("{id:int}/tenant/{tenantId}")]
        public ResponseResult<StudyCoursesMapVm> Update(int id, int tenantId, [FromBody] StudyCoursesMapUpdateVm vm)
        {
            var result = _studyCoursesMapService.UpdateStudyCoursesMap(id, tenantId, vm);
            if (result == null)
                return new ResponseResult<StudyCoursesMapVm>(HttpStatusCode.NotFound, null, "Study courses map not found");
            return new ResponseResult<StudyCoursesMapVm>(HttpStatusCode.OK, result, "Study courses map updated successfully");
        }

        [HttpDelete("{id:int}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _studyCoursesMapService.DeleteStudyCoursesMap(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Study courses map not found");
            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Study courses map deleted successfully");
        }
    }
}
