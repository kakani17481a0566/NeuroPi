using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.Nutrition.Services.Interface;
using NeuroPi.Nutrition.ViewModel.Genes;
using NeuroPi.CommonLib.Model;
using System.Net;

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

    }
}
