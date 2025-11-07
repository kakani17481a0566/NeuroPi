using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Implementation;
using NeuroPi.Nutrition.ViewModel.NutritionalItemMealType;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalItemMealTypeController : ControllerBase
    {
        private readonly NutritionalItemMealTypeServiceImpl _service;

        public NutritionalItemMealTypeController(NutritionalItemMealTypeServiceImpl service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<NutritionalItemMealTypeResponseVM>> GetNutritionItemMealType()
        {
            var result = _service.GetNutritionalItemMealType();
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemMealTypeResponseVM>>(HttpStatusCode.NotFound,null,"Nutrition iteam meal type not found");
            }
            return new ResponseResult<List<NutritionalItemMealTypeResponseVM>>(HttpStatusCode.OK, result, "Nutrition iteam meal deatils fetched successfully");
        }
        [HttpGet("id/{id}")]
        public ResponseResult<NutritionalItemMealTypeResponseVM> GetNutritionItemMealTypeById(int id)
        {
            var result = _service.GetNutritionalItemMealTypeById(id);
            if (result == null) 
            {
                return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition iteam meal type not found");
            }
            return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.OK, result, "Nutrition iteam meal deatils fetched successfully");
        }
        [HttpGet("id{id}/tenantId/{tenantId}")]
        public ResponseResult<NutritionalItemMealTypeResponseVM> GetNutritionItemMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var result = _service.GetNutritionalItemMealTypeByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition iteam meal type not found");
            }
            return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.OK, result, "Nutrition iteam meal deatils fetched successfully");
        }

        [HttpGet("tenantId/{tenantId}")]

        public ResponseResult<List<NutritionalItemMealTypeResponseVM>> GetNutritionItemMealTypeByTenantId(int tenantId)
        {
            var result = _service.GetNutritionalItemMealTypeByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemMealTypeResponseVM>>(HttpStatusCode.NotFound, null, "Nutrition item meal type not found");
            }
            return new ResponseResult<List<NutritionalItemMealTypeResponseVM>>(HttpStatusCode.OK, result, "Nutrition iteam meal deatils fetched successfully");
        }

        [HttpPost]
        public ResponseResult<NutritionalItemMealTypeResponseVM> CreateNutritionItemMealType([FromBody] NutritionalItemMealTypeRequestVM vm)
        {
            var result = _service.CreateNutrionalItemMealType(vm);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.BadRequest, null, "Unable to create nutrition item meal type");
            }
            return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.OK, result, "New nutrition item meal type created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<NutritionalItemMealTypeResponseVM> updateNutritionalItemMealType(int id, int tenantId, [FromBody] NutritionalItemMealTypeUpdateVM vm)
        {
            var result = _service.UpdateNutritionalMealType(id, tenantId, vm);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition item meal type not found");
            }
            return new ResponseResult<NutritionalItemMealTypeResponseVM>(HttpStatusCode.OK, result, " nutrition item meal type updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalItemMealType(int id, int tenantId)
        {
            var result = _service.DeleteNutritionalItemMealType(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutrition item meal type not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, true, "Nutrition item meal type deleted successfully");
        }

    }
}
