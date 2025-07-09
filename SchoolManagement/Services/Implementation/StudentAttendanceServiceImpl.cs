using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var attendanceDate = request.Date;

            var existingRecord = _context.StudentAttendance
                .FirstOrDefault(a => a.StudentId == request.StudentId && a.Date == attendanceDate);

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
   




        public bool SaveAttendance(SaveAttendanceRequestVm request)
        {
            var now = DateTime.UtcNow;

            // Get student IDs from entries
            var studentIds = request.Entries.Select(e => e.StudentId).ToList();

            // Fetch existing attendance records for those students
            var existingAttendance = _context.StudentAttendance
                .Where(a =>
                    !a.IsDeleted &&
                    a.Date == request.Date &&
                    a.BranchId == request.BranchId &&
                    a.TenantId == request.TenantId &&
                    studentIds.Contains(a.StudentId))
                .ToDictionary(a => a.StudentId);

            foreach (var entry in request.Entries)
            {
                if (existingAttendance.TryGetValue(entry.StudentId, out var attendance))
                {
                    // ✅ Update check-in or check-out if not already set
                    if (attendance.FromTime == TimeSpan.Zero && entry.FromTime != TimeSpan.Zero)
                        attendance.FromTime = entry.FromTime;

                    if (attendance.ToTime == TimeSpan.Zero && entry.ToTime != TimeSpan.Zero)
                        attendance.ToTime = entry.ToTime;

                    attendance.UpdatedBy = request.UserId;
                    attendance.UpdatedOn = now;
                }
                else
                {
                    // ✅ Insert new attendance record
                    _context.StudentAttendance.Add(new MStudentAttendance
                    {
                        StudentId = entry.StudentId,
                        UserId = request.UserId,
                        Date = request.Date,
                        FromTime = entry.FromTime,
                        ToTime = entry.ToTime,
                        BranchId = request.BranchId,
                        TenantId = request.TenantId,
                        CreatedBy = request.UserId,
                        CreatedOn = now,
                        IsDeleted = false
                    });
                }
            }

            _context.SaveChanges();
            return true;
        }



        public StudentAttendanceStructuredSummaryVm GetAttendanceSummary(DateOnly date, int tenantId, int branchId)
        {
            var studentsWithAttendance = _context.Students
                .Where(s => !s.IsDeleted && s.TenantId == tenantId && s.BranchId == branchId)
                .Include(s => s.Course)
                .Include(s => s.ParentStudents).ThenInclude(ps => ps.Parent).ThenInclude(p => p.User)
                .Include(s => s.StudentAttendances)
                .AsEnumerable()
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    CourseId = s.Course?.Id ?? 0,
                    CourseName = s.Course?.Name ?? "Unassigned",
                    Parent = s.ParentStudents
                        .Where(ps => !ps.IsDeleted && !ps.Parent.IsDeleted && ps.Parent.User != null)
                        .Select(ps => new
                        {
                            ps.ParentId,
                            ParentName = ps.Parent.User.Username,
                            MobileNumber = ps.Parent.User.MobileNumber,
                            AlternateNumber = ps.Parent.User.AlternateNumber
                        })
                        .FirstOrDefault(),
                    Attendance = s.StudentAttendances
                        .Where(a => a.Date == date && !a.IsDeleted)
                        .OrderByDescending(a => a.Id)
                        .FirstOrDefault()
                })
                .OrderBy(x => x.CourseName)
                .ThenBy(x => x.Name)
                .ToList();

            // Step 1: Get headers from the VM using reflection
            var headers = typeof(StudentAttendanceSummaryVm)
                .GetProperties()
                .Select(p => char.ToLowerInvariant(p.Name[0]) + p.Name.Substring(1)) // convert to camelCase for frontend
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

            var records = studentsWithAttendance.Select(x =>
            {
                var att = x.Attendance;

                return new StudentAttendanceSummaryVm
                {
                    StudentId = x.Id,
                    StudentName = x.Name,
                    ClassName = x.CourseName,
                    CourseId = x.CourseId,
                    ParentId = x.Parent?.ParentId ?? 0,
                    ParentName = x.Parent?.ParentName ?? "Not Assigned",
                    MobileNumber = x.Parent?.MobileNumber ?? "-",
                    AlternateNumber = x.Parent?.AlternateNumber ?? "-",
                    AttendanceId = att?.Id,
                    AttendanceDate = att?.Date.ToString("yyyy-MM-dd") ?? "Attendance not given",
                    FromTime = FormatTime(att?.FromTime),
                    ToTime = FormatTime(att?.ToTime),
                    MarkedBy = att?.CreatedBy is int cb && users.TryGetValue(cb, out var mb) ? mb : "Not marked",
                    MarkedOn = att != null ? FormatDateTime(att.CreatedOn) : null,
                    UpdatedBy = att?.UpdatedBy is int ub && users.TryGetValue(ub, out var ubName) ? ubName : "",
                    UpdatedOn = att?.UpdatedOn.HasValue == true ? FormatDateTime(att.UpdatedOn.Value) : null,
                    AttendanceStatus = GetAttendanceStatus(att)
                };
            }).ToList();

            var distinctCourses = records
                .GroupBy(r => new { r.CourseId, r.ClassName })
                .Select(g => new CourseVm
                {
                    Id = g.Key.CourseId,
                    Name = g.Key.ClassName
                })
                .OrderBy(c => c.Name)
                .ToList();

            return new StudentAttendanceStructuredSummaryVm
            {
                Headers = headers,
                Records = records,
                Courses = distinctCourses,
                TotalStudents = records.Count,
                CheckedInCount = records.Count(r => r.AttendanceStatus == "Checked-In"),
                CheckedOutCount = records.Count(r => r.AttendanceStatus == "Checked-Out"),
                NotMarkedCount = records.Count(r => r.AttendanceStatus == "Not Marked"),
                UnknownCount = records.Count(r => r.AttendanceStatus == "Unknown")
            };
        }




        // 🔽 Helper Methods 🔽

        private string FormatTime(TimeSpan? time) =>
         time.HasValue && time.Value != TimeSpan.Zero ? time.Value.ToString(@"hh\:mm") : "Not marked";

        private string FormatDateTime(DateTime dateTime) =>
            dateTime.ToString("yyyy-MM-dd HH:mm");

        private string GetAttendanceStatus(MStudentAttendance attendance)
        {
            if (attendance == null) return "Not Marked";

            var hasFrom = attendance.FromTime != null && attendance.FromTime != TimeSpan.Zero;
            var hasTo = attendance.ToTime != null && attendance.ToTime != TimeSpan.Zero;

            if (hasFrom && hasTo) return "Checked-Out";
            if (hasFrom) return "Checked-In";

            return "Not Marked";
        }




    }
}
