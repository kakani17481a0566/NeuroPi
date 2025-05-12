using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.UserRoles;
using System;

namespace NeuroPi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _service;

        public UserRolesController(IUserRolesService service)
        {
            _service = service;
        }

        [HttpGet]
        public ResponseResult<List<UserRoleVM>> GetAll()
        {
            var roles = _service.GetAll()
                .Select(r => new UserRoleVM
                {
                    UserRoleId = r.UserRoleId,
                    UserId = r.UserId,
                    RoleId = r.RoleId,
                    TenantId = r.TenantId
                }).ToList();

            return ResponseResult<List<UserRoleVM>>.SuccessResponse(HttpStatusCode.OK, roles, "User roles fetched");
        }

        [HttpGet("{id}")]
        public ResponseResult<UserRoleVM> GetById(int id)
        {
            var role = _service.GetById(id);
            if (role == null)
                return ResponseResult<UserRoleVM>.FailResponse(HttpStatusCode.NotFound, "User role not found");

            var result = new UserRoleVM
            {
                UserRoleId = role.UserRoleId,
                UserId = role.UserId,
                RoleId = role.RoleId,
                TenantId = role.TenantId
            };

            return ResponseResult<UserRoleVM>.SuccessResponse(HttpStatusCode.OK, result, "User role found");
        }

        [HttpPost]
        public ResponseResult<UserRoleVM> Create([FromBody] UserRoleCreateVM input)
        {
            var entity = new MUserRole
            {
                UserId = input.UserId,
                RoleId = input.RoleId,
                TenantId = input.TenantId,
                CreatedBy = input.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            var created = _service.Create(entity);

            var result = new UserRoleVM
            {
                UserRoleId = created.UserRoleId,
                UserId = created.UserId,
                RoleId = created.RoleId,
                TenantId = created.TenantId
            };

            return ResponseResult<UserRoleVM>.SuccessResponse(HttpStatusCode.Created, result, "User role created");
        }

        [HttpPut("{id}")]
        public ResponseResult<UserRoleVM> Update(int id, [FromBody] UserRoleUpdateVM input)
        {
            var entity = new MUserRole
            {
                UserId = input.UserId,
                RoleId = input.RoleId,
                TenantId = input.TenantId,
                UpdatedBy = input.UpdatedBy
            };

            var updated = _service.Update(id, entity);
            if (updated == null)
                return ResponseResult<UserRoleVM>.FailResponse(HttpStatusCode.NotFound, "User role not found");

            var result = new UserRoleVM
            {
                UserRoleId = updated.UserRoleId,
                UserId = updated.UserId,
                RoleId = updated.RoleId,
                TenantId = updated.TenantId
            };

            return ResponseResult<UserRoleVM>.SuccessResponse(HttpStatusCode.OK, result, "User role updated");
        }

        [HttpDelete("{id}")]
        public ResponseResult<object> Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, "User role not found");

            return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, null, "User role deleted");
        }
    }
}
