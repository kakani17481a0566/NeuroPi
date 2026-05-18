using SchoolManagement.ViewModel.EmployeeDetails;
using SchoolManagement.ViewModel.EmployeeProgress;

namespace SchoolManagement.Services.Interface
{
    public interface IEmployeeDetailService
    {
        List<EmployeeDetailsVM> GetAllEmployees(int tenantId);
        EmployeeDetailsVM CreateEmployeeDetail(EmployeeDetailRequestVM request);
        EmployeeDetailsVM UpdateEmployeeDetail(int id, int tenantId, EmployeeDetailUpdateVM request);
        EmployeeDetailsVM DeleteEmployeeDetail(int id, int tenantId);
        EmployeeProgressVm? GetEmployeeProgress(int id, int tenantId);
    }
}
