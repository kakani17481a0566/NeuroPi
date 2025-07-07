using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Transaction;
using System.Net;
using System.Collections.Generic;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST: api/transaction/transfer
        [HttpPost("transfer")]
        public ResponseResult<object> Transfer([FromBody] TransactionRequestVM request)
        {
            var result = _transactionService.Transfer(request);

            if (!result.IsSuccess)
            {
                return new ResponseResult<object>(HttpStatusCode.BadRequest, null, result.ErrorMessage);
            }

            var responseData = new
            {
                DebitTrxId = result.DebitTrxId,
                CreditTrxId = result.CreditTrxId,
                RefTransactionId = result.RefTrnsId
            };

            return new ResponseResult<object>(HttpStatusCode.OK, responseData, "Transaction successful");
        }

        // GET: api/transaction/get-by-trxid
        [HttpGet("get-by-trxid")]
        public ResponseResult<TransactionResponseVM> GetByTrxId([FromQuery] int trxId)
        {
            var result = _transactionService.GetByTrxId(trxId);
            if (result == null)
                return new ResponseResult<TransactionResponseVM>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<TransactionResponseVM>(HttpStatusCode.OK, result);
        }

        // GET: api/transaction/get-by-trxid-tenant
        [HttpGet("get-by-trxid-tenant")]
        public ResponseResult<TransactionResponseVM> GetByTrxIdAndTenantId([FromQuery] int trxId, [FromQuery] int tenantId)
        {
            var result = _transactionService.GetByTrxIdAndTenantId(trxId, tenantId);
            if (result == null)
                return new ResponseResult<TransactionResponseVM>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<TransactionResponseVM>(HttpStatusCode.OK, result);
        }

        // GET: api/transaction/get-by-reftrnsid
        [HttpGet("get-by-reftrnsid")]
        public ResponseResult<List<TransactionResponseVM>> GetByRefTrnsId([FromQuery] string refTrnsId)
        {
            var result = _transactionService.GetByRefTrnsId(refTrnsId);
            if (result == null || result.Count == 0)
                return new ResponseResult<List<TransactionResponseVM>>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<List<TransactionResponseVM>>(HttpStatusCode.OK, result);
        }

        // PUT: api/transaction/update-amount-by-ref
        [HttpPut("update-amount-by-ref")]
        public ResponseResult<string> UpdateTrxAmountByRefAndTenant([FromBody] UpdateTrxAmountRequestVM request)
        {
            var isSuccess = _transactionService.UpdateAmountByRefTrnsIdAndTenant(
                request.RefTrnsId, request.TenantId, request.NewAmount, request.ModifiedBy);

            if (!isSuccess)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "No transactions found or update failed.");
            }

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Transaction amounts updated successfully.");
        }

        // GET: api/transaction/table/tenant/{tenantId}
        [HttpGet("table/tenant/{tenantId}")]
        public ResponseResult<VTransactionTableVM> GetFormattedTransactionTable(int tenantId)
        {
            return _transactionService.GetFormattedTransactionTable(tenantId);
        }
    }
}
