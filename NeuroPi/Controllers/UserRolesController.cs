using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.UserRoles;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        // GET: api/UserRoles
        [HttpGet]
        public ResponseResult<List<UserRoleVM>> GetAllUserRoles()
        {
            var result = _userRolesService.GetAll();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserRoleVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No data for user roles");
            }

            return new ResponseResult<List<UserRoleVM>>(
                HttpStatusCode.OK,
                result,
                "User roles fetched successfully");
        }

        // GET: api/UserRoles/{id}
        [HttpGet("{id}")]
        public ResponseResult<UserRoleVM> GetUserRoleById(int id)
        {
            var result = _userRolesService.GetUserRoleById(id);
            if (result == null)
            {
                return new ResponseResult<UserRoleVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User role not found");
            }

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.OK,
                result,
                "User role fetched successfully");
        }

        // POST: api/UserRoles
        [HttpPost]
        public ResponseResult<UserRoleVM> AddUserRole([FromBody] UserRoleCreateVM userRole)
        {
            var result = _userRolesService.Create(userRole);
            if (result == null)
            {
                return new ResponseResult<UserRoleVM>(HttpStatusCode.NotAcceptable, null, "Failed to create user role");
            }

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.Created,
                result,
                "User role created successfully");
        }

        // PUT: api/UserRoles/{id}
        [HttpPut("{id}")]
        public ResponseResult<UserRoleVM> UpdateUserRoleById(int id, [FromBody] UserRoleUpdateVM userRole)
        {
            var result = _userRolesService.Update(id, userRole);
            if (result == null)
            {
                return new ResponseResult<UserRoleVM>(HttpStatusCode.NotFound, null, "User role not found");
            }

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.OK,
                result,
                "User role updated successfully");
        }

        // DELETE: api/UserRoles/{id}
        // DELETE: api/UserRoles/Tenant/{tenantId}/id/{id}
        [HttpDelete("Tenant/{tenantId}/id/{id}")]
        public ResponseResult<object> DeleteUserRoleByTenantIdAndId(int tenantId, int id)
        {
            var result = _userRolesService.DeleteByTenantIdAndId(tenantId, id);
            if (result)
            {
                return new ResponseResult<object>(
                    HttpStatusCode.OK,
                    null,
                    "User role deleted successfully by tenantId and id");
            }

            return new ResponseResult<object>(
                HttpStatusCode.BadRequest,
                null,
                $"User role not found with tenantId {tenantId} and id {id}");
        }

        // GET: api/UserRoles/Tenant/{tenantId}
        [HttpGet("Tenant/{tenantId}")]
        public ResponseResult<List<UserRoleVM>> GetUserRolesByTenantId(int tenantId)
        {
            var result = _userRolesService.GetUserRolesByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserRoleVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No data for user roles for the provided tenant");
            }

            return new ResponseResult<List<UserRoleVM>>(
                HttpStatusCode.OK,
                result,
                "User roles fetched successfully for the tenant");
        }

        // GET: api/UserRoles/Tenant/{tenantId}/{id}
        [HttpGet("Tenant/{tenantId}/{id}")]
        public ResponseResult<UserRoleVM> GetUserRoleByTenantIdAndId(int tenantId, int id)
        {
            var result = _userRolesService.GetUserRoleByTenantIdAndId(tenantId, id);
            if (result == null)
            {
                return new ResponseResult<UserRoleVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User role not found for the provided TenantId and Id");
            }

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.OK,
                result,
                "User role fetched successfully for the tenant");
        }
    }
}
