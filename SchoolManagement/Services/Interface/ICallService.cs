using SchoolManagement.ViewModel.Call;

namespace SchoolManagement.Services.Interface
{
    public interface ICallService
    {
        List<CallResponseVM> GetAllEmployeeLogs(int empId,int tenantId);

        List<CallResponseVM> GetAllLogs(int tenantId);

        Task<CallResponseVM> AddCallAsync(CallCreateVM request);

        Task<CallDashboardOverviewVM> GetDashboardOverviewAsync(int tenantId);
    }
}
