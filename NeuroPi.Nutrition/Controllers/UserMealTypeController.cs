using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserMealType;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMealTypeController : ControllerBase
    {
        private readonly IUserMealType _userMealTypeService;
        public UserMealTypeController(IUserMealType userMealTypeService)
        {
            _userMealTypeService = userMealTypeService;
        }

        [HttpGet]
        public ResponseResult<List<UserMealTypeResponseVM>> GetAllUserMealTypes()
        {
            var result = _userMealTypeService.GetAllUserMealTypes();
            if (result == null)
            {
                return new ResponseResult<List<UserMealTypeResponseVM>>(HttpStatusCode.NotFound, null, "No User Meal Types found");

            }
            return new ResponseResult<List<UserMealTypeResponseVM>>(HttpStatusCode.OK, result, "User Meal Types fetched successfully");
        }

        [HttpGet("id/{id}")]
        public ResponseResult<UserMealTypeResponseVM> GetUserMealTypeById(int id)
        {
            var result = _userMealTypeService.GetUserMealTypeById(id);
            if (result == null)
            {
                return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.NotFound, null, "User Meal Type not found");
            }
            return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.OK, result, "User Meal Type fetched successfully");

        }

        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<UserMealTypeResponseVM> GetUserMealTypeByIdAndTenantId(int id, int tenantId)
        {
            var result = _userMealTypeService.GetUserMealTypeByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.NotFound, null, "User Meal Type not found");
            }
            return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.OK, result, "User Meal Type fetched successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<UserMealTypeResponseVM>> GetUserMealTypesByTenantId(int tenantId)
        {
            var result = _userMealTypeService.GetUserMealTypesByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<UserMealTypeResponseVM>>(HttpStatusCode.NotFound, null, "No User Meal Types found for the given tenant");
            }
            return new ResponseResult<List<UserMealTypeResponseVM>>(HttpStatusCode.OK, result, "User Meal Types fetched successfully for the given tenant");
        }

        [HttpPost]
        public ResponseResult<UserMealTypeResponseVM> CreateUserMealType(UserMealTypeRequestVM requestVM)
        {
            var result = _userMealTypeService.CreateUserMealType(requestVM);
            if (result == null)
            {
                return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.BadGateway, null, "User Meal Type not created");
            }
            return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.OK, result, "User Meal Type created successfully");
        }

        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<UserMealTypeResponseVM> UpdateUserMealType(int id, int tenantId, UserMealTypeUpdateVM requestVM)
        {
            var result = _userMealTypeService.UpdateUserMealType(id, tenantId, requestVM);
            if (result == null)
            {
                return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.NotFound, null, "User Meal Type not found");
            }
            return new ResponseResult<UserMealTypeResponseVM>(HttpStatusCode.OK, result, "User Meal Type updated successfully");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteUserMealType(int id, int tenantId)
        {
            var result = _userMealTypeService.DeleteUserMealType(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "User Meal Type not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "User Meal Type deleted successfully");


        }
    }
}