using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalItemRecipe;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalItemRecipeController : ControllerBase
    {
        private readonly INutritionalItemRecipe _NutritionalItemRecipe;
        public NutritionalItemRecipeController(INutritionalItemRecipe NutritionalItemRecipe)
        {
            _NutritionalItemRecipe = NutritionalItemRecipe;
        }

        [HttpGet("GetNutritionalItemRecipees")]
        public ResponseResult<List<NutritionalItemRecipeResponseVM>> GetAllNutritionalItemRecipe()
        {
            var result = _NutritionalItemRecipe.GetAllNutritionalItemRecipe();
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemRecipeResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");

            }
            return new ResponseResult<List<NutritionalItemRecipeResponseVM>>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");

        }

        [HttpGet("GetNutritionalItemRecipeById/{id}")]
        public ResponseResult<NutritionalItemRecipeResponseVM> GetNutritionalItemRecipeById(int id)
        {
            var result = _NutritionalItemRecipe.GetNutritionalItemRecipeById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");
        }

        [HttpGet("GetNutritionalItemRecipeByIdAndTenantId/{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionalItemRecipeResponseVM> GetNutritionalItemRecipeByIdAndTenantId(int id, int tenantId)
        {
            var result = _NutritionalItemRecipe.GetNutritionalItemRecipeByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");
        }
        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<NutritionalItemRecipeResponseVM>> GetNutritionalItemRecipeByTenantId(int tenantId)
        {
            var result = _NutritionalItemRecipe.GetNutritionalItemRecipeByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionalItemRecipeResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<List<NutritionalItemRecipeResponseVM>>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");
        }
        [HttpPost("CreateNutritionalItemRecipe")]
        public ResponseResult<NutritionalItemRecipeResponseVM> CreateNutritionalItemRecipe([FromBody] NutritionalItemRecipeRequestVM request)
        {

            var result = _NutritionalItemRecipe.CreateNutritionalItemRecipe(request);
            if (result == null)
            {
                return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.InternalServerError, null, "Failed to create Nutritional Focus");
            }
            return new ResponseResult<NutritionalItemRecipeResponseVM>(HttpStatusCode.OK, result, "Nutritional Focus created successfully");
        }

        [HttpPut("DeleteNutritionalItemRecipeById/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalItemRecipeById(int id, int tenantId)
        {
            var result = _NutritionalItemRecipe.DeleteNutritionalItemRecipeById(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutritional Focus Not Found or could not be deleted");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutritional Focus deleted successfully");
        }

        [HttpDelete("DeleteNutritionalItemRecipeById/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalItemRecipeByIdUsingDelete(int id, int tenantId)
        {
            var result = _NutritionalItemRecipe.DeleteNutritionalItemRecipeById(id, tenantId);
            if (!result)

            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutritional Focus Not Found or could not be deleted");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutritional Focus deleted successfully");
        }

    }
}
