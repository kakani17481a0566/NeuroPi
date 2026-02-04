using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel;
using NeuroPi.UserManagment.Response;
using System.Net;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public ResponseResult<List<RolesResponseVM>> GetAll([FromQuery] int tenantId)
        {
            var result = _rolesService.GetAll(tenantId);
            return new ResponseResult<List<RolesResponseVM>>(HttpStatusCode.OK, result, "Roles fetched successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<RolesResponseVM> GetById(int id)
        {
            var result = _rolesService.GetById(id);
            if (result == null)
                return new ResponseResult<RolesResponseVM>(HttpStatusCode.NotFound, null, "Role not found");
            return new ResponseResult<RolesResponseVM>(HttpStatusCode.OK, result, "Role fetched successfully");
        }

        [HttpPost]
        public ResponseResult<RolesResponseVM> Create([FromBody] RolesRequestVM request)
        {
            var result = _rolesService.Create(request);
            return new ResponseResult<RolesResponseVM>(HttpStatusCode.OK, result, "Role created successfully");
        }

        [HttpPut("{id}")]
        public ResponseResult<RolesResponseVM> Update(int id, [FromBody] RolesUpdateVM request)
        {
            var result = _rolesService.Update(id, request);
            if (result == null)
                return new ResponseResult<RolesResponseVM>(HttpStatusCode.NotFound, null, "Role not found");
            return new ResponseResult<RolesResponseVM>(HttpStatusCode.OK, result, "Role updated successfully");
        }

        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            var result = _rolesService.Delete(id);
            if (!result)
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Role not found");
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Role deleted successfully");
        }
    }
}
