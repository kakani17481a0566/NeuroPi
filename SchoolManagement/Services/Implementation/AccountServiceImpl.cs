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
        public AccountResponseVM CreateAccount(AccountRequestVM account)
        {
            var newAccount = AccountRequestVM.ToModel(account);
            newAccount.CreatedOn = DateTime.UtcNow;
            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            return AccountResponseVM.ToViewModel(newAccount);
        }

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

        public AccountResponseVM GetAccountByID(int id)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Id == id&&!a.IsDeleted);
            if (account == null)
            {
                return null; 
            }
            return AccountResponseVM.ToViewModel(account);

        }

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

        public List<AccountResponseVM> GetAccountByTenantId(int tenantId)
        {
            return _context.Accounts
                .Where(a => a.TenantId == tenantId && !a.IsDeleted)
                .Select(AccountResponseVM.ToViewModel)
                .ToList();
        }

        public List<AccountResponseVM> GetAccountDetails()
        {
            var accounts = _context.Accounts.ToList();
            return AccountResponseVM.ToViewModelList(accounts);

        }

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
