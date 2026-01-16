using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Permissions;
using NeuroPi.UserManagment.ViewModel.RolePermission;
using System.Net;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public ResponseResult<PermissionResponseVM> AddPermission([FromBody] PermissionRequestVM requestVM)
        {
            var result = _permissionService.AddPermission(requestVM);
            if (result != null)
            {
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permission added Successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.NotAcceptable, null, "Permission Not Added Successfully");
        }

        [HttpGet]
        public ResponseResult<List<PermissionResponseVM>> GetAllPermissions()
        {
            var result = _permissionService.GetPermissions();
            if (result != null)
            {
                return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.OK, result, "Permissions fetched Successfully");
            }
            return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.NotFound, null, "Permissions Not Found");
        }

        [HttpGet("id")]
        public ResponseResult<PermissionResponseVM> GetPermissionById(int  id)
        {
            var result = _permissionService.GetById(id);
            if (result != null)
            {
                //var response = PermissionResponseVM.ToViewModel(result);
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permissions Fetched By id Successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.NotFound, null, $"Permission not found with id {id}");
        }

        [HttpDelete("id")]
        public ResponseResult<Object> DeleteById(int id,int tenantId)
        {
            var result = _permissionService.DeletePermission(id,tenantId);
            if (result != null)
            {
                return new ResponseResult<object>(HttpStatusCode.OK, null, "Permission Deleted Successfully");
            }
            return new ResponseResult<object>(HttpStatusCode.BadRequest, null, $"Permission Not found with id {id}");
        }

        [HttpPut("id")]
        public ResponseResult<PermissionResponseVM> UpdatePermissionById(int id,int tenantId, PermissionUpdateRequestVM permission)
        {
            var result = _permissionService.UpdatePermission(id,tenantId, permission);
            if (result != null)
            {
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permission Updated successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.BadGateway, null, "Permission not updated");
        }
        [HttpGet]
        [Route("/get-by-tenantId")]
        public ResponseResult<List<PermissionResponseVM>> GetAllByTenantId(int id)
        {
            var result=_permissionService.GetAllPermissionsByTenantId(id);
            if(result != null)
            {
                return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.OK, result, "Permissions fetched successfully by using tenant id");
            }
            return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.NotFound, null, "Permissions not found ");
        }

        [HttpGet]
        [Route("/get-by-Id-and-tenantId")]
        public ResponseResult<PermissionResponseVM> GetByTenantIdAndId(int id,int tenantId)
        {
            var result = _permissionService.GetByIdAndTenantId(id,tenantId);
            if (result != null)
            {
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permissions fetched successfully by using tenant id");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.NotFound, null, "Permissions not found ");
        }
        [HttpGet("/getrolePermissions")]
        public List<PermissionDescriptionVM>  GetRolePermissions( int roleId)
        {
            List<PermissionDescriptionVM> result =_permissionService.GetByPermissionId(roleId);
             return result;
        }
    }
}
