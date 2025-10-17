using SchoolManagement.ViewModel.PosTransactionDetail;
using SchoolManagement.ViewModel.PostTransactionMaster;

namespace SchoolManagement.Services.Interface
{
    public interface IPosTransactionMasterService
    {
        PosInvoiceVM CreatePostTransaction(PosTransactionMasterRequestVM request);

        List<PosTransactionMasterResponseVM> GetAllPostTransactions();

        List<PosTransactionMasterResponseVM> GetPostTransactionById(int studentId);
    }
}
