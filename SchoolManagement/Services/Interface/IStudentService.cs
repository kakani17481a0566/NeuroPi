using SchoolManagement.ViewModel.Student;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentService
    {
        List<StudentResponseVM> GetAllStudents();

        List<StudentResponseVM> GetStudentsByTenantId(int tenantId);

        StudentResponseVM GetStudentById(int Id);

        StudentResponseVM GetStudentByTenantIdAndId(int tenantId, int Id);

        StudentResponseVM AddStudent(StudentRequestVM studentRequestVM);

        StudentResponseVM UpdateStudent(int Id, int tenantId, StudentUpdateVM UpdateVM);

        bool DeleteStudent(int Id, int tenantId);


    }
}
