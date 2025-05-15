using System.Net;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.Config;

namespace NeuroPi.UserManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        // GET: api/Config
        [HttpGet]
        public ResponseResult<List<ConfigVM>> GetAllConfigs()
        {
            var result = _configService.GetAllConfigs();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<ConfigVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No configuration data found.");
            }

            return new ResponseResult<List<ConfigVM>>(
                HttpStatusCode.OK,
                result,
                "Configurations fetched successfully.");
        }

        // GET: api/Config/{id}
        [HttpGet("{id}")]
        public ResponseResult<ConfigVM> GetConfigById(int id)
        {
            var result = _configService.GetConfigById(id);
            if (result == null)
            {
                return new ResponseResult<ConfigVM>(HttpStatusCode.NotFound, null, "Configuration not found.");
            }

            return new ResponseResult<ConfigVM>(HttpStatusCode.OK, result, "Configuration fetched successfully.");
        }

        // GET: api/Config/{id}/{tenantId}
        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<ConfigVM> GetConfigByIdAndTenant(int id, int tenantId)
        {
            var result = _configService.GetConfigByIdAndTenant(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<ConfigVM>(HttpStatusCode.NotFound, null, "Configuration not found for given ID and TenantId.");
            }

            return new ResponseResult<ConfigVM>(HttpStatusCode.OK, result, "Configuration fetched successfully.");
        }

        // GET: api/Config/tenant/{tenantId}
        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<ConfigVM>> GetConfigsByTenantId(int tenantId)
        {
            var result = _configService.GetConfigsByTenantId(tenantId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<ConfigVM>>(HttpStatusCode.NotFound, null, "No configurations found for this TenantId.");
            }

            return new ResponseResult<List<ConfigVM>>(HttpStatusCode.OK, result, "Configurations fetched for TenantId successfully.");
        }

        // POST: api/Config
        [HttpPost]
        public ResponseResult<ConfigVM> CreateConfig([FromBody] ConfigCreateVM config)
        {
            var result = _configService.CreateConfig(config);
            if (result == null)
            {
                return new ResponseResult<ConfigVM>(HttpStatusCode.BadRequest, null, "Failed to create configuration.");
            }

            return new ResponseResult<ConfigVM>(HttpStatusCode.Created, result, "Configuration created successfully.");
        }

        // PUT: api/Config/{id}/{tenantId}
        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<ConfigVM> UpdateConfig(int id, int tenantId, [FromBody] ConfigUpdateVM config)
        {
            var existingConfig = _configService.GetConfigByIdAndTenant(id, tenantId);

            if (existingConfig == null)
            {
                return new ResponseResult<ConfigVM>(HttpStatusCode.NotFound, null, "Configuration not found or TenantId does not match.");
            }

            var result = _configService.UpdateConfig(id, config);

            if (result == null)
            {
                return new ResponseResult<ConfigVM>(HttpStatusCode.BadRequest, null, "Failed to update configuration.");
            }

            return new ResponseResult<ConfigVM>(HttpStatusCode.OK, result, "Configuration updated successfully.");
        }

        // DELETE: api/Config/{id}/{tenantId}
        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<object> DeleteConfig(int id, int tenantId)
        {
            var result = _configService.DeleteConfig(id, tenantId);
            if (result)
            {
                return new ResponseResult<object>(HttpStatusCode.OK, null, "Configuration deleted successfully.");
            }

            return new ResponseResult<object>(HttpStatusCode.BadRequest, null, "Configuration not found.");
        }
    }
}
