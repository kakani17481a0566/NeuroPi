using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.Vitamins;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VitaminsController : ControllerBase
    {
        private readonly IVitamins _vitaminService;
        public VitaminsController(IVitamins vitaminService)
        {
            _vitaminService = vitaminService;
        }
        [HttpGet]
        public ResponseResult<List<VitaminsResponseVM>> GetAllVitamins()
        {
            var vitamins = _vitaminService.GetAllVitamins();
            if (vitamins == null || vitamins.Count == 0)
            {
                return new ResponseResult<List<VitaminsResponseVM>>(HttpStatusCode.NotFound, null, "No Vitamins Found");
            }
            return new ResponseResult<List<VitaminsResponseVM>>(HttpStatusCode.OK, vitamins, "Vitamins Retrived Sucessfully");

        }

        [HttpGet("id/{id}")]
        public ResponseResult<VitaminsResponseVM> GetVitaminById(int id)
        {
            var vitamin = _vitaminService.GetVitaminById(id);
            if (vitamin == null)
            {
                return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.NotFound, null, "Vitamin Not Found");
            }
            return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.OK, vitamin, "Vitamin Retrived Sucessfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<VitaminsResponseVM>> GetVitaminByTenantId(int tenantId)
        {
            var vitamins = _vitaminService.GetVitaminByTenantId(tenantId);
            if (vitamins == null || vitamins.Count == 0)
            {
                return new ResponseResult<List<VitaminsResponseVM>>(HttpStatusCode.NotFound, null, "No Vitamins Found for the Tenant");
            }
            return new ResponseResult<List<VitaminsResponseVM>>(HttpStatusCode.OK, vitamins, "Vitamins Retrived Sucessfully for the Tenant");
        }

        [HttpGet("id/{id}/tenantId{tenantId}")]
        public ResponseResult<VitaminsResponseVM> GetVitaminsByIdAndTenantID(int id, int tenantId)
        {
            var vitamin = _vitaminService.GetVitaminsByIdAndTenantID(id, tenantId);
            if (vitamin == null)
            {
                return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.NotFound, null, "Vitamin Not Found for the Tenant");
            }
            return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.OK, vitamin, "Vitamin Retrived Sucessfully for the Tenant");

        }
        [HttpPost]
        public ResponseResult<VitaminsResponseVM> CreateVitamin(VitaminsRequestVm vitaminRequest)
        {
            var createdVitamin = _vitaminService.CreateVitamin(vitaminRequest);
            return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.Created, createdVitamin, "Vitamin Created Sucessfully");
        }
        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<VitaminsResponseVM> UpdateVitamin(int id, int tenantId, VitaminsUpdateVM vitaminUpdate)
        {
            var updatedVitamin = _vitaminService.UpdateVitamin(id, tenantId, vitaminUpdate);
            if (updatedVitamin == null)
            {
                return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.NotFound, null, "Vitamin Not Found for Update");
            }
            return new ResponseResult<VitaminsResponseVM>(HttpStatusCode.OK, updatedVitamin, "Vitamin Updated Sucessfully");
        }
        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteVitamin(int id, int tenantId)
        {
            var isDeleted = _vitaminService.DeleteVitamin(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Vitamin not found.");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Vitamin deleted successfully.");
        }

    }
}