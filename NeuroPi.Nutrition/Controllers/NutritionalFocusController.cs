using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Interface;
using NeuroPi.Nutrition.ViewModel.NutritionalFocus;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalFocusController : ControllerBase
    {
        private readonly INutritionalFocus _nutritionalFocus;
        public NutritionalFocusController(INutritionalFocus nutritionalFocus)
        {
            _nutritionalFocus = nutritionalFocus;
        }

        [HttpGet("GetNutritionalFocuses")]
        public ResponseResult<List<NutritionalFocusResponseVM>> GetAllNutritionalFocus()
        {
            var result = _nutritionalFocus.GetAllNutritionalFocus();
            if (result == null)
            {
                return new ResponseResult<List<NutritionalFocusResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");

            }
            return new ResponseResult<List<NutritionalFocusResponseVM>>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");

        }

        [HttpGet("GetNutritionalFocusById/{id}")]
        public ResponseResult<NutritionalFocusResponseVM> GetNutritionalFocusById(int id)
        {
            var result = _nutritionalFocus.GetNutritionalFocusById(id);
            if (result == null)
            {
                return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");
        }

        [HttpGet("GetNutritionalFocusByIdAndTenantId/{id}/tenant/{tenantId}")]
        public ResponseResult<NutritionalFocusResponseVM> GetNutritionalFocusByIdAndTenantId(int id, int tenantId)
        {
            var result = _nutritionalFocus.GetNutritionalFocusByIdAndTenantId(id, tenantId);
            if (result == null)
            {
                return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Nutritional focus fetched successfully");
        }
        [HttpGet("tenantId/{tenantId}")]
        public ResponseResult<List<NutritionalFocusResponseVM>> GetNutritionalFocusByTenantId(int tenantId)
        {
            var result = _nutritionalFocus.GetNutritionalFocusByTenantId(tenantId);
            if (result == null)
            {
                return new ResponseResult<List<NutritionalFocusResponseVM>>(HttpStatusCode.NotFound, null, "Nutritional Focus Not Found");
            }
            return new ResponseResult<List<NutritionalFocusResponseVM>>(HttpStatusCode.OK,result, "Nutritional focus fetched successfully");
        }
        [HttpPost("CreateNutritionalFocus")]
        public ResponseResult<NutritionalFocusResponseVM> CreateNutritionalFocus([FromBody] NutritionalFocusRequestVM request)
        {

            var result = _nutritionalFocus.CreateNutritionalFocus(request);
            if (result == null)
            {
                return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.InternalServerError, null, "Failed to create Nutritional Focus");
            }
            return new ResponseResult<NutritionalFocusResponseVM>(HttpStatusCode.OK, result, "Nutritional Focus created successfully");
        }

        [HttpPut("DeleteNutritionalFocusById/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalFocusById(int id, int tenantId)
        {
            var result = _nutritionalFocus.DeleteNutritionalFocusById(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutritional Focus Not Found or could not be deleted");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutritional Focus deleted successfully");
        }

        [HttpDelete("DeleteNutritionalFocusById/{id}/tenant/{tenantId}")]
        public ResponseResult<bool> DeleteNutritionalFocusByIdUsingDelete(int id, int tenantId)
        {
            var result = _nutritionalFocus.DeleteNutritionalFocusById(id, tenantId);
            if (!result)
            {
                return new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Nutritional Focus Not Found or could not be deleted");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, true, "Nutritional Focus deleted successfully");
        }
   
    }
}