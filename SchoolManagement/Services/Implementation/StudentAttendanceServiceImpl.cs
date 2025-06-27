using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentAttendance;

namespace SchoolManagement.Services.Implementation
{
    public class StudentAttendanceServiceImpl : IStudentAttendanceService
    {
        private readonly SchoolManagementDb _context;
        private readonly NeuroPiDbContext _userContext;

        public StudentAttendanceServiceImpl(
            SchoolManagementDb context,
            NeuroPiDbContext userContext)
        {
            _context = context;
            _userContext = userContext;
        }

        public StudentAttendanceResponseVm AddStudentAttendance(StudentAttendanceRequestVM request)
        {
            var attendanceDate = request.Date.Date;

            var existingRecord = _context.StudentAttendance
                .FirstOrDefault(a => a.StudentId == request.StudentId && a.Date.Date == attendanceDate);

            if (request.FromTime != TimeSpan.Zero && request.ToTime == TimeSpan.Zero)
            {
                if (existingRecord != null && existingRecord.FromTime != TimeSpan.Zero)
                    throw new Exception("Student already checked in for the day.");

                if (existingRecord == null)
                {
                    var newRecord = StudentAttendanceRequestVM.ToModel(request);
                    newRecord.CreatedOn = DateTime.UtcNow;
                    _context.StudentAttendance.Add(newRecord);
                    _context.SaveChanges();
                    return StudentAttendanceResponseVm.ToViewModel(newRecord);
                }
                else
                {
                    existingRecord.FromTime = request.FromTime;
                    existingRecord.UpdatedBy = request.CreatedBy;
                    existingRecord.UpdatedOn = DateTime.UtcNow;
                    _context.StudentAttendance.Update(existingRecord);
                    _context.SaveChanges();
                    return StudentAttendanceResponseVm.ToViewModel(existingRecord);
                }
            }
            else if (request.ToTime != TimeSpan.Zero)
            {
                if (existingRecord == null || existingRecord.FromTime == TimeSpan.Zero)
                    throw new Exception("The student didn't check in.");

                if (existingRecord.ToTime != TimeSpan.Zero)
                    throw new Exception("Student already checked out.");

                existingRecord.ToTime = request.ToTime;
                existingRecord.UpdatedBy = request.CreatedBy;
                existingRecord.UpdatedOn = DateTime.UtcNow;
                _context.StudentAttendance.Update(existingRecord);
                _context.SaveChanges();
                return StudentAttendanceResponseVm.ToViewModel(existingRecord);
            }
            else
            {
                throw new Exception("Invalid attendance data. Either check-in or check-out time must be provided.");
            }
        }

        public bool DeleteStudentAttendance(int id, int tenantId)
        {
            var attendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);

            if (attendance == null)
                return false;

            attendance.IsDeleted = true;
            attendance.UpdatedOn = DateTime.UtcNow;
            _context.StudentAttendance.Update(attendance);
            _context.SaveChanges();
            return true;
        }

        public StudentAttendanceResponseVm GetStudentAttendanceById(int id)
        {
            var attendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == id && !a.IsDeleted);

            return attendance == null ? null : StudentAttendanceResponseVm.ToViewModel(attendance);
        }

        public List<StudentAttendanceResponseVm> GetStudentAttendanceByTenantId(int tenantId)
        {
            var attendances = _context.StudentAttendance
                .Where(a => a.TenantId == tenantId && !a.IsDeleted)
                .ToList();

            return attendances.Select(StudentAttendanceResponseVm.ToViewModel).ToList();
        }

        public List<StudentAttendanceResponseVm> GetStudentAttendanceList()
        {
            var attendances = _context.StudentAttendance
                .Where(a => !a.IsDeleted)
                .ToList();

            return attendances.Select(StudentAttendanceResponseVm.ToViewModel).ToList();
        }

        public StudentAttendanceResponseVm GetStudentAttendanceListByIdAndTenantId(int id, int tenantId)
        {
            var attendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);

            return attendance == null ? null : StudentAttendanceResponseVm.ToViewModel(attendance);
        }

        public StudentAttendanceResponseVm UpdateStudentAttendance(int id, int tenantId, StudentAttendanceUpdateVM vm)
        {
            var existingAttendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);

            if (existingAttendance == null)
                return null;

            existingAttendance.Date = vm.Date;
            existingAttendance.UpdatedOn = DateTime.UtcNow;
            _context.StudentAttendance.Update(existingAttendance);
            _context.SaveChanges();
            return StudentAttendanceResponseVm.ToViewModel(existingAttendance);
        }

        public List<StudentAttendanceSummaryVm> GetAttendanceSummary(DateTime date, int tenantId, int branchId)
        {
            var studentsWithAttendance = _context.Students
                .Where(s => !s.IsDeleted && s.TenantId == tenantId && s.BranchId == branchId)
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    CourseId = s.Course.Id, 

                    CourseName = s.Course.Name,
                    Attendance = s.StudentAttendances
                        .Where(a => a.Date == date.Date && !a.IsDeleted)
                        .OrderByDescending(a => a.Id)
                        .FirstOrDefault()
                })
                .OrderBy(x => x.CourseName)
                .ThenBy(x => x.Id)
                .ToList();

            var userIds = studentsWithAttendance
                .SelectMany(x => new[] { x.Attendance?.CreatedBy, x.Attendance?.UpdatedBy })
                .Where(id => id.HasValue)
                .Select(id => id.Value)
                .Distinct()
                .ToList();

            var users = _userContext.Users
                .Where(u => userIds.Contains(u.UserId))
                .ToDictionary(u => u.UserId, u => $"{u.FirstName} {u.LastName}".Trim());

            var result = studentsWithAttendance.Select(x =>
            {
                var att = x.Attendance;
                return new StudentAttendanceSummaryVm
                {
                    StudentId = x.Id,
                    StudentName = x.Name,
                    ClassName = x.CourseName,
                    CourseId = x.CourseId, 

                    AttendanceId = att?.Id,
                    AttendanceDate = att?.Date.ToString("yyyy-MM-dd") ?? "Attendance not given",
                    FromTime = FormatTime(att?.FromTime),
                    ToTime = FormatTime(att?.ToTime),
                    MarkedBy = att?.CreatedBy != null && users.TryGetValue(att.CreatedBy, out var createdBy) ? createdBy : "Not marked",
                    MarkedOn = att != null ? FormatDateTime(att.CreatedOn) : null,
                    UpdatedBy = att?.UpdatedBy != null && users.TryGetValue(att.UpdatedBy.Value, out var updatedBy) ? updatedBy : "",
                    UpdatedOn = att?.UpdatedOn.HasValue == true ? FormatDateTime(att.UpdatedOn.Value) : null,
                    AttendanceStatus = GetAttendanceStatus(att)
                };
            }).ToList();

            return result;
        }

        // 🔽 Helper Methods 🔽

        private string FormatTime(TimeSpan? time) =>
            time.HasValue && time.Value != TimeSpan.Zero ? time.Value.ToString(@"hh\:mm") : "Not marked";

        private string FormatDateTime(DateTime dateTime) =>
            dateTime.ToString("yyyy-MM-dd HH:mm");

        private string GetAttendanceStatus(MStudentAttendance attendance)
        {
            if (attendance == null) return "Not Marked";
            if (attendance.FromTime != TimeSpan.Zero && attendance.ToTime == TimeSpan.Zero) return "Checked-In";
            if (attendance.FromTime != TimeSpan.Zero && attendance.ToTime != TimeSpan.Zero) return "Checked-Out";
            return "Unknown";
        }
    }
}
