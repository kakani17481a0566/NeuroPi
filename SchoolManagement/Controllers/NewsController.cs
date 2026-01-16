using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.News;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("financial")]
        public async Task<ResponseResult<List<NewsViewModel>>> GetFinancialNews()
        {
            var result = await _newsService.GetFinancialNewsAsync();

            if (result == null || !result.Any())
            {
                 return new ResponseResult<List<NewsViewModel>>(HttpStatusCode.NotFound, null, "No articles found.");
            }

            return new ResponseResult<List<NewsViewModel>>(HttpStatusCode.OK, result, "Financial news retrieved successfully");
        }
    }
}
