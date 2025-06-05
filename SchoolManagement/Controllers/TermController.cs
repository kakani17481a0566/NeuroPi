using Microsoft.AspNetCore.Mvc;
using System.Net;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Term;
using SchoolManagement.Response;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermController : ControllerBase
    {
        private readonly ITermService _termService;

        public TermController(ITermService termService)
        {
            _termService = termService;
        }

        [HttpGet("GetAll")]
        public ResponseResult<List<TermResponseVM>> GetAll()
        {
            var result = _termService.GetAll();
            return new ResponseResult<List<TermResponseVM>>(HttpStatusCode.OK, result);
        }

        [HttpGet("GetByTenantId/{tenantId}")]
        public ResponseResult<List<TermResponseVM>> GetByTenantId(int tenantId)
        {
            var result = _termService.GetByTenantId(tenantId);
            return new ResponseResult<List<TermResponseVM>>(HttpStatusCode.OK, result);
        }

        [HttpGet("GetById/{id}")]
        public ResponseResult<TermResponseVM> GetById(int id)
        {
            var result = _termService.GetById(id);
            if (result == null)
                return new ResponseResult<TermResponseVM>(HttpStatusCode.NotFound, null, "Term not found");
            return new ResponseResult<TermResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpGet("GetById/{id}/{tenantId}")]
        public ResponseResult<TermResponseVM> GetById(int id, int tenantId)
        {
            var result = _termService.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<TermResponseVM>(HttpStatusCode.NotFound, null, "Term not found");
            return new ResponseResult<TermResponseVM>(HttpStatusCode.OK, result);
        }

        [HttpPost("Post")]
        public ResponseResult<TermResponseVM> Post([FromBody] TermRequestVM termVM)
        {
            var createdTerm = _termService.Create(termVM);
            return new ResponseResult<TermResponseVM>(HttpStatusCode.Created, createdTerm, "Term created successfully");
        }

        [HttpPut("Put/{id}/{tenantId}")]
        public ResponseResult<TermResponseVM> Put(int id, int tenantId, [FromBody] TermUpdateVM termVM)
        {
            var updatedTerm = _termService.Update(id, tenantId, termVM);
            if (updatedTerm == null)
                return new ResponseResult<TermResponseVM>(HttpStatusCode.NotFound, null, "Term not found");
            return new ResponseResult<TermResponseVM>(HttpStatusCode.OK, updatedTerm, "Term updated successfully");
        }

        [HttpDelete("Delete/{id}/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var success = _termService.Delete(id, tenantId);
            if (!success)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Term not found");
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Term deleted successfully");
        }
    }
}
