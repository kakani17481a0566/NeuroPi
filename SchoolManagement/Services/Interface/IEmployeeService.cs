using SchoolManagement.ViewModel.Employee;
using SchoolManagement.ViewModel.Master;

namespace SchoolManagement.Services.Interface
{
    public interface IEmployeeService
    {
        EmployeeResponseVM GetById(int id);

        List<EmployeeResponseVM> GetAll();

        List<EmployeeResponseVM> GetAllByTenantId(int tenantId);
        EmployeeResponseVM GetByIdAndTenantId(int id, int tenantId);

        EmployeeResponseVM CreateEmployee(EmployeeRequestVM masterType);


        EmployeeResponseVM UpdateEmployee(int id, int tenantId, MasterUpdateVM masterType);

        EmployeeResponseVM DeleteById(int id, int tenantId);

        //List<EmployeeResponseVM> GetByEmployeeId(int id, int tenantId);
    }
}
