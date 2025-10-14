using SchoolManagement.ViewModel.PosTransactionDetail;

namespace SchoolManagement.Services.Interface
{
    public interface IPosTransactionDetailService
    {
        PosTransactionDetailResponseVM GetAllDetailsByMasterTransactionId(int masterTransactionId);
    }
}
