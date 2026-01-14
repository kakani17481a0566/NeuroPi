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
            student.UpdatedOn = DateTime.UtcNow;
            student.UpdatedBy = request.UpdatedBy;

            // Update extra profile fields
            if (request.DateOfBirth.HasValue)
                student.DateOfBirth = DateOnly.FromDateTime(request.DateOfBirth.Value);
            
            student.Gender = request.Gender;
            student.BloodGroup = request.BloodGroup;
            student.RegNumber = request.AdmissionNumber; // Map AdmissionNumber to RegNumber
            student.AdmissionGrade = request.AdmissionGrade;
            
            if (request.DateOfJoining.HasValue)
                student.DateOfJoining = DateOnly.FromDateTime(request.DateOfJoining.Value);

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
                MUser createdUser;

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
                            Password = request.User.Password, // ⚠️ hash later
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

                        var parentRole = _userContext.Roles.FirstOrDefault(r => r.Name == "PARENT");
                        if (parentRole == null)
                            throw new Exception("Role 'PARENT' not found in database. Please seed roles.");

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
                            var parentRole = _userContext.Roles.FirstOrDefault(r => r.Name == "PARENT");
                            if (parentRole == null)
                                throw new Exception("Role 'PARENT' not found in database. Please seed roles.");

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

                    createdUser = user;
                    userTransaction.Commit();
                }

                // ---------------------------
                // 2. Handle Parent/Student + Contacts in SchoolManagementDb
                // ---------------------------
                using (var schoolTransaction = _context.Database.BeginTransaction())
                {
                    var user = createdUser;

                    if (user == null)
                        throw new Exception($"User '{request.User.Username}' not found after creation.");

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

                    // Student duplicate check
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

                        StudentImageUrl = request.Student.StudentImageUrl,
                        FatherPhoto = request.Student.FatherPhoto,
                        MotherPhoto = request.Student.MotherPhoto,
                        JointPhoto = request.Student.JointPhoto,

                        RegistrationChannel = request.Student.RegistrationChannel,
                        CreatedBy = user.UserId,
                        CreatedOn = DateTime.UtcNow,

                        Signature = request.Student.Documents.Signature,
                        BirthCertificate = request.Student.Documents.BirthCertificate,
                        KidPassport = request.Student.Documents.KidPassport,
                        Adhar = request.Student.Documents.Adhar,
                        ParentAdhar = request.Student.Documents.ParentAdhar,
                        MotherAdhar = request.Student.Documents.MotherAdhar,
                        HealthForm = request.Student.Documents.HealthForm,
                        PrivacyForm = request.Student.Documents.PrivacyForm,
                        LiabilityForm = request.Student.Documents.LiabilityForm
                    };

                    // ---------------------------
                    // 3. Map Extra Details (Transport, Custody, Medical, Languages, English Skills)
                    // ---------------------------
                    if (request.Student.Transport != null)
                    {
                        student.HasRegularTransport = request.Student.Transport.Regular.IsEnabled;
                        student.RegularTransportId = request.Student.Transport.Regular.TransportId;
                        student.RegularTransportText = request.Student.Transport.Regular.FreeText;
                        student.HasAlternateTransport = request.Student.Transport.Alternate.IsEnabled;
                        student.AlternateTransportId = request.Student.Transport.Alternate.TransportId;
                        student.AlternateTransportText = request.Student.Transport.Alternate.FreeText;
                        student.OtherTransportText = request.Student.Transport.OtherTransportText;
                    }

                    if (request.Student.CustodyFamily != null)
                    {
                        student.SpeechTherapy = request.Student.CustodyFamily.SpeechTherapy;
                        student.Custody = request.Student.CustodyFamily.Custody;
                        student.CustodyOfId = request.Student.CustodyFamily.CustodyOfId;
                        student.LivesWithId = request.Student.CustodyFamily.LivesWithId;
                        student.SiblingsInThisSchool = request.Student.CustodyFamily.SiblingsInThisSchool;
                        student.SiblingsThisNames = request.Student.CustodyFamily.SiblingsThisNames;
                        student.SiblingsInOtherSchool = request.Student.CustodyFamily.SiblingsInOtherSchool;
                        student.SiblingsOtherNames = request.Student.CustodyFamily.SiblingsOtherNames;
                    }

                    if (request.Student.MedicalInfo != null)
                    {
                        student.AnyAllergy = request.Student.MedicalInfo.AnyAllergy;
                        student.WhatAllergyId = request.Student.MedicalInfo.WhatAllergyId;
                        student.OtherAllergyText = request.Student.MedicalInfo.OtherAllergyText;
                        student.MedicalKit = request.Student.MedicalInfo.MedicalKit;
                        student.SeriousMedicalConditions = request.Student.MedicalInfo.SeriousMedicalConditions;
                        student.SeriousConditionsInfo = request.Student.MedicalInfo.SeriousConditionsInfo;
                        student.OtherMedicalInfo = request.Student.MedicalInfo.OtherMedicalInfo;
                    }

                    if (request.Student.Languages != null)
                    {
                        student.LanguageAdultsHome = request.Student.Languages.LanguageAdultsHome;
                        student.LanguageMostUsedWithChild = request.Student.Languages.LanguageMostUsedWithChild;
                        student.LanguageFirstLearned = request.Student.Languages.LanguageFirstLearned;
                    }

                    if (request.Student.EnglishSkills != null)
                    {
                        student.CanReadEnglish = request.Student.EnglishSkills.CanReadEnglish;
                        student.ReadSkillId = request.Student.EnglishSkills.ReadSkillId;
                        student.CanWriteEnglish = request.Student.EnglishSkills.CanWriteEnglish;
                        student.WriteSkillId = request.Student.EnglishSkills.WriteSkillId;
                    }

                    _context.Students.Add(student);
                    _context.SaveChanges();

                    // Generate RegNumber
                    var year = student.DateOfJoining.HasValue
                        ? student.DateOfJoining.Value.Year
                        : DateTime.UtcNow.Year;

                    var lastReg = _context.Students
                        .Where(s => s.TenantId == request.TenantId &&
                                    s.BranchId == student.BranchId &&
                                    s.CourseId == student.CourseId &&
                                    s.DateOfJoining.HasValue &&
                                    s.DateOfJoining.Value.Year == year)
                        .OrderByDescending(s => s.RegNumber)
                        .Select(s => s.RegNumber)
                        .FirstOrDefault();

                    var seq = 1;
                    if (!string.IsNullOrEmpty(lastReg) && int.TryParse(lastReg[^3..], out var lastSeq))
                        seq = lastSeq + 1;

                    student.RegNumber = $"{year % 100:D2}{student.BranchId:D2}{student.CourseId:D2}{seq:D3}";
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

                    // ---------------------------
                    // 4. Handle Contacts (avoid duplicates)
                    // ---------------------------
                    if (request.Contacts != null && request.Contacts.Any())
                    {
                        var relationships = _context.Masters
                            .Where(m => m.MasterTypeId == 43 && !m.IsDeleted)
                            .ToDictionary(m => m.Id, m => m.Code!.ToUpper());

                        foreach (var c in request.Contacts)
                        {
                            var existingContact = _context.Contacts.FirstOrDefault(x =>
                                x.TenantId == request.TenantId &&
                                x.RelationshipId == c.RelationshipId &&
                                (
                                    (!string.IsNullOrEmpty(c.Email) && x.Email == c.Email) ||
                                    (!string.IsNullOrEmpty(c.PriNumber) && x.PriNumber == c.PriNumber)
                                )
                            );

                            MContact contact;
                            if (existingContact != null)
                            {
                                contact = existingContact;
                            }
                            else
                            {
                                contact = new MContact
                                {
                                    Name = c.Name,
                                    PriNumber = c.PriNumber,
                                    SecNumber = c.SecNumber,
                                    Email = c.Email,
                                    Address1 = c.Address1,
                                    Address2 = c.Address2,
                                    City = c.City,
                                    State = c.State,
                                    Pincode = c.Pincode,
                                    Qualification = c.Qualification,
                                    Profession = c.Profession,
                                    TenantId = request.TenantId,
                                    RelationshipId = c.RelationshipId,
                                    CreatedBy = user.UserId,
                                    CreatedOn = DateTime.UtcNow
                                };
                                _context.Contacts.Add(contact);
                                _context.SaveChanges();
                            }

                            if (relationships.TryGetValue(c.RelationshipId, out var code))
                            {
                                switch (code)
                                {
                                    case "FATHER":
                                        student.FatherContactId = contact.Id;
                                        break;
                                    case "MOTHER":
                                        student.MotherContactId = contact.Id;
                                        break;
                                    case "GUARDIAN":
                                        student.GuardianContactId = contact.Id;
                                        break;
                                    case "AFTER_SCHOOL":
                                        student.AdditionalSupportContactId = contact.Id;
                                        break;
                                    case "EMERGENCY":
                                        student.EmergencyContactId = contact.Id;
                                        break;
                                }
                            }
                        }

                        _context.Students.Update(student);
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
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Registration failed: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }


        public List<StudentListVM> GetStudentsByTenantCourseBranch(int tenantId, int courseId, int branchId)
        {
            var students = _context.Students
                .Where(s => !s.IsDeleted
                            && s.TenantId == tenantId
                            && !s.Course.IsDeleted
                            && s.Course.TenantId == tenantId
                            && (courseId == -1 || s.Course.Id == courseId)
                            && !s.Branch.IsDeleted
                            && s.Branch.TenantId == tenantId
                            && (branchId == -1 || s.Branch.Id == branchId))
                .Select(s => new StudentListVM
                {
                    Id = s.Id,
                    FirstName = s.Name,
                    LastName = s.LastName,
                    CourseName = s.Course.Name,
                    BranchName = s.Branch.Name
                })
                .OrderBy(s => s.BranchName)   // ✅ sorted in DB, not in memory
                .ThenBy(s => s.CourseName)
                .ThenBy(s => s.FirstName)
                .ToList();

            return students;
        }


        private byte[]? SafeBase64Decode(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;

            // Handle data URI like "data:image/png;base64,xxxx"
            var base64 = input.Contains(",") ? input.Split(',')[1] : input;

            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                // log the issue here if you want
                return null; // gracefully ignore bad data
            }
        }

        public List<StudentFilterResponseVM> GetAllStudentsByName(string name, DateOnly? dob = null)
        {
            List<StudentFilterResponseVM> result = new List<StudentFilterResponseVM>();
            var query = _context.Students
                .Include(e => e.Course)
                .Include(b => b.Branch)
                .Where(s => EF.Functions.Like(s.Name.ToLower(), $"%{name.ToLower()}%") || EF.Functions.Like(s.LastName.ToLower(), $"%{name.ToLower()}%"));

            if (dob.HasValue)
            {
                query = query.Where(s => s.DateOfBirth == dob.Value);
            }

            var StudentsList = query.ToList();

            if (StudentsList != null)
            {
                foreach (var student in StudentsList)
                {
                    StudentFilterResponseVM responseVM = new StudentFilterResponseVM();
                    responseVM.StudentId = student.Id;
                    responseVM.StudentName = student.FullName;
                    responseVM.courseId = student.CourseId;
                    responseVM.CourseName = student.Course.Name;
                    responseVM.BranchId = student.BranchId;
                    responseVM.BranchName = student.Branch.Name;
                    result.Add(responseVM);
                }
                return result;

            }
            return result;
        }

        public List<StudentFilterResponseVM> GetStudentsByParentUserId(int userId)
        {
            var result = new List<StudentFilterResponseVM>();
            var parent = _context.Parents.FirstOrDefault(p => p.UserId == userId && !p.IsDeleted);
            if (parent != null)
            {
                var linkedStudents = _context.ParentStudents
                                             .Where(ps => ps.ParentId == parent.Id && !ps.IsDeleted)
                                             .Include(ps => ps.Student).ThenInclude(s => s.Course)
                                             .Include(ps => ps.Student).ThenInclude(s => s.Branch)
                                             .Select(ps => ps.Student)
                                             .Where(s => !s.IsDeleted)
                                             .ToList();

                foreach (var student in linkedStudents)
                {
                    StudentFilterResponseVM responseVM = new StudentFilterResponseVM();
                    responseVM.StudentId = student.Id;
                    responseVM.StudentName = student.FullName;
                    responseVM.courseId = student.CourseId;
                    responseVM.CourseName = student.Course?.Name;
                    responseVM.BranchId = student.BranchId;
                    responseVM.BranchName = student.Branch?.Name;
                    result.Add(responseVM);
                }
            }
            return result;
        }
        public bool UpdateLinkedStudents(int userId, List<int> studentIds)
        {
            try
            {
                // 1. Get User to find TenantId
                var user = _userContext.Users.FirstOrDefault(u => u.UserId == userId && !u.IsDeleted);
                if (user == null) return false;

                // 2. Find or Create Parent
                Console.WriteLine($"[DEBUG] Searching for parent with UserId: {userId}");
                var parent = _context.Parents.FirstOrDefault(p => p.UserId == userId && p.TenantId == user.TenantId && !p.IsDeleted);
                
                if (parent == null)
                {
                    Console.WriteLine($"[DEBUG] Parent not found. Creating new parent for UserId: {userId}");
                    parent = new MParent
                    {
                        UserId = userId,
                        TenantId = user.TenantId,
                        CreatedBy = userId,
                        CreatedOn = DateTime.UtcNow
                    };
                    _context.Parents.Add(parent);
                    _context.SaveChanges();
                    Console.WriteLine($"[DEBUG] Created new Parent. Id: {parent.Id}, UserId: {parent.UserId}");
                }
                else
                {
                    Console.WriteLine($"[DEBUG] Found existing Parent. Id: {parent.Id}, UserId: {parent.UserId}");
                }

                // 3. Get existing links (including deleted ones to restore if needed)
                var existingLinks = _context.ParentStudents
                    .Where(ps => ps.ParentId == parent.Id)
                    .ToList();
                
                Console.WriteLine($"[DEBUG] Found {existingLinks.Count} existing links for ParentId: {parent.Id}");

                var requestedStudentIds = studentIds.Distinct().ToList();

                // 4. Process links
                foreach (var studentId in requestedStudentIds)
                {
                    var existingLink = existingLinks.FirstOrDefault(ps => ps.StudentId == studentId);
                    if (existingLink != null)
                    {
                        // Enable if deleted
                        if (existingLink.IsDeleted)
                        {
                            Console.WriteLine($"[DEBUG] Restoring link for StudentId: {studentId}");
                            existingLink.IsDeleted = false;
                            existingLink.UpdatedOn = DateTime.UtcNow;
                            existingLink.UpdatedBy = userId;
                            _context.ParentStudents.Update(existingLink);
                        }
                    }
                    else
                    {
                        // Create new link
                        Console.WriteLine($"[DEBUG] Creating new link: ParentId={parent.Id}, StudentId={studentId}");
                        _context.ParentStudents.Add(new MParentStudent
                        {
                            ParentId = parent.Id,
                            StudentId = studentId,
                            TenantId = user.TenantId,
                            CreatedBy = userId,
                            CreatedOn = DateTime.UtcNow
                        });
                    }
                }

                // 5. Remove links not in request
                foreach (var link in existingLinks.Where(ps => !requestedStudentIds.Contains(ps.StudentId) && !ps.IsDeleted))
                {
                    link.IsDeleted = true;
                    link.UpdatedOn = DateTime.UtcNow;
                    link.UpdatedBy = userId;
                    _context.ParentStudents.Update(link);
                }

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating linked students: {ex.Message}");
                return false;
            }
        }
    }
}
