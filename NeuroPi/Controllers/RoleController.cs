using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Role;
using System.Net;

namespace NeuroPi.Controllers
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
                return ResponseResult<List<RoleResponseVM>>.SuccessResponse(System.Net.HttpStatusCode.OK, result, "Roles fetched successfully");
            }
            return ResponseResult<List<RoleResponseVM>>.FailResponse(System.Net.HttpStatusCode.NoContent, "Roles Not found");
        }
        [HttpGet("id")]
        public ResponseResult<RoleResponseVM> GetRoleById(int id)
        {
            var result = _roleService.GetRoleById(id);
            if (result != null)
            {
                return ResponseResult<RoleResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Role fetched successfully");
            }
            return ResponseResult<RoleResponseVM>.FailResponse(HttpStatusCode.NotFound, "Role Not found");
        }
        [HttpPost]

        public ResponseResult<RoleResponseVM> AddRole(RoleRequestVM roleRequest)
        {
            var result = _roleService.AddRole(roleRequest);
            if (result != null)
            {
                return ResponseResult<RoleResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Role Created successfully");
            }
            return ResponseResult<RoleResponseVM>.FailResponse(HttpStatusCode.NotFound, "Role Not Created ");

        }
        [HttpPut("id")]
        public ResponseResult<RoleResponseVM> UpdateRole(int id, RoleRequestVM roleRequest)
        {
            var result = _roleService.UpdateRole(id, roleRequest);
            if (result != null)
            {
                return ResponseResult<RoleResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "updated role successfully");
            }
            return ResponseResult<RoleResponseVM>.FailResponse(HttpStatusCode.NotModified, "Not updated role");

        }
        [HttpDelete("id")]
        public ResponseResult<object> DeleteRoleById(int id)
        {
            _roleService.DeleteRoleById(id);
             return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, null, "Deleted  role successfully");
            

        }
    }
    
}
