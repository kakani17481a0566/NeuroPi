using SchoolManagement.ViewModel.Transaction;
using SchoolManagement.ViewModel.Transcation;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface ITransactionService
    {
        // Performs a transaction transfer between accounts
        TransactionResultVM Transfer(TransactionRequestVM request);

        // Retrieves a single transaction by transaction ID
        TransactionResponseVM GetByTrxId(int trxId);

        // Retrieves a single transaction by transaction ID and tenant ID
        TransactionResponseVM GetByTrxIdAndTenantId(int trxId, int tenantId);

        // Retrieves a list of transactions that share the same reference transaction ID
        List<TransactionResponseVM> GetByRefTrnsId(string refTrnsId);

        // Updates transaction amounts by reference transaction ID and tenant ID, includes modifiedBy user ID
        bool UpdateAmountByRefTrnsIdAndTenant(string refTrnsId, int tenantId, double newAmount, int modifiedBy);
    }
}
