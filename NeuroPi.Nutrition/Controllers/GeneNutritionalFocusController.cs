
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneNutritionalFocusController : ControllerBase
    {
        private readonly IGeneNutritionalFocus _geneNutritionalFocus;
        public GeneNutritionalFocusController(IGeneNutritionalFocus geneNutritionalFocus)
        {
            _geneNutritionalFocus = geneNutritionalFocus;
        }
        [HttpGet]
        public ResponseResult<List<GeneNutritionalFocusResponseVM>> GetGeneNutritionalFocus()
        {
            var result = _geneNutritionalFocus.GetAllGeneNutritionalFocus();
            if (result == null)
            {
                return new ResponseResult<List<GeneNutritionalFocusResponseVM>>(HttpStatusCode.NotFound, null, "Genes Nutritional Focus not found");

            }
            return new ResponseResult<List<GeneNutritionalFocusResponseVM>>(HttpStatusCode.OK, result, "Genes Nutritional Focus fetched successfully");

        }
        [HttpGet("{id}")]
        public ResponseResult<GeneNutritionalFocusResponseVM> GetGeneNutritionalFocus(int id)
        {
            var result = _geneNutritionalFocus.GetGeneNutritionalFocusById(id);
            if (result == null)
            {
                return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.NotFound, null, "Genes Nutritional Focus not found");
            }
            return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Genes Nutritional Focus fetched successfully");
        }

        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<GeneNutritionalFocusResponseVM>> GetGeneNutritionalFocusByTenantId(int tenantId)
        {
            var result = _geneNutritionalFocus.GetGeneNutritionalFocusByTenantId(tenantId);
            if (result != null)
            {
                return new ResponseResult<List<GeneNutritionalFocusResponseVM>>(HttpStatusCode.OK, result, "Gene Nutritional focus details fetched successfully");
            }
            return new ResponseResult<List<GeneNutritionalFocusResponseVM>>(HttpStatusCode.NotFound, null, "Gene Nutritional focus details not found");

        }

        [HttpGet("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<GeneNutritionalFocusResponseVM> GetGeneNutritionalFocusByIdAndTenantId(int id, int tenantId)
        {
            var result = _geneNutritionalFocus.GetGeneNutritionalFocusByIdAndTenantId(id, tenantId);
            if (result != null)
            {
                return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Gene Nutritional focus details fetched successfully");
            }
            return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.NotFound, null, "Gene Nutritional focus details not found");

        }

        [HttpPost]
        public ResponseResult<GeneNutritionalFocusResponseVM> CreateGeneNutritionalFocus([FromBody] GeneNutritionalFocusRequestVM request)
        {
            var result = _geneNutritionalFocus.CreateGeneNutritionalFocus(request);
            return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.Created, result, "Gene Nutritional focus created successfully");

        }
        [HttpPut("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<GeneNutritionalFocusResponseVM> UpdateGeneNutritionalFocusByIdAndTenantId(int id, int tenantId, [FromBody] GeneNutritionalFocusUpdateVM update)
        {
            var result = _geneNutritionalFocus.UpdateGeneNutritionalFocusByIdAndTenantId(id, tenantId, update);
            if (result != null)
            {
                return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Gene Nutritional focus updated successfully");
            }
            return new ResponseResult<GeneNutritionalFocusResponseVM>(HttpStatusCode.NotFound, null, "Gene Nutritional focus not found");
        }

        [HttpDelete("id/{id}/tenantId/{tenantId}")]
        public ResponseResult<bool> DeleteGeneNutritionalFocusByIdAndTenantId(int id, int tenantId)
        {
            var result = _geneNutritionalFocus.DeleteGeneNutritionalFocusById(id, tenantId);
            if (result)
            {
                return new ResponseResult<bool>(HttpStatusCode.OK, true, "Gene Nutritional focus deleted successfully");
            }
            return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Gene Nutritional focus not found");
        }
    }
}
