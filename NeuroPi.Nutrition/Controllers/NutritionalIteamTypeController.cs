using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.Genes;
using NeuroPi.Nutrition.ViewModel.NutritionalFocus;
using NeuroPi.Nutrition.ViewModel.NutritionalIteamType;
using System.Net;

namespace NeuroPi.Nutrition.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalIteamTypeController : ControllerBase
    {
        private readonly INutritionalIteamType _NutritionalIteamTypeService;

        public NutritionalIteamTypeController(INutritionalIteamType NutritionalIteamTypeService)
        {
            _NutritionalIteamTypeService = NutritionalIteamTypeService;
        }
        [HttpGet]
        public ResponseResult<List<NutritionalIteamTypeResponseVM>> GetAllNutritionalFocus()
        {
            var result = _NutritionalIteamTypeService.GetAllNutritionalIteamType();
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<NutritionalIteamTypeResponseVM>>(
                    HttpStatusCode.NotFound,
                    null,
                    "No genes data found.");
            }
            return new ResponseResult<List<NutritionalIteamTypeResponseVM>>(
               HttpStatusCode.OK,
                result,
                "Genes fetched successfully.");
        }
    }
}
