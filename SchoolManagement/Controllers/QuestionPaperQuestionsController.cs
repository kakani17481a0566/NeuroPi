using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionPaperQuestions;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionPaperQuestionsController : ControllerBase
    {
        private readonly IQuestionPaperQuestionsService _service;

        public QuestionPaperQuestionsController(IQuestionPaperQuestionsService service)
        {
            _service = service;
        }

        [HttpGet("tenant/{tenantId:int}")]
        public async Task<ResponseResult<List<QuestionPaperQuestionsListVM>>> GetAllByTenantId(int tenantId)
        {
            var result = await _service.GetAllByTenantId(tenantId);
            if (result == null || result.Count == 0)
                return new ResponseResult<List<QuestionPaperQuestionsListVM>>(HttpStatusCode.NotFound, null, "No question paper questions found for this tenant.");

            return new ResponseResult<List<QuestionPaperQuestionsListVM>>(HttpStatusCode.OK, result, "Question paper questions fetched successfully.");
        }

        [HttpGet("{id:int}/{tenantId:int}")]
        public async Task<ResponseResult<QuestionPaperQuestionsGetVM>> GetById(int id, int tenantId)
        {
            var result = await _service.GetByIdAndTenantId(id, tenantId);
            if (result == null)
                return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.NotFound, null, $"Question paper question with ID {id} not found for this tenant.");

            return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.OK, result, "Question paper question fetched successfully.");
        }

        [HttpPost]
        public async Task<ResponseResult<QuestionPaperQuestionsGetVM>> Create([FromBody] QuestionPaperQuestionsCreateVM request)
        {
            var result = await _service.Create(request);
            if (result == null)
                return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.BadRequest, null, "Question paper question creation failed.");

            return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.Created, result, "Question paper question created successfully.");
        }

        [HttpPut("{id:int}/{tenantId:int}")]
        public async Task<ResponseResult<QuestionPaperQuestionsGetVM>> Update(int id, int tenantId, [FromBody] QuestionPaperQuestionsUpdateVM request)
        {
            var result = await _service.Update(id, tenantId, request);
            if (result == null)
                return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.BadRequest, null, "Question paper question update failed.");

            return new ResponseResult<QuestionPaperQuestionsGetVM>(HttpStatusCode.OK, result, "Question paper question updated successfully.");
        }

        [HttpDelete("{id:int}/{tenantId:int}")]
        public async Task<ResponseResult<string>> Delete(int id, int tenantId)
        {
            var result = await _service.Delete(id, tenantId);
            if (!result)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, $"Question paper question with ID {id} not found for this tenant.");

            return new ResponseResult<string>(HttpStatusCode.OK, $"Question paper question with ID {id} deleted successfully.");
        }

        [HttpGet("assessment/{tenantId:int}")]
        public async Task<ResponseResult<List<AssessmentSubdomainVM>>> GetAssessment(int tenantId)
        {
            var result = await _service.GetAssessmentByTenantId(tenantId);
            if (result == null || result.Count == 0)
                return new ResponseResult<List<AssessmentSubdomainVM>>(HttpStatusCode.NotFound, null, "No assessment data found for this tenant.");

            return new ResponseResult<List<AssessmentSubdomainVM>>(HttpStatusCode.OK, result, "Assessment data fetched successfully.");
        }
    }
}
