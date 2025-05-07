using Microsoft.AspNetCore.Mvc;
using NeuroPi.Models;
using NeuroPi.Response;
using NeuroPi.Services.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ResponseResult<List<MTenant>>> GetTenants()
        {
            var result = await _tenantService.GetAllTenantsAsync();
            return ResponseResult<List<MTenant>>.Success(result, "Tenants fetched successfully");
        }

    }
}
