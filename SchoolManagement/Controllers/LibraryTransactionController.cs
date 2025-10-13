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

        public ResponseResult<LibraryTransactionResponseVM> GetAllLibraryTransactions([FromQuery] int studentId)
        {
            var response = _libraryTransactionsService.GetAllLibraryTransactions(studentId);
            if (response == null)
            {
                return new ResponseResult<LibraryTransactionResponseVM>(HttpStatusCode.NotFound, response, "No data Found for the specified student");
            }
            return new ResponseResult<LibraryTransactionResponseVM>(HttpStatusCode.OK, response, "Library Transactions fetched successfully");
        }

        [HttpPost]
        public ResponseResult<string> CreateLibraryTransaction([FromBody] LibraryTransactionRequestVM ltRequestVm)
        {
            var response = _libraryTransactionsService.CreateLibraryTransaction(ltRequestVm);
            if (response == "inserted")
            {
                return new ResponseResult<string>(HttpStatusCode.OK, response, "Library Transaction created successfully");
            }
            return new ResponseResult<string>(HttpStatusCode.BadRequest, response, "Failed to create Library Transaction");

        }
        [HttpPut]
        public ResponseResult<string> UpdateLibraryTransaction([FromBody] LibraryTransactionUpdateVM updateVM)
        {
            var response = _libraryTransactionsService.UpdateLibraryTransaction(updateVM);
            if (response != null)
            {
                return new ResponseResult<string>(HttpStatusCode.OK,response,"cheked out successfully");
            }
            return new ResponseResult<string>(HttpStatusCode.GatewayTimeout, response, "please try again after some time");
        }
    }
}
