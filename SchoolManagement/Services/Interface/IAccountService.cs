using SchoolManagement.ViewModel.Account;

namespace SchoolManagement.Services.Interface
{
    public interface IAccountService
    {
        // Retrieves all accounts from the database
        List<AccountResponseVM> GetAccountDetails();

        // Retrieves an account by its ID and Tenant ID
        AccountResponseVM GetAccountByIdAndTenantId(int id, int TenantId);

        // Retrieves an account by its ID
        AccountResponseVM GetAccountByID(int id);

        // Retrieves all accounts for a specific tenant
        List<AccountResponseVM> GetAccountByTenantId(int TenantId);

        // Creates a new account and saves it to the database
        AccountResponseVM CreateAccount(AccountRequestVM account);
        // Updates an existing account by ID and Tenant ID
        AccountResponseVM UpdateAccount(int id, int tenantId, AccountUpdateVM account);
        // Deletes an account by ID and Tenant ID, marking it as deleted
        bool DeleteAccountByIdAndTenantId(int id, int tenantId);
    }
}
