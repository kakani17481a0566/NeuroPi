using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Book;
using System.Collections.Generic;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BooksController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        // GET: api/books/{tenantId}
        // Get all books for a specific tenant
        // Developed by: Mohith
        [HttpGet("{tenantId}")]
        public IActionResult GetAllBooks(int tenantId)
        {
            var books = _booksService.GetAllBooks(tenantId);
            return new ResponseResult<List<BookResponseVM>>(HttpStatusCode.OK, books);
        }
        //GET: api/books/{tenantId}/{id}
        // Get a book by its ID for a specific tenant
        // Developed by: Mohith
        [HttpGet("{tenantId}/{id}")]
        public IActionResult GetBookById(int tenantId, int id)
        {
            var book = _booksService.GetBookById(id, tenantId);
            if (book == null)
                return new ResponseResult<BookResponseVM>(HttpStatusCode.NotFound, null, "Book not found");

            return new ResponseResult<BookResponseVM>(HttpStatusCode.OK, book);
        }

        // POST: api/books
        // Create a new book
        // Developed by: Mohith
        [HttpPost]
        public IActionResult CreateBook([FromBody] BookRequestVM book)
        {
            if (book == null)
                return new ResponseResult<BookResponseVM>(HttpStatusCode.BadRequest, null, "Invalid book data");

            var createdBook = _booksService.CreateBook(book);

            return new ResponseResult<BookResponseVM>(HttpStatusCode.Created, createdBook, "Book created successfully");
        }

        // PUT: api/books/{tenantId}/{id}
        // Update an existing book
        // Developed by: Mohith
        [HttpPut("{tenantId}/{id}")]
        public IActionResult UpdateBook(int tenantId, int id, [FromBody] BookUpdateVM book)
        {
            if (book == null)
                return new ResponseResult<BookResponseVM>(HttpStatusCode.BadRequest, null, "Invalid book data");

            var updatedBook = _booksService.UpdateBook(id, tenantId, book);
            if (updatedBook == null)
                return new ResponseResult<BookResponseVM>(HttpStatusCode.NotFound, null, "Book not found");

            return new ResponseResult<BookResponseVM>(HttpStatusCode.OK, updatedBook, "Book updated successfully");
        }


        // DELETE: api/books/{tenantId}/{id}
        // Delete a book by its ID for a specific tenant
        // Developed by: Mohith
        [HttpDelete("{tenantId}/{id}")]
        public IActionResult DeleteBook(int tenantId, int id)
        {
            var deleted = _booksService.DeleteBook(id, tenantId);
            if (!deleted)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Book not found");

            return new ResponseResult<string>(HttpStatusCode.OK, "Deleted", "Book deleted successfully");
        }
    }
}
