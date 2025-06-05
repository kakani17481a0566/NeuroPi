using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Account;

//code written by Kiran on 27-05-2025

namespace SchoolManagement.Services.Implementation
{
    public class AccountServiceImpl : IAccountService
    {
        private readonly SchoolManagementDb _context;

        public AccountServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        // Creates a new account and saves it to the database
        // Developed by: Kiran
        public AccountResponseVM CreateAccount(AccountRequestVM account)
        {
            var newAccount = AccountRequestVM.ToModel(account);
            newAccount.CreatedOn = DateTime.UtcNow;
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            return AccountResponseVM.ToViewModel(newAccount);
        }

        // Deletes an account by ID and marks it as deleted
        // Developed by: Kiran
        public bool DeleteAccountByIdAndTenantId(int id, int tenantId)
        {
            var account = _context.Accounts
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (account == null)
            {
                return false;
            }
            account.IsDeleted = true;
            account.UpdatedOn = DateTime.UtcNow;
            _context.Accounts.Update(account);
            _context.SaveChanges();
            return true;
        }

        // Retrieves an account by ID
        // Developed by: Kiran
        public AccountResponseVM GetAccountByID(int id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id&&!a.IsDeleted);
            if (account == null)
            {
                return null; 
            }
            return AccountResponseVM.ToViewModel(account);

        }

        // Retrieves an account by ID and Tenant ID 
        // Developed by: Kiran
        public AccountResponseVM GetAccountByIdAndTenantId(int id, int TenantId)
        {
            var account = _context.Accounts
                .FirstOrDefault(a => a.Id == id && a.TenantId == TenantId && !a.IsDeleted);
            if (account == null)
            {
                return null;
            }
            return AccountResponseVM.ToViewModel(account);
        }

        // Retrieves all accounts for a specific tenant
        // Developed by: Kiran
        public List<AccountResponseVM> GetAccountByTenantId(int tenantId)
        {
           var accounts = _context.Accounts
                .Where(a => a.TenantId == tenantId && !a.IsDeleted)
                .ToList();
            return accounts.Count > 0 ? AccountResponseVM.ToViewModelList(accounts) : null;
        }

        // Retrieves all accounts in the system
        // Developed by: Kiran
        public List<AccountResponseVM> GetAccountDetails()
        {
            var accounts = _context.Accounts.Where(a=>!a.IsDeleted).ToList();
            return accounts.Count>0 ? AccountResponseVM.ToViewModelList(accounts):null;

        }

        // Updates an existing account by ID and Tenant ID
        // Developed by: Kiran
        public AccountResponseVM UpdateAccount(int id, int tenantId, AccountUpdateVM account)
        {
            var ExistingAccount = _context.Accounts
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (ExistingAccount == null)
            {
                return null;
            }
            ExistingAccount.AccName = account.AccName;
            ExistingAccount.UpiPhoneNo = account.UpiPhoneNo;
            ExistingAccount.AccNumber = account.AccNumber;
            ExistingAccount.CcNumber = account.CcNumber;
            ExistingAccount.AccType = account.AccType;
            ExistingAccount.Branch = account.Branch;
            ExistingAccount.BankName = account.BankName;
            ExistingAccount.IfscCode = account.IfscCode;
            ExistingAccount.Address = account.Address;
            ExistingAccount.UpdatedBy = account.UpdatedBy;
            ExistingAccount.UpdatedOn = DateTime.UtcNow;


            _context.Accounts.Update(ExistingAccount);
            return AccountResponseVM.ToViewModel(ExistingAccount);
        }
    }
}
