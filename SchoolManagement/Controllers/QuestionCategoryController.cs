using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionCategory;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCategoryController : ControllerBase
    {
        private readonly IQuestionCategoryService _questionCategoryService;

        public QuestionCategoryController(IQuestionCategoryService questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }

        [HttpGet]
        public ResponseResult<List<QuestionCategoryResponseVM>> GetQuestionCategories()
        {
            var result = _questionCategoryService.GetQuestionCategories();
            if (result == null)
            {
                return new ResponseResult<List<QuestionCategoryResponseVM>>(HttpStatusCode.NotFound, null, "Question Category records Not Found");

            }
            return new ResponseResult<List<QuestionCategoryResponseVM>>(HttpStatusCode.OK, result, "Question Category records retrieved successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<QuestionCategoryResponseVM> GetQuestionCategoryById(int id)
        {
            var result = _questionCategoryService.GetQuestionCategoryById(id);
            if (result == null)
            {
                return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.NotFound, null, "Question Category not found");
            }
            return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.OK, result, "Question Category retrieved successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<QuestionCategoryResponseVM>> GetQuestionCategoriesByTenantId(int tenantId)
        {
            var result = _questionCategoryService.GetQuestionCategoryByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<QuestionCategoryResponseVM>>(HttpStatusCode.NotFound, null, "Question Category records not found for the specified tenant");
            }
            return new ResponseResult<List<QuestionCategoryResponseVM>>(HttpStatusCode.OK, result, "Question Category records retrieved successfully for the specified tenant");
        }
        [HttpGet("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<QuestionCategoryResponseVM> GetQuestionCategoryByIdAndTenantId(int id, int tenantId)
        {
            var result = _questionCategoryService.GetQuestionCategoryByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.NotFound, null, "Question Category not found for the specified id and tenant");
            }
            return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.OK, result, "Question Category retrieved successfully for the specified id and tenant");
        }
        [HttpPost]
        public ResponseResult<QuestionCategoryResponseVM> CreateQuestionCategory(QuestionCategoryRequestVM request)
        {
            var result = _questionCategoryService.CreateQuestionCategory(request);
            if (result == null)
            {
                return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create Question Category");
            }
            return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.OK, result, "Question Category created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<QuestionCategoryResponseVM> UpdateQuestionCategory(int id, int tenantId, QuestionCategoryUpdateVM request)
        {
            var result = _questionCategoryService.UpdateQuestionCategory(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update Question Category");
            }
            return new ResponseResult<QuestionCategoryResponseVM>(HttpStatusCode.OK, result, "Question Category updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteQuestionCategory(int id, int tenantId)
        {
            var result = _questionCategoryService.DeleteQuestionCategory(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Failed to delete Question Category");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Question Category deleted successfully");
        }
    }
}