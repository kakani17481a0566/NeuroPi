using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionPaper;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionPaperController : ControllerBase
    {
        private readonly IQuestionPaperService _QuestionPaperService;

        public QuestionPaperController(IQuestionPaperService QuestionPaperService)
        {
            _QuestionPaperService = QuestionPaperService;
        }

        [HttpGet]
        public ResponseResult<List<QuestionPaperResponseVM>> GetQuestionPapers()
        {
            var result = _QuestionPaperService.GetQuestionPapers();
            if (result == null)
            {
                return new ResponseResult<List<QuestionPaperResponseVM>>(HttpStatusCode.NotFound, null, "Question Paper records Not Found");

            }
            return new ResponseResult<List<QuestionPaperResponseVM>>(HttpStatusCode.OK, result, "Question Paper records retrieved successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<QuestionPaperResponseVM> GetQuestionPaperById(int id)
        {
            var result = _QuestionPaperService.GetQuestionPaperById(id);
            if (result == null)
            {
                return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.NotFound, null, "Question Paper not found");
            }
            return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.OK, result, "Question Paper retrieved successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<QuestionPaperResponseVM>> GetQuestionCategoriesByTenantId(int tenantId)
        {
            var result = _QuestionPaperService.GetQuestionPaperByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<QuestionPaperResponseVM>>(HttpStatusCode.NotFound, null, "Question Paper records not found for the specified tenant");
            }
            return new ResponseResult<List<QuestionPaperResponseVM>>(HttpStatusCode.OK, result, "Question Paper records retrieved successfully for the specified tenant");
        }
        [HttpGet("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<QuestionPaperResponseVM> GetQuestionPaperByIdAndTenantId(int id, int tenantId)
        {
            var result = _QuestionPaperService.GetQuestionPaperByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.NotFound, null, "Question Paper not found for the specified id and tenant");
            }
            return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.OK, result, "Question Paper retrieved successfully for the specified id and tenant");
        }
        [HttpPost]
        public ResponseResult<QuestionPaperResponseVM> CreateQuestionPaper(QuestionPaperRequestVM request)
        {
            var result = _QuestionPaperService.CreateQuestionPaper(request);
            if (result == null)
            {
                return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create Question Paper");
            }
            return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.OK, result, "Question Paper created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<QuestionPaperResponseVM> UpdateQuestionPaper(int id, int tenantId, QuestionPaperUpdateVM request)
        {
            var result = _QuestionPaperService.UpdateQuestionPaper(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update Question Paper");
            }
            return new ResponseResult<QuestionPaperResponseVM>(HttpStatusCode.OK, result, "Question Paper updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteQuestionPaper(int id, int tenantId)
        {
            var result = _QuestionPaperService.DeleteQuestionPaper(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Failed to delete Question Paper");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Question Paper deleted successfully");
        }
    }
}