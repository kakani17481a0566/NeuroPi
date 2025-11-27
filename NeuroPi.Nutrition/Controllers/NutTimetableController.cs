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
          [FromQuery] DateTime date,
          [FromQuery] int tenantId,
          [FromQuery] int moduleId)
        {
            try
            {
                var data = _timetableService.GetTimetable(userId, date, tenantId, moduleId);

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
