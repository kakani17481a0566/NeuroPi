 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalItemVitamins;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalItemVitaminsController : ControllerBase
    {
        private readonly INutritionalItemVitamins _nutritionalItemVitaminsService;
        public NutritionalItemVitaminsController(INutritionalItemVitamins nutritionalItemVitaminsService)
        {
            _nutritionalItemVitaminsService = nutritionalItemVitaminsService;
        }

        [HttpGet]
        public ResponseResult<List<NutritionalItemVitaminsResponseVM>> GetAll()
        {
            var result = _nutritionalItemVitaminsService.GetNutritionalItemVitamins();
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemVitaminsResponseVM>>(HttpStatusCode.NotFound, null, "No Nutritional Item Vitamins found.");

            }
            return new ResponseResult<List<NutritionalItemVitaminsResponseVM>>(HttpStatusCode.OK, result, "Nutritional Item Vitamins fetched successfully.");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<NutritionalItemVitaminsResponseVM> GetById(int id)
        {
            var result = _nutritionalItemVitaminsService.GetNutritionalItemVitaminsById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Item Vitamins not found.");
            }
            return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.OK, result, "Nutritional Item Vitamins fetched successfully.");

        }
        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionalItemVitaminsResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var result = _nutritionalItemVitaminsService.GetNutritionalItemVitaminsByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Item Vitamins not found for the given tenant.");
            }
            return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.OK, result, "Nutritional Item Vitamins fetched successfully for the given tenant.");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<NutritionalItemVitaminsResponseVM>> GetByTenantId(int tenantId)
        {
            var result = _nutritionalItemVitaminsService.GetNutritionalVitaminByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemVitaminsResponseVM>>(HttpStatusCode.NotFound, null, "No Nutritional Item Vitamins found for the given tenant.");
            }
            return new ResponseResult<List<NutritionalItemVitaminsResponseVM>>(HttpStatusCode.OK, result, "Nutritional Item Vitamins fetched successfully for the given tenant.");
        }
        [HttpPost]
        public ResponseResult<NutritionalItemVitaminsResponseVM> Create([FromBody] NutritionalItemVitaminsRequestVM request)
        {
            var result = _nutritionalItemVitaminsService.CreateNutritionalItemVitamins(request);
            return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.Created, result, "Nutritional Item Vitamins created successfully.");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<NutritionalItemVitaminsResponseVM> Update(int id, int tenantId, [FromBody] NutritionalItemVitaminsUpdateVM request)
        {
            var result = _nutritionalItemVitaminsService.UpdateNutritionalItemVitamins(id, tenantId, request);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Item Vitamins not found for update.");
            }
            return new ResponseResult<NutritionalItemVitaminsResponseVM>(HttpStatusCode.OK, result, "Nutritional Item Vitamins updated successfully.");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var success = _nutritionalItemVitaminsService.DeleteNutritionalItemVitamins(id, tenantId);
            return new ResponseResult<bool>(
                success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                success,
                success ? "Nutritional Item Vitamins deleted successfully." : "Nutritional Item Vitamins deletion failed."
            );
        }

    }
}