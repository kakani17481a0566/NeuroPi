using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.MealPlanMonitoring;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealPlanMonitoringController : ControllerBase
    {
        private readonly IMealPlanMonitoring _service;

        public MealPlanMonitoringController(IMealPlanMonitoring service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<MealPlanMonitoringResponseVM>> Getall()
        {
            var result = _service.GetAllMealPlanMonitoring();
            if (result == null)
            {
                return new ResponseResult<List<MealPlanMonitoringResponseVM>>(HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");
            }
            return new ResponseResult<List<MealPlanMonitoringResponseVM>>(HttpStatusCode.OK, result, "Meal Plan Monitoring Not Found");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<MealPlanMonitoringResponseVM> GetMealPlanMonitotingById(int id)
        {
            var result = _service.GetMealPlanMonitoringById(id);
            if (result == null)
            {
                return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");
            }
            return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.OK, result, "Meal Plan Monitoring Not Found");

        }
        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<MealPlanMonitoringResponseVM> GetMealPlanMonitotingById(int id, int tenantId)
        {
            var result = _service.GetMealPlanMonitoringById(id);
            if (result == null)
            {
                return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");
            }
            return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.OK, result, "Meal Plan Monitoring Not Found");

        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<MealPlanMonitoringResponseVM>> GetallByTenantId(int tenantId)
        {
            var result = _service.GetAllMealPlanMonitoringByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<MealPlanMonitoringResponseVM>>(HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");
            }
            return new ResponseResult<List<MealPlanMonitoringResponseVM>>(HttpStatusCode.OK, result, "Meal Plan Monitoring Not Found");
        }

        [HttpPost]
        public ResponseResult<MealPlanMonitoringResponseVM> CreateMealPlanMonitoring(MealPlanMonitoringRequestVM request)
        {
            var result = _service.CreateMealPlanMonitoring(request);

            return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.OK, result, "Meal Plan Monitoring Created.");

        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<MealPlanMonitoringResponseVM> UpdateMealPlanMonitoring(int id, int tenantId, MealPlanMonitoringUpdateVM updateVM)
        {
            var result = _service.UpdateMealPlanMonitoring(id, tenantId, updateVM);
            if (result == null)
            {
                return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.NotFound, null, "Meal plan monitoring not found");
            }
            return new ResponseResult<MealPlanMonitoringResponseVM>(HttpStatusCode.OK, result, "Meal Plan Monitoring Updated.");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteMealPlanMonitoring(int id, int tenantId)
        {
            var result = _service.DeleteMealPlanMonitoring(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Meal plan monitoring not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Meal Plan Monitoring deleted.");

        }


        [HttpGet("7days/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<MealPlan7daysCardVM> Get7DaysMealPlanCard(int userId, int tenantId)
        {
            var result = _service.Get7DaysMealPlanCard(userId, tenantId);

            if (result == null)
            {
                return new ResponseResult<MealPlan7daysCardVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "No meal plan data found for this week."
                );
            }

            return new ResponseResult<MealPlan7daysCardVM>(
                HttpStatusCode.OK,
                result,
                "7 days meal plan card loaded successfully."
            );
        }


        // -------------------------------------------------------------------
        // ⭐ NEW ENDPOINT — Get Meal Monitoring
        // Date is optional. If not passed, backend uses today's date.
        // Example:
        // GET /api/MealPlanMonitoring/monitor/user/1/tenant/1
        // GET /api/MealPlanMonitoring/monitor/user/1/tenant/1/date/2025-02-19
        // -------------------------------------------------------------------
        [HttpGet("monitor/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<MealPlanMonitoringResponseViewVM> GetMealMonitoring(
            int userId,
            int tenantId,
            [FromQuery] string? date = null)
        {
            // If no date provided → use today
            DateOnly selectedDate;

            if (string.IsNullOrWhiteSpace(date))
            {
                selectedDate = DateOnly.FromDateTime(DateTime.Today);
            }
            else
            {
                if (!DateOnly.TryParse(date, out selectedDate))
                {
                    return new ResponseResult<MealPlanMonitoringResponseViewVM>(
                        HttpStatusCode.BadRequest,
                        null,
                        "Invalid date format. Use yyyy-MM-dd."
                    );
                }
            }

            var result = _service.GetMealMonitoring(userId, tenantId, selectedDate);

            if (result == null)
            {
                return new ResponseResult<MealPlanMonitoringResponseViewVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Meal monitoring data not found."
                );
            }

            return new ResponseResult<MealPlanMonitoringResponseViewVM>(
                HttpStatusCode.OK,
                result,
                "Meal monitoring loaded successfully."
            );
        }




    }

}
