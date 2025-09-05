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

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<RolePermissionResponseVM> UpdateRolePermissionByIdAndTenantId(int id, int tenantId, [FromBody] RolePermissionVM rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.BadRequest, null, "Invalid data for update");
            }

            try
            {
                var updatedRolePermission = _rolePermissionService.UpdateRolePermissionByIdAndTenantId(id,tenantId, rolePermission);
                return updatedRolePermission == null
                    ? new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.NotFound, null, "Role permission not found")
                    : new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.OK, updatedRolePermission, "Role permission updated successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.InternalServerError, null, $"Error updating role permission: {ex.Message}");
            }
        }
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> Delete(int id, int tenantId)
        {
            try
            {
                var response = _rolePermissionService.DeleteByIdAndTenantId(id, tenantId);
                return response
                    ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Role permission deleted successfully")
                    : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Role permission not found");
            }
            catch (Exception ex)
            {
                return new ResponseResult<bool>(HttpStatusCode.InternalServerError, false, $"Error deleting role permission: {ex.Message}");
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

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<RolePermissionResponseVM>> GetRolePermissionByTenantID(int tenantId)
        {
            try
            {
                var result = _rolePermissionService.GetRolePermissionByTenantId(tenantId);
                return result.Count > 0
                    ? new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.OK, result, "Role permissions found")
                    : new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.NotFound, null, $"No role permissions found for tenant id {tenantId}");
            }
            catch (Exception ex)
            {
                return new ResponseResult<List<RolePermissionResponseVM>>(HttpStatusCode.InternalServerError, null, $"Error retrieving role permissions: {ex.Message}");
            }
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<RolePermissionResponseVM> GetByIdAndTenantId(int id, int tenantId)
        {
            try
            {
                var result = _rolePermissionService.GetRolePermissionByIdAndTenantId(id, tenantId);
                return result != null
                    ? new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.OK, result, "Role permission found")
                    : new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.NotFound, null, $"No role permission found with id {id} for tenant id {tenantId}");
            }
            catch (Exception ex)
            {
                return new ResponseResult<RolePermissionResponseVM>(HttpStatusCode.InternalServerError, null, $"Error retrieving role permission: {ex.Message}");
            }
        }

        [HttpGet("{roleId}/tenant/{tenantId}")]
        public ResponseResult<List<RolePermissionDescVM>> GetRolePermissionByRoleIdAndTenantId(int roleId, int tenantId)
        {
            //try
            //{
                var result = _rolePermissionService.GetRolePermissionByRoleIdAndTenantId(roleId, tenantId);
                return result != null
                    ? new ResponseResult<List<RolePermissionDescVM>>(HttpStatusCode.OK, result, "Role Permission Description Found")
                    : new ResponseResult<List<RolePermissionDescVM>>(HttpStatusCode.NotFound, null, $"No Role Description found with role id{roleId} for tenant id {tenantId}");
            
            //catch(Exception ex)
            //{
            //    return new ResponseResult<List<RolePermissionDescVM>>(HttpStatusCode.InternalServerError, null, $"Error retrieving role permission Description: {ex.Message}");
            //}
        }
        




    }
}
