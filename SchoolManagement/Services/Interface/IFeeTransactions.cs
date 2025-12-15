using System.Threading.Tasks;
using SchoolManagement.Response;
using SchoolManagement.ViewModel.FeeTransactions;

namespace SchoolManagement.Services.Interface
{
    public interface IFeeTransactions
    {

        ResponseResult<int> BuildFeeTransactions(BuildFeeTransactionsRequest request);
        ResponseResult<FeeReportSummaryVM> GetStudentFeeReport(int tenantId, int studentId);
        ResponseResult<List<FeeReportTransactionVM>> GetRecentTransactions(int tenantId, int branchId, int courseId, int limit);
        ResponseResult<int> AddPayment(AddPaymentRequest request);
        ResponseResult<int> AddBill(AddBillRequest request);

    }
}
