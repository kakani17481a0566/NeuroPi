using System.Threading.Tasks;
using SchoolManagement.Response;
using SchoolManagement.ViewModel.FeeTransactions;
using SchoolManagement.ViewModel.Student;

namespace SchoolManagement.Services.Interface
{
    public interface IFeeTransactions
    {

        ResponseResult<int> BuildFeeTransactions(BuildFeeTransactionsRequest request);
        ResponseResult<FeeReportSummaryVM> GetStudentFeeReport(int tenantId, int studentId);
        ResponseResult<List<FeeReportTransactionVM>> GetRecentTransactions(int tenantId, int branchId, int courseId, int limit);
        ResponseResult<int> AddPayment(AddPaymentRequest request);
        ResponseResult<int> AddBill(AddBillRequest request);

        ResponseResult<FeeStatsVM> GetBranchFeeStats(int tenantId, int branchId);
        ResponseResult<List<FeeReportTransactionVM>> GetBranchTransactions(int tenantId, int branchId, int limit);
        ResponseResult<List<FeeHistoryVM>> GetBranchFeeHistory(int tenantId, int branchId);
        ResponseResult<List<StudentListVM>> GetCompletedPayments(int tenantId, int branchId);

    }
}
