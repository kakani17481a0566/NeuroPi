using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserGamesStatus;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGamesStatusesController : ControllerBase
    {
        private readonly IUserGamesStatuses _service;

        public UserGamesStatusesController(IUserGamesStatuses service)
        {
            _service = service;
        }

        // =========================================================
        //  SAVE (INSERT / UPDATE)
        // =========================================================
        [HttpPost("Save")]
        public ResponseResult<UserGameStatusVM> Save([FromBody] SaveUserGameStatusVM model)
        {
            try
            {
                var data = _service.SaveUserGameStatus(model);

                return new ResponseResult<UserGameStatusVM>(
                    HttpStatusCode.OK,
                    data,
                    "Status saved successfully"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<UserGameStatusVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    "Error: " + ex.Message
                );
            }
        }

        // =========================================================
        //  GET STATUS FOR USER + TIMETABLE
        // =========================================================
        [HttpGet("Get")]
        public ResponseResult<UserGameStatusVM?> Get(
            [FromQuery] int userId,
            [FromQuery] int timetableId,
            [FromQuery] int tenantId)
        {
            try
            {
                var data = _service.GetUserGameStatus(userId, timetableId, tenantId);

                return new ResponseResult<UserGameStatusVM?>(
                    HttpStatusCode.OK,
                    data,
                    "Status fetched successfully"
                );
            }
            catch (Exception ex)
            {
                return new ResponseResult<UserGameStatusVM?>(
                    HttpStatusCode.InternalServerError,
                    null,
                    "Error: " + ex.Message
                );
            }
        }
    }
}
