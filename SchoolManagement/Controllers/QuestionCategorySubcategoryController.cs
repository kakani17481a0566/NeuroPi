using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.QuestionCategorySubcategory;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionCategorySubcategoryController : ControllerBase
    {
        private readonly IQuestionCategorySubcategoryService _QuestionCategorySubcategoryService;

        public QuestionCategorySubcategoryController(IQuestionCategorySubcategoryService QuestionCategorySubcategoryService)
        {
            _QuestionCategorySubcategoryService = QuestionCategorySubcategoryService;
        }

        [HttpGet]
        public ResponseResult<List<QuestionCategorySubcategoryResponseVM>> GetQuestionCategories()
        {
            var result = _QuestionCategorySubcategoryService.GetQuestionCategorySubcategories();
            if (result == null)
            {
                return new ResponseResult<List<QuestionCategorySubcategoryResponseVM>>(HttpStatusCode.NotFound, null, "Question Category Subcategory records Not Found");

            }
            return new ResponseResult<List<QuestionCategorySubcategoryResponseVM>>(HttpStatusCode.OK, result, "Question Category Subcategory records retrieved successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategoryById(int id)
        {
            var result = _QuestionCategorySubcategoryService.GetQuestionCategorySubcategoryById(id);
            if (result == null)
            {
                return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.NotFound, null, "Question Category Subcategory not found");
            }
            return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.OK, result, "Question Category Subcategory retrieved successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<QuestionCategorySubcategoryResponseVM>> GetQuestionCategoriesByTenantId(int tenantId)
        {
            var result = _QuestionCategorySubcategoryService.GetQuestionCategorySubcategoryByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<QuestionCategorySubcategoryResponseVM>>(HttpStatusCode.NotFound, null, "Question Category Subcategory records not found for the specified tenant");
            }
            return new ResponseResult<List<QuestionCategorySubcategoryResponseVM>>(HttpStatusCode.OK, result, "Question Category Subcategory records retrieved successfully for the specified tenant");
        }
        [HttpGet("id/tenantId/{id}/{tenantId}")]
        public ResponseResult<QuestionCategorySubcategoryResponseVM> GetQuestionCategorySubcategoryByIdAndTenantId(int id, int tenantId)
        {
            var result = _QuestionCategorySubcategoryService.GetQuestionCategorySubcategoryByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.NotFound, null, "Question Category Subcategory not found for the specified id and tenant");
            }
            return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.OK, result, "Question Category Subcategory retrieved successfully for the specified id and tenant");
        }
        [HttpPost]
        public ResponseResult<QuestionCategorySubcategoryResponseVM> CreateQuestionCategorySubcategory(QuestionCategorySubcategoryRequestVM request)
        {
            var result = _QuestionCategorySubcategoryService.CreateQuestionCategorySubcategory(request);
            if (result == null)
            {
                return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.BadRequest, null, "Failed to create Question Category Subcategory");
            }
            return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.OK, result, "Question Category Subcategory created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<QuestionCategorySubcategoryResponseVM> UpdateQuestionCategorySubcategory(int id, int tenantId, QuestionCategorySubcategoryUpdateVM request)
        {
            var result = _QuestionCategorySubcategoryService.UpdateQuestionCategorySubcategory(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.BadRequest, null, "Failed to update Question Category Subcategory");
            }
            return new ResponseResult<QuestionCategorySubcategoryResponseVM>(HttpStatusCode.OK, result, "Question Category Subcategory updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteQuestionCategorySubcategory(int id, int tenantId)
        {
            var result = _QuestionCategorySubcategoryService.DeleteQuestionCategorySubcategory(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.BadRequest, false, "Failed to delete Question Category Subcategory");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Question Category Subcategory deleted successfully");
        }
    }
}