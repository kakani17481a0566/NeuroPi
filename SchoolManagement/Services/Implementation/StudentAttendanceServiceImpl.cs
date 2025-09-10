using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NeuroPi.UserManagment.Data;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentAttendance;
using System.Globalization;

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
        private static readonly string[] AcceptFormats =
   {
        "dd-MM-yyyy", "d-M-yyyy", "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"
    };

        private static DateOnly ParseDateOnly(string s)
        {
            if (!DateOnly.TryParseExact(s?.Trim(), AcceptFormats, CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var d))
                throw new ArgumentException($"Invalid date '{s}'. Use dd-MM-yyyy (e.g., 01-08-2025).");
            return d;
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

            var studentIds = request.Entries.Select(e => e.StudentId).ToList();

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
                // ⚠️ Skip invalid time range
                if (entry.FromTime != TimeSpan.Zero &&
                    entry.ToTime != TimeSpan.Zero &&
                    entry.ToTime < entry.FromTime)
                {
                    throw new InvalidOperationException($"Check-out time cannot be earlier than check-in time for student ID {entry.StudentId}.");
                }

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
                    s.StudentImageUrl,
                    s.BloodGroup,
                    s.Gender,
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
                    ImageUrl = x.StudentImageUrl,
                    Gender = x.Gender,
                    BloodGroup = x.BloodGroup,
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




        public List<StudentAttendanceGraphVM> GetStudentAttendanceGraph(int studentId, int tenantId, int? branchId, int days = 7)
        {
            var fromDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-days));

            var query = _context.StudentAttendance
                .Where(s => !s.IsDeleted &&
                            s.StudentId == studentId &&
                            s.TenantId == tenantId &&
                            s.Date >= fromDate);

            if (branchId.HasValue)
                query = query.Where(s => s.BranchId == branchId.Value);

            return query
                .OrderBy(s => s.Date)
                .ThenBy(s => s.FromTime)
                .Include(m => m.Student)
                .Include(m => m.Student.Course)
                .Select(s => new StudentAttendanceGraphVM
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.Name,
                    CourseName = s.Student.Course.Name,
                    Date = s.Date.ToString("yyyy-MM-dd"),
                    InTime = TimeOnly.FromTimeSpan(s.FromTime).ToString("HH:mm"),
                    OutTime = TimeOnly.FromTimeSpan(s.ToTime).ToString("HH:mm"),

                    TotalTime = (s.FromTime != TimeSpan.Zero && s.ToTime != TimeSpan.Zero && s.ToTime >= s.FromTime)
                        ? (s.ToTime - s.FromTime).ToString(@"hh\:mm")
                        : null

                })
                .ToList();
        }

        public List<StudentAttendanceGraphVM> GetLast30DaysGraph(
    int studentId, int tenantId, int? branchId, string selectedDate, string outputFormat = "yyyy-MM-dd")
        {

            var endDay = ParseDateOnly(selectedDate);
            var fromDay = endDay.AddDays(-29);

            var query = _context.StudentAttendance
                .AsNoTracking()
                .Where(s => !s.IsDeleted
                            && s.StudentId == studentId
                            && s.TenantId == tenantId
                            && s.Date >= fromDay
                            && s.Date <= endDay);

            if (branchId.HasValue)
                query = query.Where(s => s.BranchId == branchId.Value);

            return query
                .OrderBy(s => s.Date)
                .ThenBy(s => s.FromTime)
                .Include(m=>m.Student)
                .Include(m=>m.Student.Course)
                .Select(s => new StudentAttendanceGraphVM
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.Name,
                    CourseName = s.Student.Course.Name,
                    Date = s.Date.ToString(outputFormat),
                    InTime = s.FromTime != TimeSpan.Zero ? TimeOnly.FromTimeSpan(s.FromTime).ToString("HH:mm") : null,
                    OutTime = s.ToTime != TimeSpan.Zero ? TimeOnly.FromTimeSpan(s.ToTime).ToString("HH:mm") : null,

                    TotalTime = (s.FromTime != TimeSpan.Zero && s.ToTime != TimeSpan.Zero && s.ToTime >= s.FromTime)
                        ? (s.ToTime - s.FromTime).ToString(@"hh\:mm")
                        : null


                })
                .ToList();
        }

        public List<StudentAttendanceGraphVM> GetAttendanceDateRange(int studentId, int tenantId, int? branchId, string fromDatestr, string toDatestr, string outputFormat = "dd-MMM-yyyy")
        {

            var startDay = ParseDateOnly(fromDatestr);
            var endDay = ParseDateOnly(toDatestr);

            if (endDay < startDay)
                throw new ArgumentException("End date cannot be earlier than start date.");

            var report = _context.StudentAttendance
                .AsNoTracking()
                .Where(s => !s.IsDeleted
                            && s.StudentId == studentId
                            && s.TenantId == tenantId
                            && s.Date >= startDay
                            && s.Date <= endDay);
            if (branchId.HasValue)
                report = report.Where(s => s.BranchId == branchId.Value);

            return report
                .OrderBy(s => s.Date)
                .ThenBy(s => s.FromTime)
                .Include(m => m.Student)
                .Include(m => m.Student.Course)
                .Select(s => new StudentAttendanceGraphVM
                {
                    StudentId = s.StudentId,
                    StudentName = s.Student.Name,
                    CourseName = s.Student.Course.Name,
                    Date = s.Date.ToString(outputFormat),
                    InTime = s.FromTime != TimeSpan.Zero ? TimeOnly.FromTimeSpan(s.FromTime).ToString("HH:mm") : null,
                    OutTime = s.ToTime != TimeSpan.Zero ? TimeOnly.FromTimeSpan(s.ToTime).ToString("HH:mm") : null,
                    TotalTime = (s.FromTime != TimeSpan.Zero && s.ToTime != TimeSpan.Zero && s.ToTime >= s.FromTime)
                        ? (s.ToTime - s.FromTime).ToString(@"hh\:mm")
                        : null
                })
                .ToList();
        }
    }
}