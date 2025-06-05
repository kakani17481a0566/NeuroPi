using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.DailyAssessment;
using System.Collections.Generic;
using System.Net;

// Developed by: Mohith 

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyAssessmentController : ControllerBase
    {
        private readonly IDailyAssessmentService _service;

        public DailyAssessmentController(IDailyAssessmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<DailyAssessmentResponseVm>> GetAll()
        {
            var data = _service.GetAll();
            return new ResponseResult<List<DailyAssessmentResponseVm>>(HttpStatusCode.OK, data, "All assessments fetched");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<DailyAssessmentResponseVm>> GetAllByTenant(int tenantId)
        {
            var data = _service.GetAllByTenant(tenantId);
            return new ResponseResult<List<DailyAssessmentResponseVm>>(HttpStatusCode.OK, data, $"Assessments for tenant {tenantId}");
        }

        [HttpGet("{id}")]
        public ResponseResult<object> GetById(int id)
        {
            var result = _service.GetById(id);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Assessment not found");

            return new ResponseResult<object>(HttpStatusCode.OK, result, "Assessment found");
        }

        [HttpPost]
        public ResponseResult<DailyAssessmentResponseVm> Create([FromBody] DailyAssessmentRequestVm request)
        {
            var result = _service.Create(request);
            return new ResponseResult<DailyAssessmentResponseVm>(HttpStatusCode.Created, result, "Assessment created");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var success = _service.Delete(id, tenantId);
            if (!success)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Assessment not found or already deleted");

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Assessment deleted");
        }


        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<object> GetByIdAndTenant(int id, int tenantId)
        {
            var result = _service.GetById(id, tenantId);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Assessment not found");

            return new ResponseResult<object>(HttpStatusCode.OK, result, "Assessment found");
        }

        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<object> Update(int id, int tenantId, [FromBody] DailyAssessmentUpdateVm request)
        {
            var result = _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Assessment not found");

            return new ResponseResult<object>(HttpStatusCode.OK, result, "Assessment updated");
        }

    }
}
