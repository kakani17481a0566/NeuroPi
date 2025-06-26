using SchoolManagement.ViewModel.StudentAttendance;

namespace SchoolManagement.Services.Interface
{
    public interface IStudentAttendanceService
    {
        List<StudentAttendanceResponseVm> GetStudentAttendanceList();

        StudentAttendanceResponseVm GetStudentAttendanceListByIdAndTenantId(int Id, int tenantId);

        List<StudentAttendanceResponseVm> GetStudentAttendanceByTenantId(int tenantId);

        StudentAttendanceResponseVm GetStudentAttendanceById(int id);

        StudentAttendanceResponseVm AddStudentAttendance(StudentAttendanceRequestVM studentAttendanceRequestVm);

        StudentAttendanceResponseVm UpdateStudentAttendance(int id, int tenantId,StudentAttendanceUpdateVM studentAttendanceRequestVm);

        bool DeleteStudentAttendance(int id, int tenantId);
    }
}
