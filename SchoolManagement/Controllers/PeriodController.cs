using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Period;
using SchoolManagement.Response;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService _periodService;

        public PeriodController(IPeriodService periodService)
        {
            _periodService = periodService;
        }

        [HttpGet]
        public ResponseResult<object> GetAll()
        {
            var result = _periodService.GetAll();
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<object> GetByTenantId(int tenantId)
        {
            var result = _periodService.GetByTenantId(tenantId);
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}")]
        public ResponseResult<object> GetById(int id)
        {
            var result = _periodService.GetById(id);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Period not found");
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }

        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<object> GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _periodService.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Period not found");
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }

        [HttpPost]
        public ResponseResult<object> Create([FromBody] PeriodRequestVM model)
        {
            var result = _periodService.Create(model);
            return new ResponseResult<object>(HttpStatusCode.Created, result);
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<object> Update(int id, int tenantId, [FromBody] PeriodUpdateVM model)
        {
            var result = _periodService.Update(id, tenantId, model);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Period not found");
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<object> Delete(int id, int tenantId)
        {
            var success = _periodService.Delete(id, tenantId);
            if (!success)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Period not found");
            return new ResponseResult<object>(HttpStatusCode.OK, null, "Period deleted successfully");
        }


        [HttpGet("table-data")]
        public ResponseResult<object> GetTableData([FromQuery] int tenantId, [FromQuery] int courseId)
        {
            var result = _periodService.GetHeadersWithData(tenantId, courseId);
            return new ResponseResult<object>(HttpStatusCode.OK, result);
        }



    }
}
