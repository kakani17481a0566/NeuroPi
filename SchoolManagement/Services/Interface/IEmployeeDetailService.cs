using SchoolManagement.ViewModel.EmployeeDetails;

namespace SchoolManagement.Services.Interface
{
    public interface IEmployeeDetailService
    {
        List<EmployeeDetailsVM> GetAllEmployees(int tenantId);
        EmployeeDetailsVM CreateEmployeeDetail(EmployeeDetailRequestVM request);
        EmployeeDetailsVM UpdateEmployeeDetail(int id, int tenantId, EmployeeDetailUpdateVM request);
        EmployeeDetailsVM DeleteEmployeeDetail(int id, int tenantId);
    }
}
