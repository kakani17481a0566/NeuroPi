using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UnplannedMeal;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnplannedMealController : ControllerBase
    {
        private readonly IUnplannedMeal _unplannedMealService;
        public UnplannedMealController(IUnplannedMeal unplannedMealService)
        {
            _unplannedMealService = unplannedMealService;
        }

        [HttpGet]
        public ResponseResult<List<UnplannedMealResponseVM>> GetUnplannedMeals()
        {
            var result = _unplannedMealService.GetUnplannedMeals();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UnplannedMealResponseVM>>(HttpStatusCode.NotFound, null, "No unplanned meals found");
            }
            return new ResponseResult<List<UnplannedMealResponseVM>>(HttpStatusCode.OK, result, "Unplanned meals retrieved successfully");

        }

        [HttpGet("{id}")]
        public ResponseResult<UnplannedMealResponseVM> GetUnplannedMealById(int id)
        {
            var result = _unplannedMealService.GetUnplannedMealById(id);
            if (result == null)
            {
                return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.NotFound, null, "Unplanned meal not found");
            }
            return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.OK, result, "Unplanned meal retrieved successfully");
        }

        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UnplannedMealResponseVM> GetUnplannedMealByIdAndTenantId(int id, int tenantId)
        {
            var result = _unplannedMealService.GetUnplannedMealByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.NotFound, null, "Unplanned meal not found for the specified tenant");
            }
            return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.OK, result, "Unplanned meal retrieved successfully for the specified tenant");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<UnplannedMealResponseVM>> GetUnplannedMealsByTenantId(int tenantId)
        {
            var result = _unplannedMealService.GetUnplannedMealsByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<UnplannedMealResponseVM>>(HttpStatusCode.NotFound, null, "No unplanned meals found for the specified tenant");
            }
            return new ResponseResult<List<UnplannedMealResponseVM>>(HttpStatusCode.OK, result, "Unplanned meals retrieved successfully for the specified tenant");
        }

        [HttpPost]
        public ResponseResult<UnplannedMealResponseVM> CreateUnplannedMeal([FromBody] UnplannedMealRequestVM requestVM)
        {
            var result = _unplannedMealService.CreateUnplannedMeal(requestVM);
            return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.Created, result, "Unplanned meal created successfully");
        }

        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UnplannedMealResponseVM> UpdateUnplannedMeal(int id, int tenantId, [FromBody] UnplannedMealUpdateVM updateVM)
        {
            var result = _unplannedMealService.UpdateUnplannedMeal(id, tenantId, updateVM);
            if (result == null)
            {
                return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.NotFound, null, "Unplanned meal not found for update");
            }
            return new ResponseResult<UnplannedMealResponseVM>(HttpStatusCode.OK, result, "Unplanned meal updated successfully");
        }

        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteUnplannedMeal(int id, int tenantId)
        {
            var success = _unplannedMealService.DeleteUnplannedMeal(id, tenantId);
            if (!success)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Unplanned meal not found for deletion");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Unplanned meal deleted successfully");
        }


    }
}