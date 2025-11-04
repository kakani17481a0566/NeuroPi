using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Nutrition.ViewModel.Genes;
using NeuroPi.CommonLib.Model;
using System.Net;
using NeuroPi.Nutrition.Services.Interface;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenGenesController : ControllerBase
    {
        private readonly IGenGenesService _genGenesService;
        public GenGenesController(IGenGenesService genGenesService)
        {
            _genGenesService = genGenesService;
        }

        [HttpGet]
        public ResponseResult<List<GenGenesResponseVM>> GetAllGenes()
        {
            var result = _genGenesService.GetAllGenes();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<GenGenesResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No genes data found.");
            }
            return new ResponseResult<List<GenGenesResponseVM>>(
               HttpStatusCode.OK,
                result,
                "Genes fetched successfully.");
        }

        [HttpGet("{id}")]
        public ResponseResult<GenGenesResponseVM> GetGenesById(int id)
        {
            var result = _genGenesService.GetGenesById(id);
            if (result == null)
            {
                return new ResponseResult<GenGenesResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Genes not found for the given id.");
            }
            return new ResponseResult<GenGenesResponseVM>(
                HttpStatusCode.OK, result, "Genes fetched successfully.");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<GenGenesResponseVM>> GetGenesByTenantId(int tenantId)
        {
            var result = _genGenesService.GetGenesByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<GenGenesResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "Genes not found for the given tenant id.");
            }
            return new ResponseResult<List<GenGenesResponseVM>>(
                HttpStatusCode.OK, result, "Genes fetched successfully.");

        }

        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<GenGenesResponseVM> GetGenesByIdAndTenantId(int id, int tenantId)
        {
            var result = _genGenesService.GetGenesByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<GenGenesResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Genes not found for the given id and tenant id.");
            }
            return new ResponseResult<GenGenesResponseVM>(
                HttpStatusCode.OK, result, "Genes fetched successfully.");
        }

        [HttpPost]
        public ResponseResult<GenGenesResponseVM> CreateGenes(GenGenesRequestVM request)
        {
            var result = _genGenesService.CreateGenes(request);
            return new ResponseResult<GenGenesResponseVM>(
                HttpStatusCode.Created, result, "Genes created successfully.");
        }
        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<GenGenesResponseVM> UpdateGenesByIdAndTenantId(int id, int tenantId, GenGenesUpdateVM update)
        {
            var result = _genGenesService.UpdateGenesByIdAndTenantId(id, tenantId, update);
            if (result == null)
            {
                return new ResponseResult<GenGenesResponseVM>(
                    HttpStatusCode.NotFound,
                    null,
                    "Genes not found for the given id and tenant id.");
            }
            return new ResponseResult<GenGenesResponseVM>(
                HttpStatusCode.OK, result, "Genes updated successfully.");
        }
        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteGenesById(int id, int tenantId)
        {
            var isDeleted = _genGenesService.DeleteGenesById(id, tenantId);
            if (!isDeleted)
            {
                return new ResponseResult<bool>(
                    HttpStatusCode.NotFound,
                    false,
                    "Genes not found for the given id and tenant id.");
            }
            return new ResponseResult<bool>(
                HttpStatusCode.OK, true, "Genes deleted successfully.");
        }
    }
}