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

        [HttpGet("recent/{tenantId:int}/{branchId:int}/{courseId:int}")]
        public ResponseResult<List<FeeReportTransactionVM>> GetRecentTransactions(
            int tenantId, 
            int branchId, 
            int courseId, 
            [FromQuery] int limit = 10)
        {
            return _feeTransactionsService.GetRecentTransactions(tenantId, branchId, courseId, limit);
        }

        [HttpGet("branch-stats/{tenantId:int}/{branchId:int}")]
        public ResponseResult<FeeStatsVM> GetBranchFeeStats(int tenantId, int branchId)
        {
            return _feeTransactionsService.GetBranchFeeStats(tenantId, branchId);
        }

        [HttpPost("add-payment")]
        public ResponseResult<int> AddPayment([FromBody] AddPaymentRequest request)
        {
            return _feeTransactionsService.AddPayment(request);
        }

        [HttpPost("add-bill")]
        public ResponseResult<int> AddBill([FromBody] AddBillRequest request)
        {
            return _feeTransactionsService.AddBill(request);
        }
    }
}
