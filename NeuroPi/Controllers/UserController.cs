using System.Globalization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public ResponseResult<UserLogInSucessVM> Get(string username, string password)
        {
            var result = _userService.LogIn(username, password);
            if (result != null)
            {
                return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.OK, result, "Logged in successfull");
            }
            return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.NoContent, null, "Logged in failed");
        }
        [HttpGet("/tenantId")]
        public ResponseResult<List<UserResponseVM>> GetAllByTenantId([FromQuery] int tenantId)
        {
            var result = _userService.GetAllUsersByTenantId(tenantId);
            if (result != null)
            {
                return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, result, "Users fetched successfully By Tenant Id");
            }
            return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, $"Users not found with {tenantId}");
        }

        [HttpGet("/get")]
        public ResponseResult<List<UserResponseVM>> GetAllUsers()
        {
            var result = _userService.GetAllUsers();
            if (result != null)
            {
                return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.OK, result, "Users fetched successfully");
            }
            return new ResponseResult<List<UserResponseVM>>(HttpStatusCode.NotFound, null, "No users found");
        }

        [HttpGet("id")]
        
        public ResponseResult<UserResponseVM> GetUserById(int id)
        {
            var result = _userService.GetUser(id);
            if (result != null)
            {
                return new ResponseResult<UserResponseVM>(HttpStatusCode.OK, result, "User fetched successfully");
            }
            return new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "No user found");
        }
        [HttpPut]

        public ResponseResult<UserResponseVM> UpdateUser(int id,int tenantId,UserUpdateRequestVM updateUser)
        {
            var result = _userService.UpdateUser(id,tenantId,updateUser);
            if (result != null)
            {
                return new ResponseResult<UserResponseVM>(HttpStatusCode.OK, result, "User updated  successfully");
            }
            return new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "No user found");
        }

        [HttpDelete]
        public ResponseResult<Object> DeleteUserById(int id,int tenantId)
        {
            var result = _userService.DeleteUser(id,tenantId);
            if (result != null)
            {
                return new ResponseResult<Object>(HttpStatusCode.OK, null, "User Deleted successfully");
            }
            return new ResponseResult<Object>(HttpStatusCode.NotFound, null, "No user found");
        }
        [HttpPost]
        public ResponseResult<UserResponseVM> AddUser(UserRequestVM requestVm)
        {
            var result = _userService.AddUser(requestVm);
            if (result != null)
            {
                return new ResponseResult<UserResponseVM>(HttpStatusCode.OK, result, "User Deleted successfully");
            }
            return new ResponseResult<UserResponseVM>(HttpStatusCode.NotFound, null, "No user found");
        }
    }
}
