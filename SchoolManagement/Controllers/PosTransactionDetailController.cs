using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PosTransactionDetail;
using System.Net;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PosTransactionDetailController : ControllerBase
    {
        private readonly IPosTransactionDetailService _posTransactionDetailService;

        public PosTransactionDetailController(IPosTransactionDetailService posTransactionDetailService)
        {
            _posTransactionDetailService = posTransactionDetailService;
        }

        [HttpGet("GetAllDetailsByMasterTransactionId/{masterTransactionId}")]
        public ResponseResult<PosTransactionDetailResponseVM> GetAllDetailsByMasterTransactionId(int masterTransactionId)
        {
            var result = _posTransactionDetailService.GetAllDetailsByMasterTransactionId(masterTransactionId);
            if (result == null)
            {
                return new ResponseResult<PosTransactionDetailResponseVM>(HttpStatusCode.NotFound, result, $"No transaction details found for studentId {masterTransactionId}");
            }
            return new ResponseResult<PosTransactionDetailResponseVM>(HttpStatusCode.OK, result, "Transaction details fetched successfully");
        }

    }
}
