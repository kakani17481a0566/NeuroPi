using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.User;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET api/user/login?username=...&password=...
        [HttpGet("login")]
        public ResponseResult<UserLogInSucessVM> Login([FromQuery] string username, [FromQuery] string password)
        {
            var loginResult = _userService.LogIn(username, password);

            if (loginResult != null)
                return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.OK, loginResult, "Logged in successfully");

            return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.Unauthorized, null, "Login failed");
        }

        // GET api/user/by-tenant?tenantId=...
        [HttpGet("by-tenant")]
        public ResponseResult<List<UserResponseVM>> GetUsersByTenantId([FromQuery] int tenantId)
        {
            var users = _userService.GetAllUsersByTenantId(tenantId);

            if (users != null && users.Count > 0)
                return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, users, "Users fetched successfully");

            return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        // GET api/user/by-tenant-with-roles?tenantId=...
        [HttpGet("by-tenant-with-roles")]
        public ResponseResult<List<UserResponseVM>> GetUsersByTenantIdWithRoles([FromQuery] int tenantId)
        {
            var users = _userService.GetAllUsersByTenantIdWithRoles(tenantId);

            if (users != null && users.Count > 0)
                return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, users, "Users with roles fetched successfully");

            return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        // GET api/user
        [HttpGet]
        public ResponseResult<List<UserResponseVM>> GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            if (users != null && users.Count > 0)
                return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, users, "Users fetched successfully");

            return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        // GET api/user/{id}?tenantId=...
        [HttpGet("{id:int}")]
        public ResponseResult<UserResponseVM> GetUserById(int id, [FromQuery] int tenantId)
        {
            Console.WriteLine($"GetUserById called with id={id}, tenantId={tenantId}");
            var user = _userService.GetUser(id, tenantId);
            Console.WriteLine(user == null ? "User not found" : "User found");

            if (user != null)
                return new ResponseResult<UserResponseVM>(HttpStatusCode.OK, user, "User fetched successfully");

            return new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "User not found");
        }


        // PUT api/user/{id}?tenantId=...
        [HttpPut("{id}")]
        public ResponseResult<UserResponseVM> UpdateUser(int id, [FromQuery] int tenantId, [FromBody] UserUpdateRequestVM updateUser)
        {
            var updatedUser = _userService.UpdateUser(id, tenantId, updateUser);

            if (updatedUser != null)
                return new ResponseResult<UserResponseVM>(HttpStatusCode.OK, updatedUser, "User updated successfully");

            return new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "User not found");
        }

        // DELETE api/user/{id}?tenantId=...
        [HttpDelete("{id}")]
        public ResponseResult<object> DeleteUser(int id, [FromQuery] int tenantId)
        {
            var deletedUser = _userService.DeleteUser(id, tenantId);

            if (deletedUser != null)
                return new ResponseResult<object>(HttpStatusCode.OK, null, "User deleted successfully");

            return new ResponseResult<object>(HttpStatusCode.NotFound, null, "User not found");
        }

        // POST api/user
        [HttpPost]
        public ResponseResult<UserResponseVM> AddUser([FromBody] UserRequestVM newUser)
        {
            var createdUser = _userService.AddUser(newUser);

            if (createdUser != null)
                return new ResponseResult<UserResponseVM>(HttpStatusCode.Created, createdUser, "User added successfully");

            return new ResponseResult<UserResponseVM>(HttpStatusCode.BadRequest, null, "Failed to add user");
        }

        [HttpPut("{id}/image")]
        public async Task<ResponseResult<string>> UpdateUserImageAsync(
           int id,
           [FromQuery] int tenantId,
           [FromForm] UserImageUploadVM request)
        {
            var result = await _userService.UpdateUserImageAsync(id, tenantId, request);

            if (!string.IsNullOrEmpty(result))
                return new ResponseResult<string>(HttpStatusCode.OK, result, "Image updated successfully");

            return new ResponseResult<string>(HttpStatusCode.BadRequest, null, "Failed to update user image");
        }

        [Authorize]
        [HttpPut("{id}/password")]
        public ResponseResult<object> UpdatePassword(
            int id,
            [FromQuery] int tenantId,
            [FromBody] UserUpdatePasswordVM request)
        {
            var result = _userService.UpdateUserPassword(id, tenantId, request, out string message);

            if (result)
            {
                return new ResponseResult<object>(
                    HttpStatusCode.OK,
                    null,
                    message // "Password updated successfully"
                );
            }

            // Choose proper HTTP code based on message
            if (message == "Current password is incorrect")
            {
                return new ResponseResult<object>(
                    HttpStatusCode.Unauthorized,
                    null,
                    message
                );
            }

            if (message == "User not found")
            {
                return new ResponseResult<object>(
                    HttpStatusCode.NotFound,
                    null,
                    message
                );
            }

            return new ResponseResult<object>(
                HttpStatusCode.BadRequest,
                null,
                message // "Failed to update password"
            );
        }

        [Authorize]
        [HttpPut("{id}/reset-password")]
        public ResponseResult<object> ResetPassword(
            int id,
            [FromQuery] int tenantId,
            [FromBody] AdminResetPasswordVM request)
        {
            var result = _userService.ResetUserPassword(id, tenantId, request, out string message);

            if (result)
            {
                return new ResponseResult<object>(
                    HttpStatusCode.OK,
                    null,
                    message // "Password reset successfully"
                );
            }

            if (message == "User not found")
            {
                return new ResponseResult<object>(
                    HttpStatusCode.NotFound,
                    null,
                    message
                );
            }

            return new ResponseResult<object>(
                HttpStatusCode.BadRequest,
                null,
                message // "Failed to reset password"
            );
        }



        // GET api/user/{id}/profile-summary?tenantId=...
        [HttpGet("{id}/profile-summary")]
        public ResponseResult<UsersProfileSummaryVM> GetUserProfileSummary(int id, [FromQuery] int tenantId)
        {
            var summary = _userService.GetUserProfileSummary(id, tenantId);

            if (summary != null)
                return new ResponseResult<UsersProfileSummaryVM>(HttpStatusCode.OK, summary, "Profile summary fetched successfully");

            return new ResponseResult<UsersProfileSummaryVM>(HttpStatusCode.NotFound, null, "User not found or no summary available");
        }

        [HttpGet("check-username")]
        public ResponseResult<bool> CheckUsername([FromQuery] string username)
        {
            try
            {
                bool exists = _userService.CheckUsernameExists(username);
                return new ResponseResult<bool>(HttpStatusCode.OK, exists, exists ? "Username exists" : "Username is available");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error checking username: {ex.Message}");
            }
        }

    }
}
