using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PublicHoliday;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicHolidayController : ControllerBase
    {
        private readonly IPublicHolidayService _service;

        public PublicHolidayController(IPublicHolidayService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<PublicHolidayResponseVM>> GetAll()
        {
            var result = _service.GetAll();
            if (result == null || result.Count == 0)
                return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.NotFound, null, "No public holidays found.");

            return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.OK, result, "Public holidays fetched successfully.");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<PublicHolidayResponseVM>> GetByTenant(int tenantId)
        {
            var result = _service.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
                return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.NotFound, null, $"No public holidays for tenant {tenantId}.");

            return new ResponseResult<List<PublicHolidayResponseVM>>(HttpStatusCode.OK, result, "Public holidays fetched successfully.");
        }

        [HttpGet("{id}")]
        public ResponseResult<PublicHolidayResponseVM> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.NotFound, null, $"Public holiday with ID {id} not found.");

            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, result, "Public holiday fetched successfully.");
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<PublicHolidayResponseVM> GetByIdAndTenant(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.NotFound, null, $"No public holiday with ID {id} and tenant {tenantId}.");

            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, result, "Public holiday fetched successfully.");
        }

        [HttpPost]
        public ResponseResult<PublicHolidayResponseVM> Create([FromBody] PublicHolidayRequestVM request)
        {
            var result = _service.CreatePublicHoliday(request);
            if (result == null)
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.BadRequest, null, "Public holiday creation failed.");

            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, result, "Public holiday created successfully.");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<PublicHolidayResponseVM> Update(int id, int tenantId, [FromBody] PublicHolidayRequestVM request)
        {
            var result = _service.UpdatePublicHoliday(id, tenantId, request);
            if (result == null)
                return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.BadRequest, null, "Public holiday update failed.");

            return new ResponseResult<PublicHolidayResponseVM>(HttpStatusCode.OK, result, "Public holiday updated successfully.");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var result = _service.DeleteById(id, tenantId);
            if (result == null)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, $"No public holiday found with ID {id}.");

            return new ResponseResult<string>(HttpStatusCode.OK, $"Public holiday with ID {id} deleted successfully.");
        }
    }
}
