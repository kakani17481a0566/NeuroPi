using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.AssessmentSkills;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentSkillsController : ControllerBase
    {
        private readonly IAssessmentSkillService _assessmentSkillService;
        public AssessmentSkillsController(IAssessmentSkillService assessmentSkillService)
        {
            _assessmentSkillService = assessmentSkillService;
        }

        [HttpGet("GetAllSkills")]
        public ResponseResult<List<AssessmentSkillsResponseVM>> GetAllSkills()
        {
            var skills = _assessmentSkillService.GetAllSkills();
            return new ResponseResult<List<AssessmentSkillsResponseVM>>(HttpStatusCode.OK, skills, "All skills retrieved successfully");
        }

        [HttpGet("GetSkillById/{id}")]
        public ResponseResult<AssessmentSkillsResponseVM> GetSkillById(int id)
        {
            var skill = _assessmentSkillService.GetSkillById(id);
            return skill == null
                ? new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.NotFound, null, "Skill not found")
                : new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.OK, skill, "Skill retrieved successfully");
        }

        [HttpGet("GetSkillsByTenantId/{tenantId}")]
        public ResponseResult<List<AssessmentSkillsResponseVM>> GetSkillsByTenantId(int tenantId)
        {
            var skills = _assessmentSkillService.GetSkillsByTenantId(tenantId);
            return skills == null
                ? new ResponseResult<List<AssessmentSkillsResponseVM>>(HttpStatusCode.NotFound, null, "No skills found for the specified tenant")
                : new ResponseResult<List<AssessmentSkillsResponseVM>>(HttpStatusCode.OK, skills, "Skills retrieved successfully");
        }

        [HttpGet("GetSkillByIdAndTenantId/{id}/{tenantId}")]
        public ResponseResult<AssessmentSkillsResponseVM> GetSkillByIdAndTenantId(int id, int tenantId)
        {
            var skill = _assessmentSkillService.GetSkillByIdAndTenantId(id, tenantId);
            return skill == null
                ? new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.NotFound, null, "Skill not found for the specified tenant")
                : new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.OK, skill, "Skill retrieved successfully");
        }

        [HttpPost("CreateSkill")]
        public ResponseResult<AssessmentSkillsResponseVM> CreateSkill([FromBody] AssessmentSkillsRequestVM skillRequest)
        {
            if (skillRequest == null)
            {
                return new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.BadRequest, null, "Invalid skill data");
            }
            var createdSkill = _assessmentSkillService.CreateAssessmentSkill(skillRequest);
            return new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.Created, createdSkill, "Skill created successfully");
        }

        [HttpPut("UpdateSkill/{id}/{tenantId}")]
        public ResponseResult<AssessmentSkillsResponseVM> UpdateSkill(int id, int tenantId, [FromBody] AssessmentSkillsUpdateVM skillRequest)
        {
            if (skillRequest == null)
            {
                return new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.BadRequest, null, "Invalid skill data");
            }
            var updatedSkill = _assessmentSkillService.UpdateAssessmentSkill(id, tenantId, skillRequest);
            return updatedSkill == null
                ? new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.NotFound, null, "Skill not found for the specified ID and tenant")
                : new ResponseResult<AssessmentSkillsResponseVM>(HttpStatusCode.OK, updatedSkill, "Skill updated successfully");
        }

        [HttpDelete("DeleteSkill/{id}/{tenantId}")]
        public ResponseResult<bool> DeleteSkill(int id, int tenantId)
        {
            var isDeleted = _assessmentSkillService.DeleteAssessmentSkillByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Skill deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Skill not found for the specified ID and tenant");


        }
    }
}