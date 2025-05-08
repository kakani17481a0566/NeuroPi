using Microsoft.AspNetCore.Mvc;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using NeuroPi.ViewModel.Tenent;
using System.Collections.Generic;
using System.Net;



        // GET: api/tenants
        [HttpGet]
        public ResponseResult<List<TenantViewModel>> GetAll()
        {
            var tenants = _tenantService.GetAllTenants();
            if (tenants == null || tenants.Count == 0)
            {
                return ResponseResult<List<TenantViewModel>>.FailResponse(HttpStatusCode.NotFound, "No tenants found");
            }
            return ResponseResult<List<TenantViewModel>>.SuccessResponse(HttpStatusCode.OK, tenants, "Tenants retrieved successfully");
        }

        // GET: api/tenants/{id}
        [HttpGet("{id}")]
        public ResponseResult<TenantViewModel> GetById(int id)
        {
            var tenant = _tenantService.GetTenantById(id);
            if (tenant == null)
            {
                return ResponseResult<TenantViewModel>.FailResponse(HttpStatusCode.NotFound, "Tenant not found");
            }
            return ResponseResult<TenantViewModel>.SuccessResponse(HttpStatusCode.OK, tenant, "Tenant retrieved successfully");
        }

        // POST: api/tenants
        [HttpPost]
        public ResponseResult<TenantViewModel> Create([FromBody] TenantInputModel tenantInput)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult<TenantViewModel>.FailResponse(HttpStatusCode.BadRequest, "Invalid tenant data");
            }

            var createdTenant = _tenantService.CreateTenant(tenantInput);
            return ResponseResult<TenantViewModel>.SuccessResponse(HttpStatusCode.Created, createdTenant, "Tenant created successfully");
        }

        // PUT: api/tenants/{id}
        [HttpPut("{id}")]
        public ResponseResult<TenantViewModel> Update(int id, [FromBody] TenantUpdateInputModel tenantUpdateInput)
        {
            if (!ModelState.IsValid)
            {
                return ResponseResult<TenantViewModel>.FailResponse(HttpStatusCode.BadRequest, "Invalid tenant data");
            }

            var updatedTenant = _tenantService.UpdateTenant(id, tenantUpdateInput);
            if (updatedTenant == null)
            {
                return ResponseResult<TenantViewModel>.FailResponse(HttpStatusCode.NotFound, "Tenant not found");
            }

            return ResponseResult<TenantViewModel>.SuccessResponse(HttpStatusCode.OK, updatedTenant, "Tenant updated successfully");
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
