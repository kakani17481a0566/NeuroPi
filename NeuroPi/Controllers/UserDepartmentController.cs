using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.UserDepartment;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDepartmentController : ControllerBase
    {
        private readonly IUserDepartmentService _userDepartmentService;
        public UserDepartmentController(IUserDepartmentService userDepartmentService)
        {
            _userDepartmentService = userDepartmentService;
        }


        [HttpGet]
        public ResponseResult<List<UserDepartmentResponseVM>> GetAll()
        {
            var userDepartments = _userDepartmentService.GetAllUserDepartments();
            if (userDepartments == null || userDepartments.Count == 0)
            {
                return new ResponseResult<List<UserDepartmentResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No user departments found");
            }
            return new ResponseResult<List<UserDepartmentResponseVM>>(
                HttpStatusCode.OK,
                userDepartments,
                "User departments retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<UserDepartmentResponseVM> GetById(int id)
        {
            var userDepartment = _userDepartmentService.GetUserDepartmentById(id);
            if (userDepartment == null)
            {
                return new ResponseResult<UserDepartmentResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User department not found");
            }
            return new ResponseResult<UserDepartmentResponseVM>(
                HttpStatusCode.OK,
                userDepartment,
                "User department retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<UserDepartmentCreateVM> Create([FromBody] UserDepartmentRequestVM userDepartmentRequest)
        {
            if (userDepartmentRequest == null)
            {
                return new ResponseResult<UserDepartmentCreateVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid request");
            }
            var createdUserDepartment = _userDepartmentService.CreateUserDepartment(userDepartmentRequest);
            if (createdUserDepartment == null)
            {
                return new ResponseResult<UserDepartmentCreateVM>(
                    HttpStatusCode.InternalServerError,
                    null,
                    "Failed to create user department");
            }
            return new ResponseResult<UserDepartmentCreateVM>(
                HttpStatusCode.Created,
                createdUserDepartment,
                "User department created successfully");
        }

        [HttpPut("{id}")]
        public ResponseResult<UserDepartmentResponseVM> Update(int id, [FromBody] UserDepartmentUpdateVM userDepartmentRequest)
        {
            if (userDepartmentRequest == null)
            {
                return new ResponseResult<UserDepartmentResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid request");
            }
            var updatedUserDepartment = _userDepartmentService.UpdateUserDepartment(id, userDepartmentRequest);
            if (updatedUserDepartment == null)
            {
                return new ResponseResult<UserDepartmentResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User department not found");
            }
            return new ResponseResult<UserDepartmentResponseVM>(
                HttpStatusCode.OK,
                updatedUserDepartment,
                "User department updated successfully");
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            var isDeleted = _userDepartmentService.DeleteUserDepartment(id);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "User department not found");
            }
            return new ResponseResult<bool>(
                HttpStatusCode.OK,
                true,
                "User department deleted successfully");
        }
    }
}