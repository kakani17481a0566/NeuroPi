using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.TimeTableWorksheet;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableWorksheetController : ControllerBase
    {
        private readonly ITimeTableWorksheetService _service;

        public TimeTableWorksheetController(ITimeTableWorksheetService service)
        {
            _service = service;
        }

        // 1. Get all worksheets (Admin/global)
        [HttpGet("all")]
        public ResponseResult<List<TimeTableWorksheetResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            return new ResponseResult<List<TimeTableWorksheetResponseVM>>(HttpStatusCode.OK, result);
        }

        // 2. Get all worksheets for a tenant
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<TimeTableWorksheetResponseVM>> GetAllByTenant(int tenantId)
        {
            var result = _service.GetAll(tenantId);
            return new ResponseResult<List<TimeTableWorksheetResponseVM>>(HttpStatusCode.OK, result);
        }

        // 3. Get worksheet by ID (Admin/global)
        [HttpGet("{id}")]
        public ResponseResult<TimeTableWorksheetResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.OK, result);
        }

        // 4. Get worksheet by ID and tenant
        [HttpGet("tenant/{tenantId}/{id}")]
        public ResponseResult<TimeTableWorksheetResponseVM> GetByIdWithTenant(int tenantId, int id)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.OK, result);
        }

        // 5. Create new worksheet
        [HttpPost]
        public ResponseResult<TimeTableWorksheetResponseVM> Create([FromBody] TimeTableWorksheetRequestVM request)
        {
            var result = _service.Create(request);
            return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.Created, result, "Created successfully");
        }

        // 6. Update worksheet by tenant and id
        [HttpPut("tenant/{tenantId}/{id}")]
        public ResponseResult<TimeTableWorksheetResponseVM> Update(int tenantId, int id, [FromBody] TimeTableWorksheetUpdateVM request)
        {
            var result = _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.NotFound, null, "Record not found");

            return new ResponseResult<TimeTableWorksheetResponseVM>(HttpStatusCode.OK, result, "Updated successfully");
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
