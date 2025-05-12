using NeuroPi.UserManagment.ViewModel.Department;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IDepartmentService
    {

        List<DepartmentResponseVM> GetAllDepartments();

        DepartmentResponseVM GetDepartmentById(int id);

        bool DeleteById(int id);

        DepartmentResponseVM AddDepartment(DepartmentRequestVM department);

        DepartmentResponseVM UpdateDepartment(int id, DepartmentRequestVM department);


    }
}
