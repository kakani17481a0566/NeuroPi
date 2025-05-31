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

        [HttpGet("login")]
        public ResponseResult<UserLogInSucessVM> LogIn([FromQuery] string username, [FromQuery] string password)
        {
            var result = _userService.LogIn(username, password);
            return result != null
                ? new ResponseResult<UserLogInSucessVM>(HttpStatusCode.OK, result, "Logged in successfully")
                : new ResponseResult<UserLogInSucessVM>(HttpStatusCode.Unauthorized, null, "Login failed");
        }

        [HttpGet("by-tenant")]
        public ResponseResult<List<UserResponseVM>> GetUsersByTenant([FromQuery] int tenantId)
        {
            var result = _userService.GetAllUsersByTenantId(tenantId);
            return result != null
                ? new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, result, "Users fetched successfully")
                : new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        [HttpGet]
        public ResponseResult<List<UserResponseVM>> GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            return result != null
                ? new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, result, "Users fetched successfully")
                : new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        [HttpGet("{id}")]
        public ResponseResult<UserResponseVM> GetUserById(int id, [FromQuery] int tenantId)
        {
            var result = _userService.GetUser(id, tenantId);
            return result != null
                ? new ResponseResult<UserResponseVM>(HttpStatusCode.OK, result, "User fetched successfully")
                : new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "User not found");
        }

        [HttpPut("{id}")]
        public ResponseResult<UserResponseVM> UpdateUser(int id, [FromQuery] int tenantId, [FromBody] UserUpdateRequestVM updateUser)
        {
            var result = _userService.UpdateUser(id, tenantId, updateUser);
            return result != null
                ? new ResponseResult<UserResponseVM>(HttpStatusCode.OK, result, "User updated successfully")
                : new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "User not found");
        }

        [HttpDelete("{id}")]
        public ResponseResult<object> DeleteUser(int id, [FromQuery] int tenantId)
        {
            var result = _userService.DeleteUser(id, tenantId);
            return result != null
                ? new ResponseResult<object>(HttpStatusCode.OK, null, "User deleted successfully")
                : new ResponseResult<object>(HttpStatusCode.NotFound, null, "User not found");
        }

        [HttpPost]
        public ResponseResult<UserResponseVM> AddUser([FromBody] UserRequestVM requestVm)
        {
            var result = _userService.AddUser(requestVm);
            return result != null
                ? new ResponseResult<UserResponseVM>(HttpStatusCode.Created, result, "User added successfully")
                : new ResponseResult<UserResponseVM>(HttpStatusCode.BadRequest, null, "Failed to add user");
        }
    }
}
