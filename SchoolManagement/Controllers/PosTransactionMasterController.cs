using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PostTransactionMaster;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosTransactionMasterController : ControllerBase
    {
        private readonly IPosTransactionMasterService posTransactionMasterService;

        public PosTransactionMasterController(IPosTransactionMasterService posTransactionMasterService)
        {
            this.posTransactionMasterService = posTransactionMasterService;
        }

        [HttpPost("CreatePostTransaction")]
        public string CreatePostTransaction([FromBody] PosTransactionMasterRequestVM request)
        {
            return posTransactionMasterService.CreatePostTransaction(request);
        }

        [HttpGet("GetPostTransactionById/{studentId}")]
        public ResponseResult<List<PosTransactionMasterResponseVM>> GetPostTransactionById(int studentId)
        {
            var result = posTransactionMasterService.GetPostTransactionById(studentId);
           if (result == null)
            {
                return new ResponseResult<List<PosTransactionMasterResponseVM>>(HttpStatusCode.NotFound, result, $"No transaction found for studentId {studentId}");


            }
           return new ResponseResult<List<PosTransactionMasterResponseVM>>(HttpStatusCode.OK, result, "Transaction fetched successfully");
        }



    }
}
