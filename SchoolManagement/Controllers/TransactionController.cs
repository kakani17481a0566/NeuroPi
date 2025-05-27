using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Transaction;
using System.Net;

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

        [HttpPost("transfer")]
        public IActionResult Transfer([FromBody] TransactionRequestVM request)
        {
            var result = _transactionService.Transfer(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new ResponseResult<string>(HttpStatusCode.BadRequest, null, result.ErrorMessage));
            }

            return new ResponseResult<object>(
                HttpStatusCode.OK,
                new
                {
                    DebitTrxId = result.DebitTrxId,
                    CreditTrxId = result.CreditTrxId,
                    RefTransactionId = result.RefTrnsId
                },
                "Transaction successful"
            );
        }

        [HttpGet("get-by-trxid")]
        public IActionResult GetByTrxId([FromQuery] int trxId)
        {
            var result = _transactionService.GetByTrxId(trxId);
            if (result == null)
                return NotFound(new { message = "Transaction not found." });

            return Ok(result);
        }

        [HttpGet("get-by-trxid-tenant")]
        public IActionResult GetByTrxIdAndTenantId([FromQuery] int trxId, [FromQuery] int tenantId)
        {
            var result = _transactionService.GetByTrxIdAndTenantId(trxId, tenantId);
            if (result == null)
                return NotFound(new { message = "Transaction not found." });

            return Ok(result);
        }

        [HttpGet("get-by-reftrnsid")]
        public IActionResult GetByRefTrnsId([FromQuery] string refTrnsId)
        {
            var result = _transactionService.GetByRefTrnsId(refTrnsId);
            if (result == null || result.Count == 0)
                return NotFound(new { message = "Transaction not found." });

            return Ok(result);
        }

        [HttpPut("update-amount-by-ref")]
        public IActionResult UpdateTrxAmountByRefAndTenant([FromBody] UpdateTrxAmountRequestVM request)
        {
            var isSuccess = _transactionService.UpdateAmountByRefTrnsIdAndTenant(
                request.RefTrnsId, request.TenantId, request.NewAmount, request.ModifiedBy);

            if (!isSuccess)
            {
                return new ResponseResult<string>(
                    HttpStatusCode.NotFound,
                    null,
                    "No transactions found or update failed."
                );
            }

            return new ResponseResult<string>(
                HttpStatusCode.OK,
                "Transaction amounts updated successfully.",
                "Success"
            );
        }
    }
}
