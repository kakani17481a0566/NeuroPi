using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.LibraryBookTitle;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemHeaderController : ControllerBase
    {
        private readonly ILibraryBookTitleService _libraryBookTitleService;

        public ItemHeaderController(ILibraryBookTitleService libraryBookTitleService)
        {
            _libraryBookTitleService = libraryBookTitleService;
        }

        [HttpGet("{tenantId}")]
        public ResponseResult<List<LibraryBookTitleResponseVM>> GetAllByTenantId([FromRoute] int tenantId)
        {
            var response = _libraryBookTitleService.GetAllByTenantId(tenantId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<LibraryBookTitleResponseVM>>(HttpStatusCode.OK, new List<LibraryBookTitleResponseVM>(), "No data Found");
            }
            return new ResponseResult<List<LibraryBookTitleResponseVM>>(HttpStatusCode.OK, response, "Item Headers fetched successfully");
        }

        [HttpPost]
        public IActionResult Create([FromBody] LibraryBookTitleRequestVM request)
        {
            var result = _libraryBookTitleService.CreateLibraryBookTitle(request);
            return new ResponseResult<LibraryBookTitleResponseVM>(HttpStatusCode.Created, result, "Book created successfully");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LibraryBookTitleRequestVM request)
        {
            var result = _libraryBookTitleService.UpdateLibraryBookTitle(id, request);
            if (result == null)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Book not found");
            }
            return new ResponseResult<LibraryBookTitleResponseVM>(HttpStatusCode.OK, result, "Book updated successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _libraryBookTitleService.DeleteLibraryBookTitle(id);
             if (!result)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Book not found");
            }
            return new ResponseResult<bool>(HttpStatusCode.OK, result, "Book deleted successfully");
        }
    }
}
