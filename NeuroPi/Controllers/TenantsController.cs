using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Tenent;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        // GET: api/tenants
        [HttpGet]
        public IActionResult GetAll()
        {
            var tenants = _tenantService.GetAllTenants();

            if (tenants == null || tenants.Count == 0)
            {
                return new ResponseResult<List<TenantVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No tenants found");
            }

            return new ResponseResult<List<TenantVM>>(
                HttpStatusCode.OK,
                tenants,
                "Tenants retrieved successfully");
        }

        // GET: api/tenants/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var tenant = _tenantService.GetTenantById(id);

            if (tenant == null)
            {
                return new ResponseResult<TenantVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Tenant not found");
            }

            return new ResponseResult<TenantVM>(
                HttpStatusCode.OK,
                tenant,
                "Tenant retrieved successfully");
        }

        // POST: api/tenants
        [HttpPost]
        public IActionResult Create([FromBody] TenantInputVM tenantInput)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<TenantVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid tenant data");
            }

            var createdTenant = _tenantService.CreateTenant(tenantInput);

            return new ResponseResult<TenantVM>(
                HttpStatusCode.Created,
                createdTenant,
                "Tenant created successfully");
        }

        // PUT: api/tenants/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TenantUpdateInputVM tenantUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return new ResponseResult<TenantVM>(
                    HttpStatusCode.BadRequest,
                    null,
                    "Invalid tenant data");
            }

            var updatedTenant = _tenantService.UpdateTenant(id, tenantUpdateInput);

            if (updatedTenant == null)
            {
                return new ResponseResult<TenantVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Tenant not found");
            }

            return new ResponseResult<TenantVM>(
                HttpStatusCode.OK,
                updatedTenant,
                "Tenant updated successfully");
        }

        // DELETE: api/tenants/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _tenantService.DeleteTenant(id);

            if (!result)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "Tenant not found or could not be deleted");
            }

            return new ResponseResult<bool>(
                HttpStatusCode.OK,
                true,
                "Tenant deleted successfully");
        }
    }
}
