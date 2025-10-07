using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Genres;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet("{tenantId}")]
        public ResponseResult<List<GenresResponseVM>> GetAllGenres(int tenantId)
        {
            var genres = _genreService.GetAllGenres(tenantId);
            if(genres!=null)
            {
                return new ResponseResult<List<GenresResponseVM>>(HttpStatusCode.OK, genres, "All genres retrieved successfully");
            }
            return new ResponseResult<List<GenresResponseVM>>(HttpStatusCode.NoContent, genres, "No data there for genres");
            
        }

    }
}
