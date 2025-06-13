using SchoolManagement.ViewModel.Students;
using System.Collections.Generic;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentService
    {
        List<StudentResponseVM> GetAll();
        StudentResponseVM GetById(int id);
        List<StudentResponseVM> GetAllByTenantId(int tenantId);
        StudentResponseVM GetByIdAndTenantId(int id, int tenantId);
        List<StudentResponseVM> GetByTenantAndBranch(int tenantId, int branchId); // ✅ Add this
        StudentResponseVM Create(StudentRequestVM request); // ✅ Add these if not present
        StudentResponseVM Update(int id, StudentRequestVM request);
        StudentResponseVM Delete(int id);
    }
}
