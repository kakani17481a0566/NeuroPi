using SchoolManagement.ViewModel.PosTransactionDetail;
using SchoolManagement.ViewModel.PosTransactionMaster;

namespace SchoolManagement.Services.Interface
{
    public interface IPosTransactionMasterService
    {
        PosInvoiceVM CreatePostTransaction(PosTransactionMasterRequestVM request);

        List<PosTransactionMasterResponseVM> GetAllPostTransactions();

        List<PosTransactionMasterResponseVM> GetPostTransactionById(int studentId);
        List<PosTransactionMasterResponseVM> GetSalesByBranchId(int branchId);
        List<SalesTrendVM> GetSalesTrends(int branchId, string period);
        List<TopSellerVM> GetTopSellers(int branchId, int count);
        SalesSummaryVM GetSalesSummary(int branchId);
        List<PaymentMethodStatVM> GetPaymentMethodStats(int branchId);
        List<CourseSalesStatVM> GetCourseSalesStats(int branchId);
    }
}
