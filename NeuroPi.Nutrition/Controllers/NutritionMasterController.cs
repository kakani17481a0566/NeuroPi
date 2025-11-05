using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionMaster;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class NutritionMasterController : ControllerBase
    {
        private readonly INutritionMaster _nutritionMasterService;

        public NutritionMasterController(INutritionMaster nutritionMasterService)
        {
            _nutritionMasterService = nutritionMasterService;
        }

        [HttpGet]
        public ResponseResult<List<NutritionMasterResponseVM>> GetNutritionMasters()
        {
            var result = _nutritionMasterService.GetNutritionMaster();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<NutritionMasterResponseVM>>(HttpStatusCode.NotFound, null, "Nutrition Master data not found.");

            }
            return new ResponseResult<List<NutritionMasterResponseVM>>(HttpStatusCode.OK, result, "Nutrition Master data fetched successfully.");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<NutritionMasterResponseVM> GetNutritionMasterById(int id)
        {
            var result = _nutritionMasterService.GetNutritionMasterById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master data not found.");
            }
            return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.OK, result, "Nutrition Master data fetched successfully.");

        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<NutritionMasterResponseVM>> GetNutritionMastersByTenantId(int tenantId)
        {
            var result = _nutritionMasterService.GetNutritionMasterByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<NutritionMasterResponseVM>>(HttpStatusCode.NotFound, null, "Nutrition Master data not found for the specified tenant.");
            }
            return new ResponseResult<List<NutritionMasterResponseVM>>(HttpStatusCode.OK, result, "Nutrition Master data fetched successfully for the specified tenant.");
        }

        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionMasterResponseVM> GetNutritionMasterByIdAndTenantId(int id, int tenantId)
        {
            var result = _nutritionMasterService.GetNutritionMasterByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master data not found for the specified id and tenant.");
            }
            return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.OK, result, "Nutrition Master data fetched successfully for the specified id and tenant.");
        }

        [HttpPost]
        public ResponseResult<NutritionMasterResponseVM> CreateNutritionMaster([FromBody] NutritionMasterRequestVM request)
        {
            if (request == null)
            {
                return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request data.");
            }
            var result = _nutritionMasterService.CreateNutritionMaster(request);
            return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.OK, result, "Nutrition Master data created successfully.");
        }

        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionMasterResponseVM> UpdateNutritionMaster(int id, int tenantId, [FromBody] NutritionMasterUpdateVM request)
        {

            var result = _nutritionMasterService.UpdateNutritionMaster(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.NotFound, null, "Nutrition Master data not found for update.");
            }
            return new ResponseResult<NutritionMasterResponseVM>(HttpStatusCode.OK, result, "Nutrition Master data updated successfully.");
        }
        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionMaster(int id, int tenantId)
        {
            var success = _nutritionMasterService.DeleteNutritionMaster(id, tenantId);
            if (success)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutrition Master data deleted successfully.");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutrition Master data not found for deletion.");
        }
    }
}