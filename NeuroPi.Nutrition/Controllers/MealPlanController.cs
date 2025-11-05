using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanController : ControllerBase
    {

        private readonly IMealPlan _mealPlanService;

        public MealPlanController(IMealPlan mealPlanService)
        {
            _mealPlanService = mealPlanService;
        }


        [HttpGet("GetAllMeanl")]
        public ResponseResult<List<MealPlanResponseVM>> GetAllMeanl()
        {
            var response = _mealPlanService.GetAllMealPlan();
            if (response != null)
            {
                return new ResponseResult<List<MealPlanResponseVM>>(HttpStatusCode.OK, response, "Meal Plans Retrived Successfully");
            }
            return new ResponseResult<List<MealPlanResponseVM>>(HttpStatusCode.NotFound, null, "No Meal Plans Found");


        }


        [HttpGet("GetMealPlanById/{id}")]
        public ResponseResult<MealPlanResponseVM> GetMealPlanById(int id)
        {
            var response = _mealPlanService.GetMealPlanById(id);
            if (response != null)
            {
                return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.OK, response, "Meal Plan Retrived Successfully");
            }
            return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.NotFound, null, "No Meal Plan Found");
        }


        [HttpGet("MealPlanByIdByTenantId/{id}/{tenantid}")]
        public ResponseResult<MealPlanResponseVM> GetMealPlanByTenantId(int id, int tenantid)
        {
            var response = _mealPlanService.GetMealPlanByIdTenantId(id, tenantid);
            if (response != null)
            {
                return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.OK, response, "Meal Plan Retrived Successfully");

            }
            return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.NotFound, null, "No Meal Plan Found");

        }

        [HttpGet("GetMealPlanByTenantId/{tenantid}")]
        public ResponseResult<List<MealPlanResponseVM>> GetMealPlanByTenantId(int tenantid)
        {
            var response = _mealPlanService.GetMealPlanByTenantId(tenantid);
            if (response != null)
            {
                return new ResponseResult<List<MealPlanResponseVM>>(HttpStatusCode.OK, response, "Meal Plans Retrived Successfully");
            }
            return new ResponseResult<List<MealPlanResponseVM>>(HttpStatusCode.NotFound, null, "No Meal Plans Found");
        }





        [HttpPost("CreateMealPlan")]
        public ResponseResult<MealPlanResponseVM> CreateMealPlan([FromBody] MealPlanRequestVM mealplanrequestvm)
        {
            var response = _mealPlanService.CreateMealPlan(mealplanrequestvm);
            if (response != null)
            {
                return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.OK, response, "Meal Plan Created Successfully");
            }
            return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.NotFound, null, "Meal Plan Not Created");
        }


        [HttpPut("UpdateMealPlan/{id}/{tenantid}")]
        public ResponseResult<MealPlanResponseVM> UpdateMealPlan(int id, int tenantid, [FromBody] MealPlanUpdateVM mealplanrequestvm)
        {
            var response = _mealPlanService.UpdateMealPlan(id, tenantid, mealplanrequestvm);
            if (response != null)
            {
                return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.OK, response, "Meal Plan Updated Successfully");
            }
            return new ResponseResult<MealPlanResponseVM>(HttpStatusCode.NotFound, null, "Meal Plan Not Updated");

        }


     


    
        [HttpDelete("DeleteMealPlan/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteMealPlan(int id, int tenantId)
        {
            var result = _mealPlanService.DeleteMealPlan(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Meal Plan Not Found or could not be deleted");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Meal Plan  deleted successfully");
        }

    }
}
