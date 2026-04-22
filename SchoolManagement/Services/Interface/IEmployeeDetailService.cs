using SchoolManagement.ViewModel.EmployeeDetails;

namespace SchoolManagement.Services.Interface
{
    public interface IEmployeeDetailService
    {
        List<EmployeeDetailsVM> GetAllEmployees(int tenantId);
    }
}
