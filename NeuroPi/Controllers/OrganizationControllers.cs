using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Organization;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public ResponseResult<object> GetAll()
        {
            try
            {
                var result = _organizationService.GetAll();
                if (result == null || result.Count == 0)
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "No organizations found");

                return new ResponseResult<object>(HttpStatusCode.OK, result, "Organizations fetched successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ResponseResult<object> GetById(int id)
        {
            try
            {
                var org = _organizationService.GetById(id);
                if (org == null)
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Organization not found");

                return new ResponseResult<object>(HttpStatusCode.OK, org, "Organization found successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        [HttpPost]
        public ResponseResult<object> Create([FromQuery] string name, [FromQuery] int tenantId, [FromQuery] int? parentId)
        {
            if (string.IsNullOrWhiteSpace(name) || tenantId <= 0)
                return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Invalid input");

            var input = new OrganizationInputVM
            {
                Name = name,
                TenantId = tenantId,
                ParentId = parentId
            };

            try
            {
                var created = _organizationService.Create(input);
                return new ResponseResult<object>(HttpStatusCode.Created, created, "Organization created successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ResponseResult<object> Update(int id, [FromQuery] string name, [FromQuery] int? parentId)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Invalid input");

            var input = new OrganizationUpdateInputVM
            {
                Name = name,
                ParentId = parentId
            };

            try
            {
                var updated = _organizationService.Update(id, input);
                if (updated == null)
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Organization not found");

                return new ResponseResult<object>(HttpStatusCode.OK, updated, "Organization updated successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ResponseResult<object> Delete(int id)
        {
            try
            {
                var success = _organizationService.Delete(id);
                if (!success)
                    return new ResponseResult<object>(HttpStatusCode.NotFound, null, "Organization not found");

                return new ResponseResult<object>(HttpStatusCode.OK, null, "Organization deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseResult<object>(HttpStatusCode.InternalServerError, null, ex.Message);
            }
        }
    }
}
