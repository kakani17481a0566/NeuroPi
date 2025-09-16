using Microsoft.AspNetCore.Components.Forms;
using Microsoft.CognitiveServices.Speech.Transcription;
using Microsoft.EntityFrameworkCore;
using NeuroPi.UserManagment.Data;
using NeuroPi.UserManagment.Model;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.Student;
using SchoolManagement.ViewModel.StudentRegistration;
using SchoolManagement.ViewModel.Students;
using SchoolManagement.ViewModel.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace SchoolManagement.Services.Implementation
{
    public class StudentServiceImpl : IStudentService
    {
        private readonly SchoolManagementDb _context;
        private readonly NeuroPiDbContext _userContext;

        public StudentServiceImpl(SchoolManagementDb schoolContext, NeuroPiDbContext userContext)
        {
            _context = schoolContext;
            _userContext = userContext;
        }


        public List<StudentResponseVM> GetAll()
        {
            return _context.Students
                .Where(s => !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM GetById(int id)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            return student == null ? null : StudentResponseVM.ToViewModel(student);
        }

        public List<StudentResponseVM> GetAllByTenantId(int tenantId)
        {
            return _context.Students
                .Where(s => s.TenantId == tenantId && !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var student = _context.Students
                .FirstOrDefault(s => s.Id == id && s.TenantId == tenantId && !s.IsDeleted);
            return student == null ? null : StudentResponseVM.ToViewModel(student);
        }

        public List<StudentResponseVM> GetByTenantAndBranch(int tenantId, int branchId)
        {
            return _context.Students
                .Where(s => s.TenantId == tenantId && s.BranchId == branchId && !s.IsDeleted)
                .Select(StudentResponseVM.ToViewModel)
                .ToList();
        }

        public StudentResponseVM Create(StudentRequestVM request)
        {
            var newStudent = request.ToModel();
            newStudent.CreatedOn = DateTime.UtcNow;
            _context.Students.Add(newStudent);
            _context.SaveChanges();

            return StudentResponseVM.ToViewModel(newStudent);
        }

        public StudentResponseVM Update(int id, StudentRequestVM request)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (student == null) return null;

            student.Name = request.Name;
            student.CourseId = request.CourseId;
            student.BranchId = request.BranchId;
            student.TenantId = request.TenantId;
            student.UpdatedOn = DateTime.UtcNow;
            student.UpdatedBy = request.UpdatedBy;

            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(student);
        }

        public StudentResponseVM Delete(int id)
        {
            var student = _context.Students.FirstOrDefault(s => s.Id == id && !s.IsDeleted);
            if (student == null) return null;

            student.IsDeleted = true;
            student.UpdatedOn = DateTime.UtcNow;

            _context.SaveChanges();
            return StudentResponseVM.ToViewModel(student);
        }


       
     public StudentVM GetByTenantCourseBranch(int tenantId, int courseId, int branchId)
        {
            var result= _context.Students
                .Include(s => s.Course)
                .Include(s => s.Branch)
                .Where(s => s.TenantId == tenantId && s.CourseId == courseId && s.BranchId == branchId && !s.IsDeleted)
                .ToList();
            if(result!=null && result.Count > 0)
            {
                StudentVM student = new StudentVM();
                student.CourseId = result.First().CourseId;
                student.CourseName = result.First().Course.Name;
                student.BranchId = result.First().BranchId;
                student.BranchName = result.First().Branch.Name;
                student.TenantId = result.First().TenantId;
                List<Student> students = new List<Student>();
                foreach(MStudent stu in result){
                    Student s = new Student()
                    {
                        id = stu.Id,
                        name = stu.Name
                    };
                    students.Add(s);

                }
                student.students = students;
                return student;
            }
            return null;

        }

        public StudentsData GetStudentDetails(int courseId, int branchId, DateOnly date,int tenantId)
        {
           // int totalStudents = 0;
            int checkedIn = 0;
            int checkedOut = 0;
            List<StudentDetails> studentDetails = new List<StudentDetails>();
            var students = _context.StudentAttendance.Where(s => s.Date == date && s.TenantId == tenantId && !s.IsDeleted).Include(s => s.Student).ToList();
            if (students != null &&  students.Count>0){
                foreach (MStudentAttendance student in students)
                {
                    MStudent mStudent = student.Student;
                    if (mStudent.BranchId == branchId && mStudent.CourseId == courseId)
                    {
                        if (student.FromTime != TimeSpan.Zero)
                        {
                            checkedIn++;
                        }
                        if (student.ToTime != TimeSpan.Zero)
                        {
                            checkedOut++;
                        }
                        var Student = new StudentDetails()
                        {
                            Name = mStudent.Name,
                            date = student.Date,
                            checkedIn = student.FromTime,
                            checkedOut = student.ToTime,
                        };
                        studentDetails.Add(Student);
                       
                    }

                }
                var response = new StudentsData()
                {
                    totalStudents = studentDetails.Count,
                    checkedIn = checkedIn,
                    checkedOut = checkedOut,
                    students = studentDetails,
                };
                return response;
            }
            return null;
        }




        public List<VStudentPerformanceVM> GetStudentPerformance(int tenantId, int courseId, int branchId)
        {
            var result = _context.DailyAssessments
                .Include(d => d.Student)
                .Include(d => d.Grade)
                .Include(d => d.TimeTable)
                    .ThenInclude(t => t.Week)
                        .ThenInclude(w => w.Term)
                .Include(d => d.TimeTable.Course)
                .Include(d => d.Assessment)
                    .ThenInclude(a => a.AssessmentSkill)
                .Where(d =>
                    !d.IsDeleted &&
                    !d.Student.IsDeleted &&
                    !d.Grade.IsDeleted &&
                    !d.TimeTable.IsDeleted &&
                    !d.TimeTable.Week.IsDeleted &&
                    !d.TimeTable.Week.Term.IsDeleted &&
                    !d.Assessment.IsDeleted &&
                    !d.Assessment.AssessmentSkill.IsDeleted &&
                    !d.TimeTable.Course.IsDeleted &&
                    d.Student.TenantId == tenantId &&
                    d.Student.CourseId == courseId &&
                    d.Student.BranchId == branchId
                )
                .Select(d => new VStudentPerformanceVM
                {
                    AssessmentId = d.Id,
                    AssessmentDate = d.AssessmentDate,
                    StudentId = d.Student.Id,
                    StudentName = d.Student.Name,
                    GradeId = d.Grade.Id,
                    Grade = d.Grade.Name,
                    GradeDescription = d.Grade.Description,
                    TimeTableId = d.TimeTable.Id,
                    DayName = d.TimeTable.Name,
                    TimeTableDate = d.TimeTable.Date,
                    WeekId = d.TimeTable.Week.Id,
                    WeekName = d.TimeTable.Week.Name,
                    WeekStartDate = d.TimeTable.Week.StartDate.ToDateTime(TimeOnly.MinValue),
                    WeekEndDate = d.TimeTable.Week.EndDate.ToDateTime(TimeOnly.MinValue),
                    TermId = d.TimeTable.Week.Term.Id,
                    TermName = d.TimeTable.Week.Term.Name,
                    TermStartDate = d.TimeTable.Week.Term.StartDate,
                    TermEndDate = d.TimeTable.Week.Term.EndDate,
                    CourseId = d.TimeTable.Course.Id,
                    CourseName = d.TimeTable.Course.Name,
                    AssessmentItemId = d.Assessment.Id,
                    AssessmentName = d.Assessment.Name,
                    SkillId = d.Assessment.AssessmentSkill.Id,
                    SkillName = d.Assessment.AssessmentSkill.Name,
                    SkillCode = d.Assessment.AssessmentSkill.Code
                })
                .ToList();

            return result;
        }


        public VStudentPerformanceChartVM GetStudentPerformanceChartData(int tenantId, int courseId, int branchId)
        {
            var rawData = _context.DailyAssessments
                .Where(d => !d.IsDeleted &&
                            !d.Student.IsDeleted &&
                            !d.Grade.IsDeleted &&
                            !d.TimeTable.IsDeleted &&
                            !d.TimeTable.Week.IsDeleted &&
                            !d.TimeTable.Week.Term.IsDeleted &&
                            !d.TimeTable.Course.IsDeleted &&
                            !d.Assessment.IsDeleted &&
                            !d.Assessment.AssessmentSkill.IsDeleted &&
                            d.Student.TenantId == tenantId &&
                            d.Student.CourseId == courseId &&
                            d.Student.BranchId == branchId)
                .Select(d => new
                {
                    StudentId = d.Student.Id,
                    StudentName = d.Student.Name,
                    SkillCode = d.Assessment.AssessmentSkill.Code,
                    Grade = d.Grade.Name
                })
                .ToList();

            var skillCodes = rawData
                .Select(x => x.SkillCode)
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            var headers = new List<string> { "Student" };
            headers.AddRange(skillCodes);

            var studentGroups = rawData
                .GroupBy(x => new { x.StudentId, x.StudentName })
                .ToList();

            var tdata = new List<List<string>>();

            foreach (var student in studentGroups)
            {
                var row = new List<string> { student.Key.StudentName };
                foreach (var skill in skillCodes)
                {
                    var grade = student.FirstOrDefault(x => x.SkillCode == skill)?.Grade ?? "Not Graded";
                    row.Add(grade);
                }
                tdata.Add(row);
            }

            return new VStudentPerformanceChartVM
            {
                Headers = headers,
                TData = tdata
            };
        }



        public List<StudentCourseTenantVm> GetStudentDropDownOptions(int tenantId, int courseId, int branchId)
        {
            return _context.Students
                .AsNoTracking()
                .Include(s => s.Course)
                .Include(s => s.Tenant)
                .Where(s => !s.IsDeleted
                         && s.TenantId == tenantId
                         && s.CourseId == courseId
                         && s.BranchId == branchId)
                .OrderBy(s => s.Name)
                .Select(s => new StudentCourseTenantVm
                {
                    Id = s.Id,
                    Name = s.Name,
                    CourseId = s.CourseId,
                    CourseName = s.Course.Name,
                    TenantId = s.TenantId,
                    TenantName = s.Tenant.Name
                })
                .ToList();
        }

        public SRStudentRegistrationResponseVM Register(SRStudentRegistrationRequestVM request)
        {
            try
            {
                // ---------------------------
                // 1. Handle User in NeuroPiDb
                // ---------------------------
                using (var userTransaction = _userContext.Database.BeginTransaction())
                {
                    var user = _userContext.Users.FirstOrDefault(u =>
                        u.Username == request.User.Username ||
                        u.Email == request.User.Email ||
                        u.MobileNumber == request.User.MobileNumber);

                    if (user == null)
                    {
                        user = new MUser
                        {
                            Username = request.User.Username,
                            FirstName = request.User.FirstName,
                            LastName = request.User.LastName,
                            Email = request.User.Email,
                            Password = request.User.Password, // ⚠️ plain-text
                            MobileNumber = request.User.MobileNumber,
                            RoleTypeId = request.User.RoleTypeId,
                            TenantId = request.TenantId,
                            UserImageUrl = request.User.UserImageUrl,
                            DateOfBirth = DateOnly.FromDateTime(request.User.Dob),
                            CreatedOn = DateTime.UtcNow,
                            CreatedBy = 1
                        };

                        _userContext.Users.Add(user);
                        _userContext.SaveChanges();

                        // assign parent role
                        var parentRole = _userContext.Roles.First(r => r.Name == "PARENT");
                        _userContext.UserRoles.Add(new MUserRole
                        {
                            UserId = user.UserId,
                            RoleId = parentRole.RoleId,
                            TenantId = request.TenantId,
                            CreatedBy = user.UserId,
                            CreatedOn = DateTime.UtcNow
                        });
                        _userContext.SaveChanges();
                    }
                    else
                    {
                        if (!_userContext.UserRoles.Any(ur => ur.UserId == user.UserId))
                        {
                            var parentRole = _userContext.Roles.First(r => r.Name == "PARENT");
                            _userContext.UserRoles.Add(new MUserRole
                            {
                                UserId = user.UserId,
                                RoleId = parentRole.RoleId,
                                TenantId = request.TenantId,
                                CreatedBy = user.UserId,
                                CreatedOn = DateTime.UtcNow
                            });
                            _userContext.SaveChanges();
                        }
                    }

                    userTransaction.Commit();
                }

                // ---------------------------
                // 2. Handle Parent/Student in SchoolManagementDb
                // ---------------------------
                using (var schoolTransaction = _context.Database.BeginTransaction())
                {
                    var user = _userContext.Users.First(u => u.Username == request.User.Username);

                    // Parent
                    var parent = _context.Parents
                        .FirstOrDefault(p => p.UserId == user.UserId && p.TenantId == request.TenantId);

                    if (parent == null)
                    {
                        parent = new MParent
                        {
                            UserId = user.UserId,
                            TenantId = request.TenantId,
                            CreatedBy = user.UserId,
                            CreatedOn = DateTime.UtcNow
                        };
                        _context.Parents.Add(parent);
                        _context.SaveChanges();
                    }

                    // Student
                    var duplicateStudent = _context.Students.FirstOrDefault(s =>
                        s.Name == request.Student.FirstName &&
                        s.LastName == request.Student.LastName &&
                        s.DateOfBirth == DateOnly.FromDateTime(request.Student.Dob) &&
                        s.TenantId == request.TenantId &&
                        !s.IsDeleted);

                    if (duplicateStudent != null)
                        throw new InvalidOperationException("Student already exists with same name and DOB");

                    var student = new MStudent
                    {
                        Name = request.Student.FirstName,
                        LastName = request.Student.LastName,
                        MiddleName = request.Student.MiddleName,
                        DateOfBirth = DateOnly.FromDateTime(request.Student.Dob),
                        Gender = request.Student.Gender,
                        BloodGroup = request.Student.BloodGroup,
                        AdmissionGrade = request.Student.AdmissionGrade,
                        DateOfJoining = DateOnly.FromDateTime(request.Student.DateOfJoining),
                        CourseId = request.Student.CourseId,
                        BranchId = request.Student.BranchId,
                        TenantId = request.TenantId,

                        // ✅ fix string → byte[]
                        StudentImageUrl = string.IsNullOrEmpty(request.Student.StudentImageUrl)
           ? null
           : Convert.FromBase64String(request.Student.StudentImageUrl),

                        FatherPhoto = request.Student.FatherPhoto,
                        MotherPhoto = request.Student.MotherPhoto,
                        JointPhoto = request.Student.JointPhoto,
                        RegistrationChannel = request.Student.RegistrationChannel,
                        CreatedBy = user.UserId,
                        CreatedOn = DateTime.UtcNow
                    };


                    // RegNumber
                    // ✅ Safely handle nullable DateOfJoining
                    var year = student.DateOfJoining.HasValue
                        ? student.DateOfJoining.Value.Year
                        : DateTime.UtcNow.Year;

                    var lastReg = _context.Students
                        .Where(s => s.TenantId == request.TenantId &&
                                    s.BranchId == student.BranchId &&
                                    s.CourseId == student.CourseId &&
                                    s.DateOfJoining.HasValue &&           // ✅ check before using Value
                                    s.DateOfJoining.Value.Year == year)
                        .OrderByDescending(s => s.RegNumber)
                        .Select(s => s.RegNumber)
                        .FirstOrDefault();

                    var seq = 1;
                    if (!string.IsNullOrEmpty(lastReg) && int.TryParse(lastReg[^3..], out var lastSeq))
                        seq = lastSeq + 1;

                    // ✅ Build RegNumber (YY + BranchId + CourseId + Seq)
                    student.RegNumber = $"{year % 100:D2}{student.BranchId:D2}{student.CourseId:D2}{seq:D3}";

                    // Update student record
                    _context.Students.Update(student);


                    // StudentCourse
                    _context.StudentCourses.Add(new MStudentCourse
                    {
                        StudentId = student.Id,
                        CourseId = student.CourseId,
                        BranchId = student.BranchId,
                        TenantId = request.TenantId,
                        IsCurrentYear = true,
                        CreatedBy = user.UserId,
                        CreatedOn = DateTime.UtcNow
                    });

                    // Contacts (same as your existing contact handling)
                    // ...

                    // ParentStudent link
                    if (!_context.ParentStudents.Any(ps => ps.ParentId == parent.Id && ps.StudentId == student.Id))
                    {
                        _context.ParentStudents.Add(new MParentStudent
                        {
                            ParentId = parent.Id,
                            StudentId = student.Id,
                            TenantId = request.TenantId,
                            CreatedBy = user.UserId,
                            CreatedOn = DateTime.UtcNow
                        });
                    }

                    _context.SaveChanges();
                    schoolTransaction.Commit();

                    return new SRStudentRegistrationResponseVM
                    {
                        StudentId = student.Id,
                        UserId = user.UserId,
                        ParentId = parent.Id,
                        RegNumber = student.RegNumber,
                        Username = user.Username,
                        Password = user.Password
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }


    }
}
