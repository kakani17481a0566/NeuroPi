using SchoolManagement.ViewModel.Account;

namespace SchoolManagement.Services.Interface
{
    public interface IAccountService
    {
        List<AccountResponseVM> GetAccountDetails();

        AccountResponseVM GetAccountByIdAndTenantId(int id, int TenantId);

        AccountResponseVM GetAccountByID(int id);

       List<AccountResponseVM> GetAccountByTenantId(int TenantId);

        AccountResponseVM CreateAccount(AccountRequestVM account);

        AccountResponseVM UpdateAccount(int id, int tenantId, AccountUpdateVM account);

        bool DeleteAccountByIdAndTenantId(int id, int tenantId);
    }
}
