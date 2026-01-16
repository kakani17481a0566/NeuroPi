using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Services.Interface;
using NeuroPi.UserManagment.ViewModel.NewsArticle;

namespace NeuroPi.UserManagment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsArticle _newsService;

        public NewsController(INewsArticle newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("financial")]
        public async Task<ActionResult<List<NewsResponseVM>>> GetNews()
        {
            var result = await _newsService.GetFinancialNewsAsync();

            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No articles found." });
            }

            return Ok(result);
        }
    }
}