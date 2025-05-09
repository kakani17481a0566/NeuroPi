using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.RolePermission;

namespace NeuroPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermisisionService _rolePermissionService;
        public RolePermissionController(IRolePermisisionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        public ResponseResult<RolePermissionResponseVM> AddRolePermission([FromBody] RolePermissionRequestVM rolePermission)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult<RolePermissionResponseVM>.FailResponse(HttpStatusCode.BadRequest, "Invalid role permission data");
            }
            var createdRolePermission = _rolePermissionService.AddRolePermission(rolePermission);
            return ResponseResult<RolePermissionResponseVM>.SuccessResponse(HttpStatusCode.Created, createdRolePermission, "Role permission created successfully");
        }
        [HttpPut("{id}")]
        public ResponseResult<RolePermissionResponseVM> UpdateRolePermissionById(int id, [FromBody] RolePermissionVM rolePermission)
        { 
            var updatedRolePermission = _rolePermissionService.UpdateRolePermissionById(id, rolePermission);
            if (updatedRolePermission == null)
            {
                return ResponseResult<RolePermissionResponseVM>.FailResponse(HttpStatusCode.NotFound, "Role permission not found");
            }
            return ResponseResult<RolePermissionResponseVM>.SuccessResponse(HttpStatusCode.OK, updatedRolePermission, "Role permission updated successfully");
        }

        [HttpGet]
        public ResponseResult<List<RolePermissionResponseVM>> GetAllRolePermissions()
        {
            var result = _rolePermissionService.GetAllRolePermissions();
            if (result != null)
            {
                return ResponseResult<List<RolePermissionResponseVM>>.SuccessResponse(HttpStatusCode.OK, result, "Role Permissions retrived Successfully");
            }
            return ResponseResult<List<RolePermissionResponseVM>>.FailResponse(HttpStatusCode.NoContent, "No data found");
        }
        

        [HttpGet("{id}")]
        public ResponseResult<RolePermissionResponseVM> GetRolePermissionById(int id)
        {
            var result = _rolePermissionService.GetRolePermissionById(id);
            if (result != null)
            {
                return ResponseResult<RolePermissionResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Role Permission found");
            }
            return ResponseResult<RolePermissionResponseVM>.FailResponse(HttpStatusCode.NotFound, $"No data found with id {id}");
        }


        [HttpDelete("{id}")]
        public ResponseResult<bool> DeleteById(int id)
        {
            var response = _rolePermissionService.DeleteById(id);
            if (response)
            {
                return ResponseResult<bool>.SuccessResponse(HttpStatusCode.OK, response, "Role Permission Deleted Successfully");
            }
            return ResponseResult<bool>.FailResponse(HttpStatusCode.NotFound, "No Content has deleted");
        }
    }
}
