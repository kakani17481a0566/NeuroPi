using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuropiForms.Services.Interface;
using NeuropiForms.ViewModels.Sections;
using System.Net;

namespace NeuropiForms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }


        [HttpGet]
        public ResponseResult<List<SectionsResponseVM>> GetAllSections()
        {
            var sections = _sectionService.GetAllSections();
            return new ResponseResult<List<SectionsResponseVM>>(HttpStatusCode.OK, sections);
        }

        [HttpGet("id{id}")]
        public ResponseResult<SectionsResponseVM> GetSectionById(int id)
        {
            var section = _sectionService.GetSectionById(id);
            if (section == null)
                return new ResponseResult<SectionsResponseVM>(HttpStatusCode.NotFound, null, "Section not found");
            return new ResponseResult<SectionsResponseVM>(HttpStatusCode.OK, section);
        }

        [HttpGet("tenantId/{tenantId}id/{id}")]
        public ResponseResult<SectionsResponseVM> GetSectionByIdAndTenantId(int tenantId, int id)
        {
            var section = _sectionService.GetSectionByIdAndTenantId(id, tenantId);
            if (section == null)
                return new ResponseResult<SectionsResponseVM>(HttpStatusCode.NotFound, null, "Section not found for the given tenant");
            return new ResponseResult<SectionsResponseVM>(HttpStatusCode.OK, section);
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<SectionsResponseVM> GetSectionByTenantId(int tenantId)
        {
            var section = _sectionService.GetSectionByTenantId(tenantId);
            if (section == null)
                return new ResponseResult<SectionsResponseVM>(HttpStatusCode.NotFound, null, "Section not found for the given tenant");
            return new ResponseResult<SectionsResponseVM>(HttpStatusCode.OK, section);
        }

        [HttpPost]
        public ResponseResult<SectionsResponseVM> CreateSection(SectionsRequestVM section)
        {
            var createdSection = _sectionService.CreateSection(section);
            return new ResponseResult<SectionsResponseVM>(HttpStatusCode.Created, createdSection);
        }

        [HttpPut("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<SectionsResponseVM> UpdateSection(int id, int tenantId, SectionsUpdateVM section)
        {
            var updatedSection = _sectionService.UpdateSection(id, tenantId, section);
            if (updatedSection == null)
                return new ResponseResult<SectionsResponseVM>(HttpStatusCode.NotFound, null, "Section not found for update");
            return new ResponseResult<SectionsResponseVM>(HttpStatusCode.OK, updatedSection);
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteSection(int id, int tenantId)
        {
            var isDeleted = _sectionService.DeleteSection(id, tenantId);
            if (!isDeleted)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Section not found for deletion");
            return new ResponseResult<bool>(HttpStatusCode.OK, true);
        }



    }
}
