using System.Security.Cryptography.X509Certificates;
using NeuroPi.UserManagment.Model;

namespace SchoolManagement.ViewModel.Account
{
    public class AccountResponseVM
    {
        public int Id { get; set; }

        public int? BookId { get; set; }
        public string AccName { get; set; } = string.Empty;
        public string UpiPhoneNo { get; set; } = string.Empty;
        public string AccNumber { get; set; } = string.Empty;
        public string CcNumber { get; set; } = string.Empty;
        public string AccType { get; set; } = string.Empty;
        public string Branch { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string IfscCode { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int TenantId { get; set; }
        public int CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


        public string Code { get; set; }

        public int PrefixSufixId { get; set; }

        public static AccountResponseVM ToViewModel(MAccount account)
        {
            return new AccountResponseVM
            {
                Id = account.Id,
                BookId = account.BookId,
                AccName = account.AccName,
                UpiPhoneNo = account.UpiPhoneNo,
                AccNumber = account.AccNumber,
                CcNumber = account.CcNumber,
                AccType = account.AccType,
                Branch = account.Branch,
                BankName = account.BankName,
                IfscCode = account.IfscCode,
                Address = account.Address,
                TenantId = account.TenantId,
                CreatedBy = account.CreatedBy,
                CreatedOn = account.CreatedOn,
                UpdatedBy = account.UpdatedBy,
                UpdatedOn = account.UpdatedOn,
                Code = account.Code,
                PrefixSufixId = account.PrefixSuffixId 
            };

        }
        public static List<AccountResponseVM> ToViewModelList(List<MAccount> accountList)
        {
            List<AccountResponseVM> result = new List<AccountResponseVM>();
            foreach (var account in accountList)
            {
                result.Add(ToViewModel(account));
            }
            return result;

        }
    }
}