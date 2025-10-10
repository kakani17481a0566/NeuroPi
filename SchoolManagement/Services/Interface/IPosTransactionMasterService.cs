using SchoolManagement.ViewModel.PostTransactionMaster;

namespace SchoolManagement.Services.Interface
{
    public interface IPosTransactionMasterService
    {
        string CreatePostTransaction(PosTransactionMasterRequestVM request);

        List<PosTransactionMasterResponseVM> GetAllPostTransactions();
    }
}
