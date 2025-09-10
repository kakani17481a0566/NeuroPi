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
        StudentAttendanceStructuredSummaryVm GetAttendanceSummary(DateOnly date, int tenantId, int branchId);


        bool SaveAttendance(SaveAttendanceRequestVm request);

        List<StudentAttendanceGraphVM> GetStudentAttendanceGraph(int studentId, int tenantId, int? branchId, int days = 7);

        List<StudentAttendanceGraphVM> GetLast30DaysGraph(int studentId, int tenantId, int? branchId, string selectedDate,string outputFormat = "dd-MMM-yyyy");

        List<StudentAttendanceGraphVM> GetAttendanceDateRange(int studentId, int tenantId, int? branchId, string fromDateStr, string toDateStr,string outputFormat = "dd-MMM-yyyy");
    }




}

