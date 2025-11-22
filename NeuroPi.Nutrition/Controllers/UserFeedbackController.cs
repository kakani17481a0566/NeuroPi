using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserFeedback;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFeedbackController : ControllerBase
    {
        private readonly IUserFeedback _userFeedbackService;

        public UserFeedbackController(IUserFeedback userFeedbackService)
        {
            _userFeedbackService = userFeedbackService;
        }

        // -------------------------------------------------------------
        // GET ALL
        // -------------------------------------------------------------
        [HttpGet]
        public ResponseResult<List<UserFeedbackResponseVM>> GetAllUserFeedbacks()
        {
            var result = _userFeedbackService.GetAllUserFeedbacks();

            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserFeedbackResponseVM>>(
                    HttpStatusCode.NotFound,
                    new List<UserFeedbackResponseVM>(),
                    "No user feedback found."
                );
            }

            return new ResponseResult<List<UserFeedbackResponseVM>>(
                HttpStatusCode.OK,
                result,
                "User feedback fetched successfully."
            );
        }

        // -------------------------------------------------------------
        // GET BY ID
        // -------------------------------------------------------------
        [HttpGet("id/{id}")]
        public ResponseResult<UserFeedbackResponseVM> GetUserFeedbackById(int id)
        {
            var result = _userFeedbackService.GetUserFeedbackById(id);

            if (result == null)
            {
                return new ResponseResult<UserFeedbackResponseVM>(
                    HttpStatusCode.NotFound,
                    new UserFeedbackResponseVM(),
                    "User feedback not found."
                );
            }

            return new ResponseResult<UserFeedbackResponseVM>(
                HttpStatusCode.OK,
                result,
                "User feedback fetched successfully."
            );
        }

        // -------------------------------------------------------------
        // GET BY TENANT
        // -------------------------------------------------------------
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<UserFeedbackResponseVM>> GetUserFeedbacksByTenantId(int tenantId)
        {
            var result = _userFeedbackService.GetUserFeedbacksByTenantId(tenantId);

            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserFeedbackResponseVM>>(
                    HttpStatusCode.NotFound,
                    new List<UserFeedbackResponseVM>(),
                    "No feedback found for this tenant."
                );
            }

            return new ResponseResult<List<UserFeedbackResponseVM>>(
                HttpStatusCode.OK,
                result,
                "User feedback fetched successfully."
            );
        }

        // -------------------------------------------------------------
        // GET BY ID + TENANT
        // -------------------------------------------------------------
        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserFeedbackResponseVM> GetUserFeedbackByIdAndTenantId(int id, int tenantId)
        {
            var result = _userFeedbackService.GetUserFeedbackByIdAndTenantId(id, tenantId);

            if (result == null)
            {
                return new ResponseResult<UserFeedbackResponseVM>(
                    HttpStatusCode.NotFound,
                    new UserFeedbackResponseVM(),
                    "User feedback not found for this tenant and id."
                );
            }

            return new ResponseResult<UserFeedbackResponseVM>(
                HttpStatusCode.OK,
                result,
                "User feedback fetched successfully."
            );
        }

        // -------------------------------------------------------------
        // CREATE
        // -------------------------------------------------------------
        [HttpPost]
        public ResponseResult<UserFeedbackResponseVM> CreateUserFeedback([FromBody] UserFeedbackRequestVM vm)
        {
            if (vm == null)
            {
                return new ResponseResult<UserFeedbackResponseVM>(
                    HttpStatusCode.BadRequest,
                    new UserFeedbackResponseVM(),
                    "Invalid request body."
                );
            }

            var result = _userFeedbackService.CreateUserFeedback(vm);

            return new ResponseResult<UserFeedbackResponseVM>(
                HttpStatusCode.OK,
                result,
                "User feedback created successfully."
            );
        }

        // -------------------------------------------------------------
        // UPDATE
        // -------------------------------------------------------------
        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserFeedbackResponseVM> UpdateUserFeedback(int id, int tenantId, [FromBody] UserFeedbackUpdateVM vm)
        {
            var result = _userFeedbackService.UpdateUserFeedback(id, tenantId, vm);

            if (result == null)
            {
                return new ResponseResult<UserFeedbackResponseVM>(
                    HttpStatusCode.NotFound,
                    new UserFeedbackResponseVM(),
                    "User feedback not found."
                );
            }

            return new ResponseResult<UserFeedbackResponseVM>(
                HttpStatusCode.OK,
                result,
                "User feedback updated successfully."
            );
        }

        // -------------------------------------------------------------
        // DELETE
        // -------------------------------------------------------------
        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteUserFeedback(int id, int tenantId)
        {
            var deleted = _userFeedbackService.DeleteUserFeedback(id, tenantId);

            if (!deleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "User feedback not found."
                );
            }

            return new ResponseResult<bool>(
                HttpStatusCode.OK,
                true,
                "User feedback deleted successfully."
            );
        }
    }
}
