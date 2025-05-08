//using Microsoft.AspNetCore.Mvc;
//using NeuroPi.Response;
//using NeuroPi.Services.Interface;
//using NeuroPi.ViewModel.Tenent;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Threading.Tasks;

//namespace NeuroPi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TenantsController : ControllerBase
//    {
//        private readonly ITenantService _tenantService;

//        public TenantsController(ITenantService tenantService)
//        {
//            _tenantService = tenantService;
//        }

//        // GET: api/tenants
//        [HttpGet]
//        public async Task<ResponseResult<List<TenantViewModel>>> GetAll()
//        {
//            try
//            {
//                var tenants = await _tenantService.GetAllTenantsAsync();
//                return tenants == null || tenants.Count == 0
//                    ? ResponseResult<List<TenantViewModel>>.FailResponse("No tenants found")
//                    : ResponseResult<List<TenantViewModel>>.SuccessResponse(tenants, "Tenants retrieved successfully");
//            }
//            catch (Exception ex)
//            {
//                return ResponseResult<List<TenantViewModel>>.FailResponse($"Error retrieving tenants: {ex.Message}");
//            }
//        }

//        // GET: api/tenants/5
//        [HttpGet("{id}")]
//        public async Task<ResponseResult<TenantViewModel>> GetById(int id)
//        {
//            try
//            {
//                var tenant = await _tenantService.GetTenantByIdAsync(id);
//                return tenant == null
//                    ? ResponseResult<TenantViewModel>.FailResponse("Tenant not found")
//                    : ResponseResult<TenantViewModel>.SuccessResponse(tenant, "Tenant retrieved successfully");
//            }
//            catch (Exception ex)
//            {
//                return ResponseResult<TenantViewModel>.FailResponse($"Error retrieving tenant: {ex.Message}");
//            }
//        }

//        // POST: api/tenants
//        [HttpPost]
//        public async Task<ResponseResult<TenantViewModel>> Create([FromBody] TenantInputModel tenantInput)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                    return ResponseResult<TenantViewModel>.FailResponse("Invalid tenant data");

//                var createdTenant = await _tenantService.CreateTenantAsync(tenantInput);
//                return ResponseResult<TenantViewModel>.SuccessResponse(createdTenant, "Tenant created successfully");
//            }
//            catch (Exception ex)
//            {
//                return ResponseResult<TenantViewModel>.FailResponse($"Error creating tenant: {ex.Message}");
//            }
//        }

//        // PUT: api/tenants/5
//        [HttpPut("{id}")]
//        public async Task<ResponseResult<TenantViewModel>> Update(int id, [FromBody] TenantUpdateInputModel tenantUpdateInput)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                    return ResponseResult<TenantViewModel>.FailResponse("Invalid tenant data");

//                var updatedTenant = await _tenantService.UpdateTenantAsync(id, tenantUpdateInput);
//                if (updatedTenant == null)
//                    return ResponseResult<TenantViewModel>.FailResponse("Tenant not found");

//                return ResponseResult<TenantViewModel>.SuccessResponse(updatedTenant, "Tenant updated successfully");
//            }
//            catch (Exception ex)
//            {
//                return ResponseResult<TenantViewModel>.FailResponse($"Error updating tenant: {ex.Message}");
//            }
//        }

//        // DELETE: api/tenants/5
//        [HttpDelete("{id}")]
//        public async Task<ResponseResult<bool>> Delete(int id)
//        {
//            try
//            {
//                var result = await _tenantService.DeleteTenantAsync(id);
//                return result
//                    ? ResponseResult<bool>.SuccessResponse(true, "Tenant deleted successfully")
//                    : ResponseResult<bool>.FailResponse("Tenant not found or could not be deleted");
//            }
//            catch (Exception ex)
//            {
//                return ResponseResult<bool>.FailResponse($"Error deleting tenant: {ex.Message}");
//            }
//        }
//    }
//}
