using Microsoft.AspNetCore.Mvc;
using NeuroPi.Models;
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
        public async Task<ActionResult<IEnumerable<MTenant>>> GetTenants()
        {
            var tenants = await _tenantService.GetAllTenantsAsync();
            return Ok(tenants);
        }
    }
}
