using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Permissions;
using System.Net;

namespace NeuroPi.Controllers
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
            var result=_permissionService.AddPermission(requestVM);
            if (result != null)
            {
                return ResponseResult<PermissionResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Permission added Successfully");
            }
            return ResponseResult<PermissionResponseVM>.FailResponse(HttpStatusCode.NotAcceptable, "Permission Not Added Successfully");
        }
        [HttpGet]
        public ResponseResult<List<PermissionResponseVM>> GetAllPermissions()
        {
            var result=_permissionService.GetPermissions();
            if (result != null)
            {
                return ResponseResult<List<PermissionResponseVM>>.SuccessResponse(HttpStatusCode.OK, result, "Permissions fetched Successfully");
            }
            return ResponseResult<List<PermissionResponseVM>>.FailResponse(HttpStatusCode.NotFound, "Permissions Not Found");
        }
        [HttpGet("id")]
        public ResponseResult<PermissionResponseVM> GetPermissionById(int id)
        {
            var result=_permissionService.GetById(id);
            if(result != null)
            {
                var response = PermissionResponseVM.ToViewModel(result);
                return ResponseResult<PermissionResponseVM>.SuccessResponse(HttpStatusCode.OK, response, "Permissions Fetched BY id Successfully");
            }
            return ResponseResult<PermissionResponseVM>.FailResponse(HttpStatusCode.NotFound, $"Permission not found with id {id}");
        }

        [HttpDelete("id")]
        public ResponseResult<Object> DeleteById(int id)
        {
            var result=_permissionService.DeletePermission(id);
            if (result != null)
            {
                return ResponseResult<Object>.SuccessResponse(HttpStatusCode.OK, null, "Permission Deleted Successfully");
            }
            return ResponseResult<Object>.FailResponse(HttpStatusCode.BadRequest, $"Permission Not found with id {id}");
        }

        [HttpPut("id")]
        public ResponseResult<PermissionResponseVM> UpdatePermissionById(int id, PermissionRequestVM permission)
        {
            var result = _permissionService.UpdatePermission(id, permission);
            if (result != null)
            {
                return ResponseResult<PermissionResponseVM>.SuccessResponse(HttpStatusCode.OK, result, "Permission Updated successfully");
            }
            return ResponseResult<PermissionResponseVM>.FailResponse(HttpStatusCode.BadGateway, "Permission not updated ");
        }
    }
}
