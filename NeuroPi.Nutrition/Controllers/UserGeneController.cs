using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.UserGene;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGeneController : ControllerBase
    {
        private readonly IUserGene _UserGeneService;
        public UserGeneController(IUserGene UserGeneService)
        {
            _UserGeneService = UserGeneService;
        }

        [HttpGet]
        public ResponseResult<List<UserGeneResponseVM>> GetUserGene()
        {
            var result = _UserGeneService.GetUserGene();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<UserGeneResponseVM>>(HttpStatusCode.NotFound, null, "No User Genes found.");

            }
            return new ResponseResult<List<UserGeneResponseVM>>(HttpStatusCode.OK, result, "User Genes fetched successfully.");

        }

        [HttpGet("id/{id}")]
        public ResponseResult<UserGeneResponseVM> GetUserGeneById(int id)
        {
            var result = _UserGeneService.GetUserGeneById(id);
            if (result == null)
            {
                return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.NotFound, null, "User Gene not found.");
            }
            return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.OK, result, "User Gene fetched successfully.");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<UserGeneResponseVM>> GetUserGeneByTenantId(int tenantId)
        {
            var result = _UserGeneService.GetUserGeneByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<UserGeneResponseVM>>(HttpStatusCode.NotFound, null, "No User Genes found for the tenant.");
            }
            return new ResponseResult<List<UserGeneResponseVM>>(HttpStatusCode.OK, result, "User Genes fetched successfully for the tenant.");
        }

        [HttpGet("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserGeneResponseVM> GetUserGeneByIdAndTenantId(int id, int tenantId)
        {
            var result = _UserGeneService.GetUserGeneByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.NotFound, null, "User Gene not found for the provided TenantId and Id");
            }
            return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.OK, result, "User Gene fetched successfully for the tenant.");
        }

        [HttpPost]
        public ResponseResult<UserGeneResponseVM> CreateUserGene([FromBody] UserGeneRequestVM UserGeneRequestVM)
        {
            if (UserGeneRequestVM == null)
            {
                return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.BadRequest, null, "Invalid request.");
            }
            var result = _UserGeneService.CreateUserGene(UserGeneRequestVM);
            return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.OK, result, "User Gene created successfully.");
        }
        [HttpPut("id/{id}/tenant/{tenantId}")]
        public ResponseResult<UserGeneResponseVM> UpdateUserGene(int id, int tenantId, [FromBody] UserGeneUpdateVM UserGeneUpdateVM)
        {

            var result = _UserGeneService.UpdateUserGene(id, tenantId, UserGeneUpdateVM);
            if (result == null)
            {
                return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.NotFound, null, "User Gene not found.");
            }
            return new ResponseResult<UserGeneResponseVM>(HttpStatusCode.OK, result, "User Gene updated successfully.");
        }
        [HttpDelete("id/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteUserGene(int id, int tenantId)
        {
            var isDeleted = _UserGeneService.DeleteUserGene(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "User Gene not found.");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "User Gene deleted successfully.");
        }
    }
}