using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.CommonLib.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.PosTransactionDetail;
using SchoolManagement.ViewModel.PosTransactionMaster;
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
        public ResponseResult<PosInvoiceVM> CreatePostTransaction([FromBody] PosTransactionMasterRequestVM request)
        {
            PosInvoiceVM result=posTransactionMasterService.CreatePostTransaction(request);
            if (result != null)
            {
                return new ResponseResult<PosInvoiceVM>(HttpStatusCode.Created, result, "Created invoice successfully");
            }
            return new ResponseResult<PosInvoiceVM>(HttpStatusCode.BadGateway, result, "not created facing some issue");
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

        [HttpGet("GetSalesByBranchId/{branchId}")]
        public ResponseResult<List<PosTransactionMasterResponseVM>> GetSalesByBranchId(int branchId)
        {
            var result = posTransactionMasterService.GetSalesByBranchId(branchId);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<PosTransactionMasterResponseVM>>(HttpStatusCode.NotFound, result, $"No sales found for branchId {branchId}");
            }
            return new ResponseResult<List<PosTransactionMasterResponseVM>>(HttpStatusCode.OK, result, "Sales fetched successfully");
        }

        [HttpGet("GetSalesTrends/{branchId}/{period}")]
        public ResponseResult<List<SalesTrendVM>> GetSalesTrends(int branchId, string period)
        {
            var result = posTransactionMasterService.GetSalesTrends(branchId, period);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<SalesTrendVM>>(HttpStatusCode.NotFound, result, $"No sales trends found for branchId {branchId} for period {period}");
            }
            return new ResponseResult<List<SalesTrendVM>>(HttpStatusCode.OK, result, "Sales trends fetched successfully");
        }

        [HttpGet("GetTopSellers/{branchId}/{count}")]
        public ResponseResult<List<TopSellerVM>> GetTopSellers(int branchId, int count)
        {
            var result = posTransactionMasterService.GetTopSellers(branchId, count);
            if (result == null || result.Count == 0)
            {
                return new ResponseResult<List<TopSellerVM>>(HttpStatusCode.NotFound, result, $"No top sellers found for branchId {branchId}");
            }
            return new ResponseResult<List<TopSellerVM>>(HttpStatusCode.OK, result, "Top sellers fetched successfully");
        }

        [HttpGet("GetSalesSummary/{branchId}")]
        public ResponseResult<SalesSummaryVM> GetSalesSummary(int branchId)
        {
            var result = posTransactionMasterService.GetSalesSummary(branchId);
            if (result == null)
            {
                return new ResponseResult<SalesSummaryVM>(HttpStatusCode.NotFound, result, $"No sales summary found for branchId {branchId}");
            }
            return new ResponseResult<SalesSummaryVM>(HttpStatusCode.OK, result, "Sales summary fetched successfully");
        }

        [HttpGet("GetPaymentMethodStats/{branchId}")]
        public ResponseResult<List<PaymentMethodStatVM>> GetPaymentMethodStats(int branchId)
        {
            var result = posTransactionMasterService.GetPaymentMethodStats(branchId);
            return new ResponseResult<List<PaymentMethodStatVM>>(HttpStatusCode.OK, result, "Payment method stats fetched successfully");
        }

        [HttpGet("GetCourseSalesStats/{branchId}")]
        public ResponseResult<List<CourseSalesStatVM>> GetCourseSalesStats(int branchId)
        {
            var result = posTransactionMasterService.GetCourseSalesStats(branchId);
            return new ResponseResult<List<CourseSalesStatVM>>(HttpStatusCode.OK, result, "Course sales stats fetched successfully");
        }
    }
}
