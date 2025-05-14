using System.Collections.Generic;
using NeuroPi.UserManagment.ViewModel.Department;

namespace NeuroPi.UserManagment.Services.Interface
{
    public interface IDepartmentService
    {
        List<DepartmentResponseVM> GetAllDepartments();

        DepartmentResponseVM GetDepartmentById(int id);

        DepartmentResponseVM GetDepartmentByIdAndTenantId(int id, int tenantId);

        List<DepartmentResponseVM> GetDepartmentsByTenantId(int tenantId);

        bool DeleteById(int id, int tenantId, int deletedBy);

        DepartmentResponseVM AddDepartment(DepartmentCreateVM department);
        DepartmentResponseVM UpdateDepartment(int id, int tenantId, DepartmentUpdateVM department);



    }
}
