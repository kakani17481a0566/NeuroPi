using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Worksheets;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSheetController : ControllerBase
    {
        private readonly IWorkSheetService _service;

        public WorkSheetController(IWorkSheetService service)
        {
            _service = service;
        }

        // 1. Get all worksheets (admin/global)
        [HttpGet("all")]
        public ResponseResult<List<WorksheetResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<WorksheetResponseVM>>(HttpStatusCode.OK, result);
        }

        // 2. Get all worksheets for a specific tenant
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<WorksheetResponseVM>> GetAllByTenant(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<WorksheetResponseVM>>(HttpStatusCode.OK, result);
        }

        // 3. Get worksheet by ID (admin/global)
        [HttpGet("{id}")]
        public ResponseResult<WorksheetResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.OK, result);
        }

        // 4. Get worksheet by ID and tenant
        [HttpGet("tenant/{tenantId}/{id}")]
        public ResponseResult<WorksheetResponseVM> GetByIdWithTenant(int tenantId, int id)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.OK, result);
        }

        // 5. Create a new worksheet
        [HttpPost]
        [Consumes("multipart/form-data")]
        public ResponseResult<WorksheetResponseVM> Create([FromForm] WorksheetRequestVM request)

        {
            var result = _service.Create(request);
            return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.Created, result, "Created successfully");
        }

        // 6. Update worksheet by tenant and id
        [HttpPut("tenant/{tenantId}/{id}")]
        public ResponseResult<WorksheetResponseVM> Update(int tenantId, int id, [FromBody] WorksheetUpdateVM request)
        {
            var result = _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<WorksheetResponseVM>(HttpStatusCode.OK, result, "Updated successfully");
        }

        // 7. Delete worksheet by tenant and id
        [HttpDelete("tenant/{tenantId}/{id}")]
        public ResponseResult<bool> Delete(int tenantId, int id)
        {
            var deleted = _service.Delete(id, tenantId);
            if (!deleted)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Record not found");

            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Deleted successfully");
        }
    }
}
