using System.Net;
using NeuroPi.UserManagment.Model;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.UserRoles;
using Microsoft.AspNetCore.Mvc;

namespace NeuroPi.UserManagment.Controllers
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

        // GET: api/userroles
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

            return new ResponseResult<List<UserRoleVM>>(
                HttpStatusCode.OK,
                roles,
                "User roles fetched");
        }

        // GET: api/userroles/{id}
        [HttpGet("{id}")]
        public ResponseResult<UserRoleVM> GetById(int id)
        {
            var role = _service.GetById(id);
            if (role == null)
                return new ResponseResult<UserRoleVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User role not found");

            var result = new UserRoleVM
            {
                UserRoleId = role.UserRoleId,
                UserId = role.UserId,
                RoleId = role.RoleId,
                TenantId = role.TenantId
            };

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.OK,
                result,
                "User role found");
        }

        // POST: api/userroles
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

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.Created,
                result,
                "User role created");
        }

        // PUT: api/userroles/{id}
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
                return new ResponseResult<UserRoleVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "User role not found");

            var result = new UserRoleVM
            {
                UserRoleId = updated.UserRoleId,
                UserId = updated.UserId,
                RoleId = updated.RoleId,
                TenantId = updated.TenantId
            };

            return new ResponseResult<UserRoleVM>(
                HttpStatusCode.OK,
                result,
                "User role updated");
        }

        // DELETE: api/userroles/{id}
        [HttpDelete("{id}")]
        public ResponseResult<object> Delete(int id)
        {
            var success = _service.Delete(id);
            if (!success)
                return new ResponseResult<object>(
                    HttpStatusCode.NotFound,
                    null,
                    "User role not found");

            return new ResponseResult<object>(
                HttpStatusCode.OK,
                null,
                "User role deleted");
        }
    }
}
