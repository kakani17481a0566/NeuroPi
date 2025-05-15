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

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<UserDepartmentResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            var userDepartment = _userDepartmentService.GetUserDepartmentByIdAndTenantId(id, tenantId);
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
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<UserDepartmentResponseVM>> GetByTenantId(int tenantId)
        {
            var userDepartments = _userDepartmentService.GetUserDepartmentsByTenantId(tenantId);
            if (userDepartments == null || userDepartments.Count == 0)
            {
                return new ResponseResult<List<UserDepartmentResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No user departments found for the specified tenant");
            }
            return new ResponseResult<List<UserDepartmentResponseVM>>(
                HttpStatusCode.OK,
                userDepartments,
                "User departments retrieved successfully");
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

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<UserDepartmentResponseVM> Update(int id, int tenantId, [FromBody] UserDepartmentUpdateVM userDepartmentUpdate)
        {
            if (userDepartmentUpdate == null)
            {
                return new ResponseResult<UserDepartmentResponseVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid request");
            }
            var updatedUserDepartment = _userDepartmentService.UpdateUserDepartmentByUserDeptIdAndTenantId(id, tenantId, userDepartmentUpdate);
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

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            var isDeleted = _userDepartmentService.DeleteUserDepartmentByUserDeptIdAndTenantId(id, tenantId);
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