using SchoolManagement.Response;
using SchoolManagement.ViewModel.Transaction;
using SchoolManagement.ViewModel.Transcation;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITransactionService
    {
        TransactionResultVM Transfer(TransactionRequestVM request);
        TransactionResponseVM GetByTrxId(int trxId);
        TransactionResponseVM GetByTrxIdAndTenantId(int trxId, int tenantId);
        List<TransactionResponseVM> GetByRefTrnsId(string refTrnsId);
        bool UpdateAmountByRefTrnsIdAndTenant(string refTrnsId, int tenantId, double newAmount, int modifiedBy);

        ResponseResult<VTransactionTableVM> GetFormattedTransactionTable(int tenantId);

    }
}
