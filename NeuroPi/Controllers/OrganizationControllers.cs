using Microsoft.AspNetCore.Mvc;
using System.Net;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Organization;
using System;

namespace NeuroPi.Controllers
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
        public IActionResult GetAll()
        {
            try
            {
                var result = _organizationService.GetAll();
                if (result.Count == 0)
                    return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, "No organizations found");

                return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var org = _organizationService.GetById(id);
                if (org == null)
                    return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, "Organization not found");

                return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, org);
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromQuery] string name, [FromQuery] int tenantId, [FromQuery] int? parentId)
        {
            if (string.IsNullOrWhiteSpace(name) || tenantId <= 0)
                return ResponseResult<object>.FailResponse(HttpStatusCode.BadRequest, "Invalid input");

            var input = new OrganizationInputModel
            {
                Name = name,
                TenantId = tenantId,
                ParentId = parentId
            };

            try
            {
                var created = _organizationService.Create(input);
                return ResponseResult<object>.SuccessResponse(HttpStatusCode.Created, created, "Organization created");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromQuery] string name, [FromQuery] int? parentId)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ResponseResult<object>.FailResponse(HttpStatusCode.BadRequest, "Invalid input");

            var input = new OrganizationUpdateInputModel
            {
                Name = name,
                ParentId = parentId
            };

            try
            {
                var updated = _organizationService.Update(id, input);
                if (updated == null)
                    return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, "Organization not found");

                return ResponseResult<object>.SuccessResponse(HttpStatusCode.OK, updated, "Organization updated");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var success = _organizationService.Delete(id);
                if (!success)
                    return ResponseResult<object>.FailResponse(HttpStatusCode.NotFound, "Organization not found");

                return ResponseResult<object>.SuccessResponse(HttpStatusCode.NoContent, null, "Organization deleted");
            }
            catch (Exception ex)
            {
                return ResponseResult<object>.FailResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
