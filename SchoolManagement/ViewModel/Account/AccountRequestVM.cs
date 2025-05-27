using NeuroPi.UserManagment.Model;

namespace SchoolManagement.ViewModel.Account
{
    public class AccountRequestVM
    {
        public int? BookId { get; set; }

        public string AccName { get; set; }

        public string UpiPhoneNo { get; set; }

        public string AccNumber { get; set; }

        public string CcNumber { get; set; }

        public string AccType { get; set; }

        public string Branch { get; set; }

        public string BankName { get; set; }

        public string IfscCode { get; set; }

        public string Address { get; set; }

        public int TenantId { get; set; }

        public int createdBy { get; set; }

        public static MAccount ToModel(AccountRequestVM requestVm)
        {
            return new MAccount()
            {
                BookId = requestVm.BookId,
                AccName = requestVm.AccName,
                UpiPhoneNo = requestVm.UpiPhoneNo,
                AccNumber = requestVm.AccNumber,
                CcNumber = requestVm.CcNumber,
                AccType = requestVm.AccType,
                Branch = requestVm.Branch,
                BankName = requestVm.BankName,
                IfscCode = requestVm.IfscCode,
                Address = requestVm.Address,
                TenantId = requestVm.TenantId,
                CreatedBy = requestVm.createdBy,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
