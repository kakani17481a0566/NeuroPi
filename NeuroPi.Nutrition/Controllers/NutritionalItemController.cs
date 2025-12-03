using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;
using NeuroPi.Nutrition.ViewModel.NutritionalItem;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalItemController : ControllerBase
    {
        private readonly INutritionalItem _nutritionalItemService;
        public NutritionalItemController(INutritionalItem nutritionalItemService)
        {
            _nutritionalItemService = nutritionalItemService;
        }

        [HttpGet]
        public ResponseResult<List<NutritionalItemResponseVM>> GetNutritionalItems()
        {
            var result = _nutritionalItemService.GetNutritionalItemResponses();
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Items not found");

            }
            return new ResponseResult<List<NutritionalItemResponseVM>>(HttpStatusCode.OK, result, "Nutritional Items fetched successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<NutritionalItemResponseVM> GetNutrionalItemById(int id)
        {
            var result = _nutritionalItemService.GetNutrionalItemById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.NotFound, null, $"Nutritional Item not Found with id {id}");
            }
            return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.OK, result, $"Nutritional Item not Found id {id}");
        }

        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<NutritionalItemResponseVM> GetNutrionalItemByIdAndTenantId(int id, int tenantId)
        {
            var result = _nutritionalItemService.GetNutritionalItemByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Item not found");
            }
            return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.OK, result, "Nutrional Item details fetched sucessfully");
        }
        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<NutritionalItemResponseVM>> GetNutrionalItemByTenantId(int tenantId)
        {
            var result = _nutritionalItemService.GetNutrionalItemByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Item not found");
            }
            return new ResponseResult<List<NutritionalItemResponseVM>>(HttpStatusCode.OK, result, "Nutrional Item details fetched sucessfully");
        }

        [HttpPost]
        public ResponseResult<NutritionalItemResponseVM> CreateNutritionalItem([FromBody] NutritionalItemRequestVM item)
        {
            var result = _nutritionalItemService.CreateNutritionalItem(item);
            return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.Created, result, "Nutritional Item created successfully");
        }
        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<NutritionalItemResponseVM> UpdateNutritionalItem(int id, int tenantId, [FromBody] NutritionalItemUpdateVM item)
        {
            var result = _nutritionalItemService.UpdateNutritionalItem(id, tenantId, item);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Item not found");
            }
            return new ResponseResult<NutritionalItemResponseVM>(HttpStatusCode.OK, result, "Nutritional Item updated successfully");
        }
        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalItem(int id, int tenantId)
        {
            var result = _nutritionalItemService.DeleteNutritionalItem(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutritional Item not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutritional Item deleted successfully");
        }

        [HttpGet("/nutrition/vm/test")]
        public ResponseResult<NutritionalItemListResponseVM> GetNutrional(
    [FromQuery] int userId,
    [FromQuery] int tenantId,
    [FromQuery] DateTime? date
)
        {
            var result = _nutritionalItemService.GetAllItems(userId, tenantId, date);

            if (result == null)
            {
                return new ResponseResult<NutritionalItemListResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Nutritional Item not found"
                );
            }

            return new ResponseResult<NutritionalItemListResponseVM>(
                HttpStatusCode.OK,
                result,
                "Nutritional Item details fetched successfully"
            );
        }




        [HttpPost("save-meal-plan")]
        public ResponseResult<SaveMealPlanResponseVM> SaveMealPlan([FromBody] SaveMealPlanVM request)
        {
            var result = _nutritionalItemService.SaveMealPlan(request);

            return new ResponseResult<SaveMealPlanResponseVM>(
                HttpStatusCode.OK, result, "Meal Plan saved successfully");
        }

        [HttpPost("edit-meal-plan")]
        public ResponseResult<SaveMealPlanResponseVM> EditMealPlan([FromBody] SaveMealPlanVM request)
        {
            var result = _nutritionalItemService.EditMealPlan(request);
            return new ResponseResult<SaveMealPlanResponseVM>(
                HttpStatusCode.OK, result, "Meal Plan updated successfully");
        }

        [HttpGet("dropdowns/{tenantId}")]
        public ResponseResult<NutritionalDropdownsVM> GetDropdowns(int tenantId)
        {
            var result = _nutritionalItemService.GetDropdowns(tenantId);

            return new ResponseResult<NutritionalDropdownsVM>(
                HttpStatusCode.OK,
                result,
                "Dropdown data fetched successfully"
            );
        }





    }
}