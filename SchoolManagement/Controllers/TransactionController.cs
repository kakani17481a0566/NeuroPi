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

        // transfer money between accounts 
        // POST api/transaction/transfer
        // Developed by : Mohith
        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransactionRequestVM request)
        {
            var result = _transactionService.Transfer(request);

            if (!result.IsSuccess)
            {
                return new ResponseResult<string>(HttpStatusCode.BadRequest, null, result.ErrorMessage);
            }

            var responseData = new
            {
                DebitTrxId = result.DebitTrxId,
                CreditTrxId = result.CreditTrxId,
                RefTransactionId = result.RefTrnsId
            };

            return new ResponseResult<object>(HttpStatusCode.OK, responseData, "Transaction successful");
        }

        // Get transaction by transaction ID
        // GET api/transaction/get-by-trxid
        // Developed by : Mohith
        [HttpGet("get-by-trxid")]
        public IActionResult GetByTrxId([FromQuery] int trxId)
        {
            var result = _transactionService.GetByTrxId(trxId);
            if (result == null)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<TransactionResponseVM>(HttpStatusCode.OK, result);
        }

        // Get transaction by transaction ID and tenant ID
        // GET api/transaction/get-by-trxid-tenant
        // Developed by : Mohith
        [HttpGet("get-by-trxid-tenant")]
        public IActionResult GetByTrxIdAndTenantId([FromQuery] int trxId, [FromQuery] int tenantId)
        {
            var result = _transactionService.GetByTrxIdAndTenantId(trxId, tenantId);
            if (result == null)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<TransactionResponseVM>(HttpStatusCode.OK, result);
        }

        // Get transactions by reference transaction ID
        // GET api/transaction/get-by-reftrnsid
        // Developed by : Mohith
        [HttpGet("get-by-reftrnsid")]
        public IActionResult GetByRefTrnsId([FromQuery] string refTrnsId)
        {
            var result = _transactionService.GetByRefTrnsId(refTrnsId);
            if (result == null || result.Count == 0)
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "Transaction not found.");

            return new ResponseResult<List<TransactionResponseVM>>(HttpStatusCode.OK, result);
        }

        // Update transaction amount by reference transaction ID and tenant ID
        // PUT api/transaction/update-amount-by-ref
        // Developed by : Mohith
        [HttpPut("update-amount-by-ref")]
        public IActionResult UpdateTrxAmountByRefAndTenant([FromBody] UpdateTrxAmountRequestVM request)
        {
            var isSuccess = _transactionService.UpdateAmountByRefTrnsIdAndTenant(
                request.RefTrnsId, request.TenantId, request.NewAmount, request.ModifiedBy);

            if (!isSuccess)
            {
                return new ResponseResult<string>(HttpStatusCode.NotFound, null, "No transactions found or update failed.");
            }

            return new ResponseResult<string>(HttpStatusCode.OK, null, "Transaction amounts updated successfully.");
        }
    }
}
