using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.SectionField;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionFieldController : ControllerBase
    {
        public readonly ISectionFieldService _sectionFieldService;

        public SectionFieldController(ISectionFieldService sectionFieldService)
        {
            _sectionFieldService = sectionFieldService;
        }

        [HttpGet]
        public ResponseResult<List<SectionFieldResponseVM>> GetAll()
        {
            var sectionFields = _sectionFieldService.GetAllSectionFields();
            return new ResponseResult<List<SectionFieldResponseVM>>(HttpStatusCode.OK, sectionFields);
        }

        [HttpGet("id/{id}")]

        public ResponseResult<SectionFieldResponseVM> GetById(int id)
        {
            var sectionField = _sectionFieldService.GetById(id);
            if(sectionField == null)
            {
                return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.OK, sectionField,"Section fields retrived successfully");

        }

        [HttpGet("tenant/{tenantId}/id/{id}")]
        public ResponseResult<SectionFieldResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var sectionField = _sectionFieldService.GetByIdAndTenantId(id, tenantId);
            if (sectionField == null)
            {
                return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.OK, sectionField, "Section fields retrived successfully");
        }

        [HttpGet("tenant{tenantId}")]
        public ResponseResult<SectionFieldResponseVM> GetByTenantId(int tenantId)
        { 
            var sectionField = _sectionFieldService.GetByTenantId(tenantId);
            if (sectionField == null)
            {
                return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.NotFound, null, "Section field not found");
            }
            return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.OK, sectionField, "Section fields retrived successfully");
        }

        [HttpPost]
        public ResponseResult<SectionFieldResponseVM> create(SectionFieldRequestVM requestVM)
        {
            var CreateSectionField = _sectionFieldService.CreateSectionField(requestVM);
            if (CreateSectionField == null)
            {
                return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create section field");
            }
            return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.OK, CreateSectionField, "Section field created successfully");

        }

        [HttpPut("tenant/{tenantId}/id/{id}")]
        public ResponseResult<SectionFieldResponseVM> updateSectionField(int id,int tenantId,SectionFieldUpdateVM updateVM)
        {
            var updateSectionField = _sectionFieldService.UpdateSectionField(id, tenantId, updateVM);
            if (updateSectionField == null)
            {
                return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update section field");
            }
            return new ResponseResult<SectionFieldResponseVM>(HttpStatusCode.OK, updateSectionField, "Section field updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> delete(int id, int tenantId)
        {
            var deleteSectionField = _sectionFieldService.DeleteSectionField(id, tenantId);
            if (deleteSectionField == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Failed to delete section field");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Section field deleted successfully");
        }

    }
}
