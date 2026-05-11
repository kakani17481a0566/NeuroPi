using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionOption;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionOptionController : ControllerBase
    {
        private readonly IQuestionOptionService _QuestionOptionService;

        public QuestionOptionController(IQuestionOptionService QuestionOptionService)
        {
            _QuestionOptionService = QuestionOptionService;
        }

        [HttpGet]
        public ResponseResult<List<QuestionOptionResponseVM>> GetQuestionOptions()
        {
            var result = _QuestionOptionService.GetQuestionOptions();
            if (result == null)
            {
                return new ResponseResult<List<QuestionOptionResponseVM>>(HttpStatusCode.NotFound, null, "Question Option records Not Found");

            }
            return new ResponseResult<List<QuestionOptionResponseVM>>(HttpStatusCode.OK, result, "Question Option records retrieved successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<QuestionOptionResponseVM> GetQuestionOptionById(int id)
        {
            var result = _QuestionOptionService.GetQuestionOptionById(id);
            if (result == null)
            {
                return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.NotFound, null, "Question Option not found");
            }
            return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.OK, result, "Question Option retrieved successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<QuestionOptionResponseVM>> GetQuestionOptionsByTenantId(int tenantId)
        {
            var result = _QuestionOptionService.GetQuestionOptionByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<QuestionOptionResponseVM>>(HttpStatusCode.NotFound, null, "Question Option records not found for the specified tenant");
            }
            return new ResponseResult<List<QuestionOptionResponseVM>>(HttpStatusCode.OK, result, "Question Option records retrieved successfully for the specified tenant");
        }
        [HttpGet("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<QuestionOptionResponseVM> GetQuestionOptionByIdAndTenantId(int id, int tenantId)
        {
            var result = _QuestionOptionService.GetQuestionOptionByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.NotFound, null, "Question Option not found for the specified id and tenant");
            }
            return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.OK, result, "Question Option retrieved successfully for the specified id and tenant");
        }
        [HttpPost]
        public ResponseResult<QuestionOptionResponseVM> CreateQuestionOption(QuestionOptionRequestVM request)
        {
            var result = _QuestionOptionService.CreateQuestionOption(request);
            if (result == null)
            {
                return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create Question Option");
            }
            return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.OK, result, "Question Option created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<QuestionOptionResponseVM> UpdateQuestionOption(int id, int tenantId, QuestionOptionUpdateVM request)
        {
            var result = _QuestionOptionService.UpdateQuestionOption(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update Question Option");
            }
            return new ResponseResult<QuestionOptionResponseVM>(HttpStatusCode.OK, result, "Question Option updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteQuestionOption(int id, int tenantId)
        {
            var result = _QuestionOptionService.DeleteQuestionOption(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Failed to delete Question Option");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Question Option deleted successfully");
        }
    }
}