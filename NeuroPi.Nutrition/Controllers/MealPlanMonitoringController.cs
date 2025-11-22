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

        // --------------------------------------------------------------------
        // CRUD — Existing Endpoints
        // --------------------------------------------------------------------
        [HttpGet]
        public ResponseResult<List<MealPlanMonitoringResponseVM>> GetAll()
        {
            var result = _service.GetAllMealPlanMonitoring();
            return new ResponseResult<List<MealPlanMonitoringResponseVM>>(
                HttpStatusCode.OK, result, "Meal Plan Monitoring Loaded");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<MealPlanMonitoringResponseVM> GetById(int id)
        {
            var result = _service.GetMealPlanMonitoringById(id);
            if (result == null)
                return new ResponseResult<MealPlanMonitoringResponseVM>(
                    HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");

            return new ResponseResult<MealPlanMonitoringResponseVM>(
                HttpStatusCode.OK, result, "Meal Plan Monitoring Loaded");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<MealPlanMonitoringResponseVM>> GetByTenant(int tenantId)
        {
            var result = _service.GetAllMealPlanMonitoringByTenantId(tenantId);
            return new ResponseResult<List<MealPlanMonitoringResponseVM>>(
                HttpStatusCode.OK, result, "Meal Plan Monitoring Loaded");
        }

        [HttpPost]
        public ResponseResult<MealPlanMonitoringResponseVM> Create(MealPlanMonitoringRequestVM request)
        {
            var result = _service.CreateMealPlanMonitoring(request);
            return new ResponseResult<MealPlanMonitoringResponseVM>(
                HttpStatusCode.OK, result, "Meal Plan Monitoring Created");
        }

        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<MealPlanMonitoringResponseVM> Update(
            int id, int tenantId, MealPlanMonitoringUpdateVM updateVM)
        {
            var result = _service.UpdateMealPlanMonitoring(id, tenantId, updateVM);

            if (result == null)
                return new ResponseResult<MealPlanMonitoringResponseVM>(
                    HttpStatusCode.NotFound, null, "Meal Plan Monitoring Not Found");

            return new ResponseResult<MealPlanMonitoringResponseVM>(
                HttpStatusCode.OK, result, "Meal Plan Monitoring Updated");
        }

        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var result = _service.DeleteMealPlanMonitoring(id, tenantId);

            return new ResponseResult<bool>(
                HttpStatusCode.OK, result, result ? "Deleted successfully" : "Delete failed");
        }

        // --------------------------------------------------------------------
        // 7 days view
        // --------------------------------------------------------------------
        [HttpGet("7days/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<MealPlan7daysCardVM> Get7DaysMealPlanCard(int userId, int tenantId)
        {
            var result = _service.Get7DaysMealPlanCard(userId, tenantId);
            if (result == null)
                return new ResponseResult<MealPlan7daysCardVM>(
                    HttpStatusCode.NotFound, null, "No data found for this week");

            return new ResponseResult<MealPlan7daysCardVM>(
                HttpStatusCode.OK, result, "7 Days Meal Card Loaded");
        }

        // --------------------------------------------------------------------
        // Meal Monitoring View (today or selected date)
        // --------------------------------------------------------------------
        [HttpGet("monitor/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<MealPlanMonitoringResponseViewVM> GetMealMonitoring(
            int userId,
            int tenantId,
            [FromQuery] string? date = null)
        {
            DateOnly selectedDate;

            if (string.IsNullOrWhiteSpace(date))
                selectedDate = DateOnly.FromDateTime(DateTime.Today);
            else if (!DateOnly.TryParse(date, out selectedDate))
                return new ResponseResult<MealPlanMonitoringResponseViewVM>(
                    HttpStatusCode.BadRequest, null, "Invalid date format (use yyyy-MM-dd)");

            var result = _service.GetMealMonitoring(userId, tenantId, selectedDate);

            return new ResponseResult<MealPlanMonitoringResponseViewVM>(
                HttpStatusCode.OK, result, "Meal Monitoring Loaded");
        }

        // --------------------------------------------------------------------
        // ⭐ NEW ENDPOINT — SAVE MEAL TRACKING (Pending or Today)
        // --------------------------------------------------------------------
        // POST /api/MealPlanMonitoring/track/user/1/tenant/1
        //
        // BODY:
        // [
        //   { "date":"2025-02-18", "mealTypeId":1, "nutritionalItemId": 5,
        //     "plannedQty":1, "consumedQty":2 },
        //   { "date":"2025-02-18", "mealTypeId":1, "nutritionalItemId": 0,
        //     "otherName":"Dosa", "otherCaloriesQuantity":120, "consumedQty":1 }
        // ]
        // --------------------------------------------------------------------
        [HttpPost("track/user/{userId}/tenant/{tenantId}")]
        public ResponseResult<SaveMealsTrackingResponseVM> SaveMealsTracking(
            int userId,
            int tenantId,
            [FromBody] List<SavePendingMeals> items)
        {
            if (items == null || items.Count == 0)
            {
                return new ResponseResult<SaveMealsTrackingResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "No meal tracking data received");
            }

            var result = _service.SaveMealsTracking(userId, tenantId, items);

            return new ResponseResult<SaveMealsTrackingResponseVM>(
                HttpStatusCode.OK, result, "Meal tracking saved successfully");
        }
    }
}
