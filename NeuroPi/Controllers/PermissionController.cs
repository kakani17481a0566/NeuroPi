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
        public IActionResult AddPermission([FromBody] PermissionRequestVM requestVM)
        {
            var result = _permissionService.AddPermission(requestVM);
            if (result != null)
            {
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permission added Successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.NotAcceptable, null, "Permission Not Added Successfully");
        }

        [HttpGet]
        public IActionResult GetAllPermissions()
        {
            var result = _permissionService.GetPermissions();
            if (result != null)
            {
                return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.OK, result, "Permissions fetched Successfully");
            }
            return new ResponseResult<List<PermissionResponseVM>>(HttpStatusCode.NotFound, null, "Permissions Not Found");
        }

        [HttpGet("id")]
        public IActionResult GetPermissionById(int id)
        {
            var result = _permissionService.GetById(id);
            if (result != null)
            {
                var response = PermissionResponseVM.ToViewModel(result);
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, response, "Permissions Fetched By id Successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.NotFound, null, $"Permission not found with id {id}");
        }

        [HttpDelete("id")]
        public IActionResult DeleteById(int id)
        {
            var result = _permissionService.DeletePermission(id);
            if (result != null)
            {
                return new ResponseResult<Object>(HttpStatusCode.OK, null, "Permission Deleted Successfully");
            }
            return new ResponseResult<Object>(HttpStatusCode.BadRequest, null, $"Permission Not found with id {id}");
        }

        [HttpPut("id")]
        public IActionResult UpdatePermissionById(int id, PermissionRequestVM permission)
        {
            var result = _permissionService.UpdatePermission(id, permission);
            if (result != null)
            {
                return new ResponseResult<PermissionResponseVM>(HttpStatusCode.OK, result, "Permission Updated successfully");
            }
            return new ResponseResult<PermissionResponseVM>(HttpStatusCode.BadGateway, null, "Permission not updated");
        }
    }
}
