using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PrefixSuffix;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrefixSuffixController : ControllerBase
    {
        private readonly IPrefixSuffixService _prefixSuffixService;
        public PrefixSuffixController(IPrefixSuffixService prefixSuffixService)
        {
            _prefixSuffixService = prefixSuffixService;
        }

        [HttpGet("GetAllPrefixSuffix")]
        public ResponseResult<List<PrefixSuffixResponseVM>> GetAllPrefixSuffix()
        {
            var response = _prefixSuffixService.GetAllPrefixSuffix();
            if (response == null || !response.Any())
            {
                return new ResponseResult<List<PrefixSuffixResponseVM>>(System.Net.HttpStatusCode.NotFound, response, "No data found");
            }
            return new ResponseResult<List<PrefixSuffixResponseVM>>(System.Net.HttpStatusCode.OK, response, "Prefix Suffixes fetched successfully");
        }

        [HttpGet("GetAllPrefixSuffixByTenantId/{tenantId}")]
        public ResponseResult<List<PrefixSuffixResponseVM>> GetAllPrefixSuffixByTenantId(int tenantId)
        {
            var response = _prefixSuffixService.GetAllPrefixSuffixByTenantId(tenantId);
            if (response == null || !response.Any())
            {
                return new ResponseResult<List<PrefixSuffixResponseVM>>(System.Net.HttpStatusCode.NotFound, response, "No data found for the specified tenant");
            }
            return new ResponseResult<List<PrefixSuffixResponseVM>>(System.Net.HttpStatusCode.OK, response, "Prefix Suffixes for tenant fetched successfully");
        }


        [HttpGet("GetPrefixSuffixById/{id}")]
        public ResponseResult<PrefixSuffixResponseVM> GetPrefixSuffixById(int id)
        {
            var response = _prefixSuffixService.GetPrefixSuffixById(id);
            if (response == null)
            {
                return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.NotFound, response, $"Prefix Suffix not found with id {id}");
            }
            return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.OK, response, "Prefix Suffix fetched successfully");
        }

        [HttpGet("GetPrefixSuffixByIdAndTenantId/{id}/{tenantId}")]
        public ResponseResult<PrefixSuffixResponseVM> GetPrefixSuffixByIdAndTenantId(int id, int tenantId)
        {
            var response = _prefixSuffixService.GetPrefixSuffixByIdAndTenantId(id, tenantId);
            if (response == null)
            {
                return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.NotFound, response, $"Prefix Suffix not found with id {id} for tenant {tenantId}");
            }
            return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.OK, response, "Prefix Suffix for tenant fetched successfully");
        }


        [HttpPost("AddPrefixSuffix")]
        public ResponseResult<PrefixSuffixResponseVM> AddPrefixSuffix([FromBody] PrefixSuffixRequestVM prefixSuffixAddVM)
        {
            if (prefixSuffixAddVM == null)
            {
                return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.BadRequest, null, "Invalid request data");
            }
            var response = _prefixSuffixService.AddPrefixSuffix(prefixSuffixAddVM);
            return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.Created, response, "Prefix Suffix added successfully");
        }
        [HttpPut("UpdatePrefixSuffix/{id}/{tenantId}")]
        public ResponseResult<PrefixSuffixResponseVM> UpdatePrefixSuffix(int id, int tenantId, [FromBody] PrefixSuffixUpdateVM prefixSuffix)
        {
            if (prefixSuffix == null)
            {
                return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.BadRequest, null, "Invalid request data");
            }
            var response = _prefixSuffixService.UpdatePrefixSuffix(id, tenantId, prefixSuffix);
            if (response == null)
            {
                return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.NotFound, null, $"Prefix Suffix not found with id {id} for tenant {tenantId}");
            }
            return new ResponseResult<PrefixSuffixResponseVM>(System.Net.HttpStatusCode.OK, response, "Prefix Suffix updated successfully");
        }


        [HttpDelete("DeletePrefixSuffix/{id}/{tenantId}")]
        public ResponseResult<bool> DeletePrefixSuffix(int id, int tenantId)
        {
            var isDeleted = _prefixSuffixService.DeletePrefixSuffix(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(System.Net.HttpStatusCode.NotFound, false, $"Prefix Suffix not found with id {id} for tenant {tenantId}");
            }
            return new ResponseResult<bool>(System.Net.HttpStatusCode.OK, true, "Prefix Suffix deleted successfully");
        }
    }
}