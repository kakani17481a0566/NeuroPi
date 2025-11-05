using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionMasterType;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionMasterTypeController : ControllerBase
    {
        private readonly INutritionMasterType _nutritionMasterTypeService;

        public NutritionMasterTypeController(INutritionMasterType nutritionMasterTypeService)
        {
            _nutritionMasterTypeService = nutritionMasterTypeService;
        }

        [HttpGet("GetAllNutritionMasterTypes")]
        public ResponseResult<List<NutritionMasterTypeResponseVM>> GetAllNutritionMasterTypes()
        {
            var result = _nutritionMasterTypeService.GetAllNutritionMasterTypes();
            if (result == null)
            {
                return new ResponseResult<List<NutritionMasterTypeResponseVM>>(HttpStatusCode.NotFound, null, "No Nutrition Master Types found");
            }
            return new ResponseResult<List<NutritionMasterTypeResponseVM>>(HttpStatusCode.OK, result, "Nutrition Master Types fetched successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<NutritionMasterTypeResponseVM>> GetAllNutritionMasterTypeByTenantId(int tenantId)
        {
            var result = _nutritionMasterTypeService.GetAllNutritionMasterTypeByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionMasterTypeResponseVM>>(HttpStatusCode.NotFound, null, "No Nutrition Master Types found for the given tenant");
            }
            return new ResponseResult<List<NutritionMasterTypeResponseVM>>(HttpStatusCode.OK, result, "Nutrition Master Types fetched successfully for the tenant");
        }
        [HttpGet("{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypeByIdAndTenantId(int id, int tenantId)
        {
            var result = _nutritionMasterTypeService.GetAllNutritionMasterTypeByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master Type not found for the given id and tenant");
            }
            return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.OK, result, "Nutrition Master Type fetched successfully for the given id and tenant");

        }
        [HttpGet("{id}")]
        public ResponseResult<NutritionMasterTypeResponseVM> GetAllNutritionMasterTypeById(int id)
        {
            var result = _nutritionMasterTypeService.GetAllNutritionMasterTypeById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master Type not found for the given id");
            }
            return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.OK, result, "Nutrition Master Type fetched successfully for the given id");
        }
        [HttpPost]
        public ResponseResult<NutritionMasterTypeResponseVM> CreateNutritionMasterType([FromBody] NutritionMasterTypeRequestVM requestVM)
        {
            var result = _nutritionMasterTypeService.CreateNutritionMasterType(requestVM);
            return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.Created, result, "Nutrition Master Type created successfully");
        }
        [HttpPut("{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionMasterTypeResponseVM> UpdateNutritionMasterType(int id, int tenantId, [FromBody] NutritionMasterTypeUpdateVM requestVM)
        {
            var result = _nutritionMasterTypeService.UpdateNutritionMasterType(id, tenantId, requestVM);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master Type not found for the given id and tenant");
            }
            return new ResponseResult<NutritionMasterTypeResponseVM>(HttpStatusCode.OK, result, "Nutrition Master Type updated successfully");
        }

        [HttpDelete("{id}/tenant/{tenantId}")]
        public ResponseResult<object> DeleteNutritionMasterType(int id, int tenantId)
        {
            var success = _nutritionMasterTypeService.DeleteNutritionMasterType(id, tenantId);
            if (!success)
            {
                return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Nutrition Master Type deletion failed or not found");
            }
            return new ResponseResult<object>(HttpStatusCode.OK, null, "Nutrition Master Type deleted successfully");
        }

    }
}