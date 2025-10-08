using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.FeeTransactions;
using SchoolManagement.Response;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeeTransactionsController : ControllerBase
    {
        private readonly IFeeTransactions _feeTransactionsService;

        public FeeTransactionsController(IFeeTransactions feeTransactionsService)
        {
            _feeTransactionsService = feeTransactionsService;
        }

        [HttpPost("build")]
        public ResponseResult<int> BuildFeeTransactions([FromBody] BuildFeeTransactionsRequest request)
        {
            // Service already returns ResponseResult<int>
            return _feeTransactionsService.BuildFeeTransactions(request);
        }

        [HttpGet("report-fee/{tenantId:int}/{studentId:int}")]
        public ResponseResult<FeeReportSummaryVM> GetStudentFeeReport(int tenantId, int studentId)
        {
            return _feeTransactionsService.GetStudentFeeReport(tenantId, studentId);
        }
    }
}
