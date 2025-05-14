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
            var result=_userService.LogIn(username, password);
            if (result != null)
            {
                return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.OK,result,"Logged in successfull");
            }
            return new ResponseResult<UserLogInSucessVM>(HttpStatusCode.NoContent, null, "Logged in failed");
        }
    }
}
