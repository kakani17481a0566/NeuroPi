using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Tenent;
using System.Collections.Generic;
using System.Net;

namespace NeuroPi.Controllers
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
        public ResponseResult<List<TenantVM>> GetAll()
        {
            var tenants = _tenantService.GetAllTenants();
            if (tenants == null || tenants.Count == 0)
            {
                return ResponseResult<List<TenantVM>>.FailResponse(HttpStatusCode.NotFound, "No tenants found");
            }
            return ResponseResult<List<TenantVM>>.SuccessResponse(HttpStatusCode.OK, tenants, "Tenants retrieved successfully");
        }

        // GET: api/tenants/{id}
        [HttpGet("{id}")]
        public ResponseResult<TenantVM> GetById(int id)
        {
            var tenant = _tenantService.GetTenantById(id);
            if (tenant == null)
            {
                return ResponseResult<TenantVM>.FailResponse(HttpStatusCode.NotFound, "Tenant not found");
            }
            return ResponseResult<TenantVM>.SuccessResponse(HttpStatusCode.OK, tenant, "Tenant retrieved successfully");
        }

        // POST: api/tenants
        [HttpPost]
        public ResponseResult<TenantVM> Create([FromBody] TenantInputVM tenantInput)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult<TenantVM>.FailResponse(HttpStatusCode.BadRequest, "Invalid tenant data");
            }

            var createdTenant = _tenantService.CreateTenant(tenantInput);
            return ResponseResult<TenantVM>.SuccessResponse(HttpStatusCode.Created, createdTenant, "Tenant created successfully");
        }

        // PUT: api/tenants/{id}
        [HttpPut("{id}")]
        public ResponseResult<TenantVM> Update(int id, [FromBody] TenantUpdateInputVM tenantUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult<TenantVM>.FailResponse(HttpStatusCode.BadRequest, "Invalid tenant data");
            }

            var updatedTenant = _tenantService.UpdateTenant(id, tenantUpdateInput);
            if (updatedTenant == null)
            {
                return ResponseResult<TenantVM>.FailResponse(HttpStatusCode.NotFound, "Tenant not found");
            }

            return ResponseResult<TenantVM>.SuccessResponse(HttpStatusCode.OK, updatedTenant, "Tenant updated successfully");
        }

        // DELETE: api/tenants/{id}
        [HttpDelete("{id}")]
        public ResponseResult<bool> Delete(int id)
        {
            var result = _tenantService.DeleteTenant(id);
            if (!result)
            {
                return ResponseResult<bool>.FailResponse(HttpStatusCode.NotFound, "Tenant not found or could not be deleted");
            }
            return ResponseResult<bool>.SuccessResponse(HttpStatusCode.OK, true, "Tenant deleted successfully");
        }
    }
}