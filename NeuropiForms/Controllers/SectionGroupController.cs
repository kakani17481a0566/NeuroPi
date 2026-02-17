using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SectionGroup;
using NeuropiForms.ViewModels.SectionGroup;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionGroupController : ControllerBase
    {
        public readonly ISectionGroupService _SectionGroupService;

        public SectionGroupController(ISectionGroupService sectionGroupService)
        {
            _SectionGroupService = sectionGroupService;
        }

        [HttpGet]
        public ResponseResult<List<SectionGroupResponseVM>> GetAll()
        {
            var SectionGroups = _SectionGroupService.GetSectionGroups();
            return new ResponseResult<List<SectionGroupResponseVM>>(HttpStatusCode.OK, SectionGroups);
        }

        [HttpGet("id/{id}")]

        public ResponseResult<SectionGroupResponseVM> GetById(int id)
        {
            var SectionGroup = _SectionGroupService.GetById(id);
            if (SectionGroup == null)
            {
                return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.OK, SectionGroup, "Section fields retrived successfully");

        }

        [HttpGet("tenant/{tenantId}/id/{id}")]
        public ResponseResult<SectionGroupResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var SectionGroup = _SectionGroupService.GetByIdAndTenantId(id, tenantId);
            if (SectionGroup == null)
            {
                return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.OK, SectionGroup, "Section fields retrived successfully");
        }

        [HttpGet("tenant{tenantId}")]
        public ResponseResult<SectionGroupResponseVM> GetByTenantId(int tenantId)
        {
            var SectionGroup = _SectionGroupService.GetByTenantId(tenantId);
            if (SectionGroup == null)
            {
                return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.OK, SectionGroup, "Section fields retrived successfully");
        }

        [HttpPost]
        public ResponseResult<SectionGroupResponseVM> create(SectionGroupRequestVM requestVM)
        {
            var CreateSectionGroup = _SectionGroupService.CreateSectionGroup(requestVM);
            if (CreateSectionGroup == null)
            {
                return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create section field");
            }
            return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.OK, CreateSectionGroup, "Section field created successfully");

        }

        [HttpPut("tenant/{tenantId}/id/{id}")]
        public ResponseResult<SectionGroupResponseVM> updateSectionGroup(int id, int tenantId, SectionGroupUpdateVM updateVM)
        {
            var updateSectionGroup = _SectionGroupService.Update(id, tenantId, updateVM);
            if (updateSectionGroup == null)
            {
                return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update section field");
            }
            return new ResponseResult<SectionGroupResponseVM>(HttpStatusCode.OK, updateSectionGroup, "Section field updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> delete(int id, int tenantId)
        {
            var deleteSectionGroup = _SectionGroupService.DeleteSectionGroup(id, tenantId);
            if (deleteSectionGroup == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Failed to delete section field");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Section field deleted successfully");
        }

    }
}
