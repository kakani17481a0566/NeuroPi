using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.StudentAttendance;

namespace SchoolManagement.Services.Implementation
{
    public class StudentAttendanceServiceImpl : IStudentAttendanceService
    {
        private readonly SchoolManagementDb _context;

        public StudentAttendanceServiceImpl(SchoolManagementDb context)
        {
            _context = context;
        }
        //public StudentAttendanceResponseVm AddStudentAttendance(StudentAttendanceRequestVM studentAttendanceRequestVm)
        //{
        //    var newAttendance = StudentAttendanceRequestVM.ToModel(studentAttendanceRequestVm);
        //    newAttendance.CreatedOn = DateTime.UtcNow;
        //    _context.StudentAttendance.Add(newAttendance);
        //    _context.SaveChanges();
        //    return StudentAttendanceResponseVm.ToViewModel(newAttendance);
        //}
        public StudentAttendanceResponseVm AddStudentAttendance(StudentAttendanceRequestVM request)
        {
            var attendanceDate = request.Date.Date;

            var existingRecord = _context.StudentAttendance
                .FirstOrDefault(a => a.StudentId == request.StudentId && a.Date.Date == attendanceDate);

            if (request.FromTime != TimeSpan.Zero && request.ToTime == TimeSpan.Zero)
            {
                // Check-in only
                if (existingRecord != null && existingRecord.FromTime != TimeSpan.Zero)
                {
                    throw new Exception("Student already checked in for the day.");
                }

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
                // Check-out
                if (existingRecord == null || existingRecord.FromTime == TimeSpan.Zero)
                {
                    throw new Exception("The student didn't check in.");
                }

                if (existingRecord.ToTime != TimeSpan.Zero)
                {
                    throw new Exception("Student already checked out.");
                }

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
            {
                return false;
            }
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
            if (attendance == null)
            {
                return null;
            }
            return StudentAttendanceResponseVm.ToViewModel(attendance);

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

        public StudentAttendanceResponseVm GetStudentAttendanceListByIdAndTenantId(int Id, int tenantId)
        {
            var attendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == Id && a.TenantId == tenantId && !a.IsDeleted);
            if (attendance == null)
            {
                return null;
            }
            return StudentAttendanceResponseVm.ToViewModel(attendance);
        }

        public StudentAttendanceResponseVm UpdateStudentAttendance(int id, int tenantId, StudentAttendanceUpdateVM studentAttendanceRequestVm)
        {
            var existingAttendance = _context.StudentAttendance
                .FirstOrDefault(a => a.Id == id && a.TenantId == tenantId && !a.IsDeleted);
            if (existingAttendance == null)
            {
                return null;
            }
            existingAttendance.Date = studentAttendanceRequestVm.Date;
            existingAttendance.UpdatedOn = DateTime.UtcNow;
            _context.StudentAttendance.Update(existingAttendance);
            _context.SaveChanges();
            return StudentAttendanceResponseVm.ToViewModel(existingAttendance);

        }
    }
}
