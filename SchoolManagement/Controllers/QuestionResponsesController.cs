using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionResponses;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionResponsesController : ControllerBase
    {
        private readonly IQuestionResponsesService _service;

        public QuestionResponsesController(IQuestionResponsesService service)
        {
            _service = service;
        }

        [HttpGet("tenant/{tenantId:int}")]
        public ResponseResult<List<QuestionResponsesListVM>> GetAllByTenantId(int tenantId)
        {
            var result = _service.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
                return new ResponseResult<List<QuestionResponsesListVM>>(HttpStatusCode.NotFound, null, "No question responses found for this tenant.");

            return new ResponseResult<List<QuestionResponsesListVM>>(HttpStatusCode.OK, result, "Question responses fetched successfully.");
        }

        [HttpGet("{id:int}/{tenantId:int}")]
        public ResponseResult<QuestionResponsesGetVM> GetById(int id, int tenantId)
        {
            var result = _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.NotFound, null, $"Question response with ID {id} not found for this tenant.");

            return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.OK, result, "Question response fetched successfully.");
        }

        [HttpPost]
        public ResponseResult<QuestionResponsesGetVM> Create([FromBody] QuestionResponsesCreateVM request)
        {
            var result = _service.Create(request);
            if (result == null)
                return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.BadRequest, null, "Question response creation failed.");

            return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.OK, result, "Question response created successfully.");
        }

        [HttpPost("batch")]
        public ResponseResult<List<QuestionResponsesGetVM>> CreateBatch([FromBody] QuestionResponsesBatchRequestVM request)
        {
            if (request == null || request.Responses == null || request.Responses.Count == 0)
                return new ResponseResult<List<QuestionResponsesGetVM>>(HttpStatusCode.BadRequest, null, "Request or response list is empty.");

            var results = _service.CreateBatch(request);
            return new ResponseResult<List<QuestionResponsesGetVM>>(HttpStatusCode.OK, results, "Question responses processed successfully.");
        }

        [HttpPut("{id:int}/{tenantId:int}")]
        public ResponseResult<QuestionResponsesGetVM> Update(int id, int tenantId, [FromBody] QuestionResponsesUpdateVM request)
        {
            var result = _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.BadRequest, null, "Question response update failed.");

            return new ResponseResult<QuestionResponsesGetVM>(HttpStatusCode.OK, result, "Question response updated successfully.");
        }

        [HttpDelete("{id:int}/{tenantId:int}")]
        public ResponseResult<string> Delete(int id, int tenantId)
        {
            var result = _service.Delete(id, tenantId);
            if (!result)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, $"Question response with ID {id} not found for this tenant.");

            return new ResponseResult<string>(HttpStatusCode.OK, $"Question response with ID {id} deleted successfully.");
        }
    }
}
