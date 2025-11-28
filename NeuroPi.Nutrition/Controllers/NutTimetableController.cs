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
            [FromQuery] int date,
            [FromQuery] int tenantId,
            [FromQuery] int moduleId)
        {
            try
            {
                DateTime resolvedDate;

                switch (date)
                {
                    case 0:
                        resolvedDate = DateTime.MinValue; // ALL
                        break;

                    case -1:
                        resolvedDate = DateTime.MinValue.AddDays(1); // CURRENT WEEK FLAG
                        break;

                    default:
                        // Convert yyyyMMdd → DateTime
                        resolvedDate = DateTime.ParseExact(date.ToString(), "yyyyMMdd", null);
                        break;
                }

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
