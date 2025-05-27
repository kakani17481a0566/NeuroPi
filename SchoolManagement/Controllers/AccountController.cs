using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeuroPi.UserManagment.Response;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Account;

namespace SchoolManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ResponseResult<List<AccountResponseVM>> GetAllAccounts()
        {
            var accounts = _accountService.GetAccountDetails();
            return new ResponseResult<List<AccountResponseVM>>(HttpStatusCode.OK, accounts, "All accounts retrieved successfully");
        }

        [HttpGet("{id}")]
        public ResponseResult<AccountResponseVM> GetAccountById(int id)
        {
            var account = _accountService.GetAccountByID(id);
            return account == null
                ? new ResponseResult<AccountResponseVM>(HttpStatusCode.NotFound, null, "Account not found")
                : new ResponseResult<AccountResponseVM>(HttpStatusCode.OK, account, "Account retrieved successfully");
        }

        [HttpGet("tenant/{tenantId}")]
        public ResponseResult<List<AccountResponseVM>> GetAccountByTenantId(int tenantId)
        {
            var account = _accountService.GetAccountByTenantId(tenantId);
            return account == null
                ? new ResponseResult<List<AccountResponseVM>>(HttpStatusCode.NotFound, null, "No account found for the specified tenant")
                : new ResponseResult<List<AccountResponseVM>>(HttpStatusCode.OK, account, "Account retrieved successfully");
        }

        [HttpGet("{id}/{tenantId}")]
        public ResponseResult<AccountResponseVM> GetAccountByIdAndTenantId(int id, int tenantId)
        {
            var account = _accountService.GetAccountByIdAndTenantId(id, tenantId);
            return account == null
                ? new ResponseResult<AccountResponseVM>(HttpStatusCode.NotFound, null, "Account not found for the specified ID and tenant")
                : new ResponseResult<AccountResponseVM>(HttpStatusCode.OK, account, "Account retrieved successfully");
        }

        [HttpPost]
        public ResponseResult<AccountResponseVM> CreateAccount([FromBody] AccountRequestVM account)
        {
            if (account == null)
            {
                return new ResponseResult<AccountResponseVM>(HttpStatusCode.BadRequest, null, "Invalid account data");
            }
            var createdAccount = _accountService.CreateAccount(account);
            return new ResponseResult<AccountResponseVM>(HttpStatusCode.Created, createdAccount, "Account created successfully");
        }

        [HttpPut("{id}/{tenantId}")]
        public ResponseResult<AccountResponseVM> UpdateAccount(int id, int tenantId, [FromBody] AccountUpdateVM account)
        {
            if (account == null)
            {
                return new ResponseResult<AccountResponseVM>(HttpStatusCode.BadRequest, null, "Invalid account data");
            }
            var updatedAccount = _accountService.UpdateAccount(id, tenantId, account);
            return updatedAccount == null
                ? new ResponseResult<AccountResponseVM>(HttpStatusCode.NotFound, null, "Account not found for the specified ID and tenant")
                : new ResponseResult<AccountResponseVM>(HttpStatusCode.OK, updatedAccount, "Account updated successfully");
        }

        [HttpDelete("{id}/{tenantId}")]
        public ResponseResult<bool> DeleteAccount(int id, int tenantId)
        {
            var isDeleted = _accountService.DeleteAccountByIdAndTenantId(id, tenantId);
            return isDeleted
                ? new ResponseResult<bool>(HttpStatusCode.OK, true, "Account deleted successfully")
                : new ResponseResult<bool>(HttpStatusCode.NotFound, false, "Account not found for the specified ID and tenant");

        }
    }
}
