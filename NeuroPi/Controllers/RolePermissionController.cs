using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.RolePermission;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        public ResponseResult<RolePermissionResponseVM> AddRolePermission([FromBody] RolePermissionRequestVM rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.BadRequest, null, "Invalid role permission data");
            }

            try
            {
                var createdRolePermission = _rolePermissionService.AddRolePermission(rolePermission);
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.Created, createdRolePermission, "Role permission created successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.InternalServerError, null, $"Error creating role permission: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ResponseResult<RolePermissionResponseVM> UpdateRolePermission(int id, [FromBody] RolePermissionVM rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data for update");
            }

            try
            {
                var updatedRolePermission = _rolePermissionService.UpdateRolePermissionById(id, rolePermission);
                return updatedRolePermission == null
                    ? new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.NotFound, null, "Role permission not found")
                    : new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.OK, updatedRolePermission, "Role permission updated successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.InternalServerError, null, $"Error updating role permission: {ex.Message}");
            }
        }

        [HttpGet]
        public ResponseResult<List<RolePermissionResponseVM>> GetAll()
        {
            try
            {
                var result = _rolePermissionService.GetAllRolePermissions();
                return result.Count > 0
                    ? new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.OK, result, "Role Permissions retrieved successfully")
                    : new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.NoContent, result, "No role permissions found");
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error retrieving role permissions: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ResponseResult<RolePermissionResponseVM> GetById(int id)
        {
            try
            {
                var result = _rolePermissionService.GetRolePermissionById(id);
                return result != null
                    ? new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.OK, result, "Role permission found")
                    : new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.NotFound, null, $"No role permission found with id {id}");
            }
            catch (Exception ex)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.InternalServerError, null, $"Error retrieving role permission: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            try
            {
                var response = _rolePermissionService.DeleteById(id);
                return response
                    ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Role permission deleted successfully")
                    : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Role permission not found");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error deleting role permission: {ex.Message}");
            }
        }
    }
}
