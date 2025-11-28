using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.TimeTable;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutTimetableController : ControllerBase
    {
        private readonly ITimetable _timetableService;

        public NutTimetableController(ITimetable timetableService)
        {
            _timetableService = timetableService;
        }

        [HttpGet]
        public ResponseResult<List<TimetableVM>> GetTimetable(
            [FromQuery] int userId,
            [FromQuery] int date,        // ⭐ CHANGED from DateTime to int
            [FromQuery] int tenantId,
            [FromQuery] int moduleId)
        {
            try
            {
                // ---------------------------------------
                // ⭐ RESOLVE DATE FLAG
                // ---------------------------------------
                DateTime resolvedDate;

                if (date == 0)
                {
                    resolvedDate = DateTime.MinValue; // ALL
                }
                else if (date == -1)
                {
                    resolvedDate = DateTime.MinValue.AddDays(1); // CURRENT WEEK
                }
                else
                {
                    // Normal date (yyyyMMdd int converted to string)
                    string dateStr = date.ToString();
                    resolvedDate = DateTime.ParseExact(dateStr, "yyyyMMdd", null);
                }

                // ---------------------------------------
                // ⭐ CALL SERVICE
                // ---------------------------------------
                var data = _timetableService.GetTimetable(userId, resolvedDate, tenantId, moduleId);

                return new ResponseResult<List<TimetableVM>>(
                    HttpStatusCode.OK,
                    data,
                    "Timetable fetched successfully"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<TimetableVM>>(
                    HttpStatusCode.InternalServerError,
                    null,
                    $"Error: {ex.Message}"
                );
            }
        }
    }
}
