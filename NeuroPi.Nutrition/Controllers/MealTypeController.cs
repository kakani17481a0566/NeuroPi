using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.MealType;
using System.Net;
using System.Security.Cryptography.Xml;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealTypeController : ControllerBase
    {
        private readonly IMealType _mealTypeService;
        public MealTypeController(IMealType mealTypeService)
        {
            _mealTypeService = mealTypeService;
        }

        [HttpGet]
        public ResponseResult<List<MealTypeResponseVM>> get()
        {
            var mealTypes = _mealTypeService.GetAllMealTypes();
            if (mealTypes == null || mealTypes.Count == 0)
            {
                return new ResponseResult<List<MealTypeResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No meal types found.");
            }
            return new ResponseResult<List<MealTypeResponseVM>>(
                HttpStatusCode.OK,
                mealTypes,
                "Meal types fetched successfully.");

        }
        [HttpGet("{id}")]
        public ResponseResult<MealTypeResponseVM> getById(int id)
        {
            var mealType = _mealTypeService.GetMealTypeById(id);
            if (mealType == null)
            {
                return new ResponseResult<MealTypeResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Meal type not found for the given id.");
            }
            return new ResponseResult<MealTypeResponseVM>(
                HttpStatusCode.OK,
                mealType,
                "Meal type fetched successfully.");

        }

        [HttpGet("tennatId/{tenantId}")]
        public ResponseResult<List<MealTypeResponseVM>> GetMealTypeByTenantId(int tenantId)
        {
            var mealType = _mealTypeService.GetMealTypeByTenantId(tenantId);
            if (mealType == null)
            {
                return new ResponseResult<List<MealTypeResponseVM>>(HttpStatusCode.NotFound, null, "Meal Type Not Found");

            }
            return new ResponseResult<List<MealTypeResponseVM>>(HttpStatusCode.OK, mealType, "Meal Types fetched successfully");
        }
        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<MealTypeResponseVM> GetMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var mealType = _mealTypeService.GetMealTypeByIdAndTenantId(id, tenantId);
            if (mealType == null)
            {
                return new ResponseResult<MealTypeResponseVM>(HttpStatusCode.NotFound, null, "Meal Type Not Found");
            }
            return new ResponseResult<MealTypeResponseVM>(HttpStatusCode.OK, mealType, "Meal Type fetched successfully");
        }

        [HttpPost]
        public ResponseResult<MealTypeResponseVM> CreateMealType([FromBody] MealTypeRequestVM mealTypeRequestVM)
        {
            var createdMealType = _mealTypeService.CreateMealType(mealTypeRequestVM);
            return new ResponseResult<MealTypeResponseVM>(
                HttpStatusCode.Created,
                createdMealType,
                "Meal type created successfully.");
        }
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<MealTypeResponseVM> UpdateMealType(int id, int tenantId, [FromBody] MealTypeUpdateVM mealTypeUpdateVM)
        {
            var updatedMealType = _mealTypeService.UpdateMealType(id, tenantId, mealTypeUpdateVM);
            if (updatedMealType == null)
            {
                return new ResponseResult<MealTypeResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Meal type not found for the given id and tenantId.");
            }
            return new ResponseResult<MealTypeResponseVM>(
                HttpStatusCode.OK,
                updatedMealType,
                "Meal type updated successfully.");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteMealType(int id, int tenantId)
        {
            var isDeleted = _mealTypeService.DeleteMealType(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "Meal type not found for the given id and tenantId.");
            }
            return new ResponseResult<bool>(
                HttpStatusCode.OK,
                true,
                "Meal type deleted successfully.");
        }


    }
}