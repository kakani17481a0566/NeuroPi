using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.LibraryTransctions;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryTransactionController : ControllerBase
    {
        private readonly ILibraryTransactionsService _libraryTransactionsService;

        public LibraryTransactionController(ILibraryTransactionsService libraryTransactionsService)
        {
            _libraryTransactionsService = libraryTransactionsService;
        }

        [HttpGet]

        public ResponseResult<List<LibraryTransactionResponseVM>> GetAllLibraryTransactions([FromQuery] int studentId)
        {
            var response = _libraryTransactionsService.GetAllLibraryTransactions(studentId);
            if (response == null || response.Count == 0)
            {
                return new ResponseResult<List<LibraryTransactionResponseVM>>(HttpStatusCode.NotFound, response, "No data Found for the specified student");
            }
            return new ResponseResult<List<LibraryTransactionResponseVM>>(HttpStatusCode.OK, response, "Library Transactions fetched successfully");
        }
    }
}
