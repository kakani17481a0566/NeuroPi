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
        [HttpGet("{id}")]
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
    }
}
