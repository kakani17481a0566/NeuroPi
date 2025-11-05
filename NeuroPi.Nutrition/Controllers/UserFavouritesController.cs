using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserFavourites;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFavouritesController : ControllerBase
    {
        private readonly IUserFavourites _userFavouritesService;
        public UserFavouritesController(IUserFavourites userFavouritesService)
        {
            _userFavouritesService = userFavouritesService;
        }

        [HttpGet]
        public ResponseResult<List<UserFavouritesResponseVM>> GetUserFavourites()
        {
            var result = _userFavouritesService.GetUserFavourites();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserFavouritesResponseVM>>(HttpStatusCode.NotFound, null, "No user favourites found.");

            }
            return new ResponseResult<List<UserFavouritesResponseVM>>(HttpStatusCode.OK, result, "User favourites fetched successfully.");

        }

        [HttpGet("id/{id}")]
        public ResponseResult<UserFavouritesResponseVM> GetUserFavouritesById(int id)
        {
            var result = _userFavouritesService.GetUserFavouritesById(id);
            if (result == null)
            {
                return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.NotFound, null, "User favourite not found.");
            }
            return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.OK, result, "User favourite fetched successfully.");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<UserFavouritesResponseVM>> GetUserFavouritesByTenantId(int tenantId)
        {
            var result = _userFavouritesService.GetUserFavouritesByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<UserFavouritesResponseVM>>(HttpStatusCode.NotFound, null, "No user favourites found for the tenant.");
            }
            return new ResponseResult<List<UserFavouritesResponseVM>>(HttpStatusCode.OK, result, "User favourites fetched successfully for the tenant.");
        }

        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserFavouritesResponseVM> GetUserFavouritesByIdAndTenantId(int id, int tenantId)
        {
            var result = _userFavouritesService.GetUserFavouritesByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.NotFound, null, "User favourite not found for the provided TenantId and Id");
            }
            return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.OK, result, "User favourite fetched successfully for the tenant.");
        }

        [HttpPost]
        public ResponseResult<UserFavouritesResponseVM> CreateUserFavourites([FromBody] UserFavouritesRequestVM userFavouritesRequestVM)
        {
            if (userFavouritesRequestVM == null)
            {
                return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request.");
            }
            var result = _userFavouritesService.CreateUserFavourites(userFavouritesRequestVM);
            return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.OK, result, "User favourite created successfully.");
        }
        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserFavouritesResponseVM> UpdateUserFavourites(int id, int tenantId, [FromBody] UserFavouritesUpdateVM userFavouritesUpdateVM)
        {

            var result = _userFavouritesService.UpdateUserFavourites(id, tenantId, userFavouritesUpdateVM);
            if (result == null)
            {
                return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.NotFound, null, "User favourite not found.");
            }
            return new ResponseResult<UserFavouritesResponseVM>(HttpStatusCode.OK, result, "User favourite updated successfully.");
        }
        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteUserFavourites(int id, int tenantId)
        {
            var isDeleted = _userFavouritesService.DeleteUserFavourites(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "User favourite not found.");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "User favourite deleted successfully.");
        }
    }
}