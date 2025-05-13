using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Role;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ResponseResult<List<RoleResponseVM>> GetRoles()
        {
            var result = _roleService.GetAllRoles();
            if (result != null)
            {
                return new ResponseResult<List<RoleResponseVM>>(HttpStatusCode.OK, result, "Roles fetched successfully");
            }
            return new ResponseResult<List<RoleResponseVM>>(HttpStatusCode.NoContent, null, "Roles not found");
        }

        [HttpGet("id")]
        public ResponseResult<RoleResponseVM> GetRoleById(int id)
        {
            var result = _roleService.GetRoleById(id);
            if (result != null)
            {
                return new ResponseResult<RoleResponseVM>(HttpStatusCode.OK, result, "Role fetched successfully");
            }
            return new ResponseResult<RoleResponseVM>(HttpStatusCode.NotFound, null, "Role not found");
        }

        [HttpPost]
        public ResponseResult<RoleResponseVM> AddRole([FromBody] RoleRequestVM roleRequest)
        {
            var result = _roleService.AddRole(roleRequest);
            if (result != null)
            {
                return new ResponseResult<RoleResponseVM>(HttpStatusCode.OK, result, "Role created successfully");
            }
            return new ResponseResult<RoleResponseVM>(HttpStatusCode.NotFound, null, "Role not created");
        }

        [HttpPut("id")]
        public ResponseResult<RoleResponseVM> UpdateRole(int id, [FromBody] RoleRequestVM roleRequest)
        {
            var result = _roleService.UpdateRole(id, roleRequest);
            if (result != null)
            {
                return new ResponseResult<RoleResponseVM>(HttpStatusCode.OK, result, "Role updated successfully");
            }
            return new ResponseResult<RoleResponseVM>(HttpStatusCode.NotModified, null, "Role not updated");
        }

        [HttpDelete("id")]
        public ResponseResult<object> DeleteRoleById(int id)
        {
            var result = _roleService.DeleteRoleById(id);
            if (result)
            {
                return new ResponseResult<object>(HttpStatusCode.OK, null, "Role deleted successfully");
            }
            return new ResponseResult<object>(HttpStatusCode.BadRequest, null, $"Role not found with id {id}");
        }
        [HttpGet("/tenant/{tenantId}")]
        public ResponseResult<List<RoleResponseVM>> GetAllRolesByTenantId(int tenantId)
        {
            var result=_roleService.GetAllRolesByTenantId(tenantId);
            return result != null ? new ResponseResult<List<RoleResponseVM>>(HttpStatusCode.OK, result, "Roles fetched successfully with tenant id") : new ResponseResult<List<RoleResponseVM>>(HttpStatusCode.NotFound, null, $"Roles Not found with tenantId{tenantId}");
        }

    }
}
