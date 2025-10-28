using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Model;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.CourseTeacher;
using SchoolManagement.ViewModel.ParentStudents;
using SchoolManagement.ViewModel.Student;
using StudentVM = SchoolManagement.ViewModel.ParentStudents.StudentVM;

namespace SchoolManagement.Services.Implementation
{
    public class ParentStudentsServiceImpl : IParentStudentsService
    {
        private readonly SchoolManagement.Data.SchoolManagementDb _db;

        public ParentStudentsServiceImpl(SchoolManagement.Data.SchoolManagementDb db)
        {
            _db = db;
        }

        public ParentStudentResponseVM Create(ParentStudentRequestVM request)
        {
            var entity = new SchoolManagement.Model.MParentStudent
            {
                ParentId = request.ParentId,
                StudentId = request.StudentId,
                TenantId = request.TenantId,
                CreatedBy = request.CreatedBy,
                CreatedOn = System.DateTime.UtcNow
            };

            _db.ParentStudents.Add(entity);
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        public List<ParentStudentResponseVM> GetAll()
        {
            return _db.ParentStudents
                .Where(x => !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public List<ParentStudentResponseVM> GetAllByTenantId(int tenantId)
        {
            return _db.ParentStudents
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public ParentStudentResponseVM GetById(int id)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public ParentStudentResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public ParentStudentResponseVM UpdateByIdAndTenantId(int id, int tenantId, SchoolManagement.ViewModel.ParentStudents.ParentStudentUpdateVM request)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.ParentId = request.ParentId;
            entity.StudentId = request.StudentId;
            entity.UpdatedBy = request.UpdatedBy;
            entity.UpdatedOn = System.DateTime.UtcNow;

            _db.SaveChanges();
            return MapToResponse(entity);
        }

        public ParentStudentResponseVM DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.IsDeleted = true;
            entity.UpdatedOn = System.DateTime.UtcNow;
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        private ParentStudentResponseVM MapToResponse(SchoolManagement.Model.MParentStudent model)
        {
            return new ParentStudentResponseVM
            {
                Id = model.Id,
                ParentId = model.ParentId,
                StudentId = model.StudentId,
                TenantId = model.TenantId
            };
        }
        public CourseTeacherVM GetParentDetails(int userId, int tenantId)
        {
            CourseTeacherVM courseTeacherVM = new CourseTeacherVM();
            var parentId = _db.Parents.Where(p => p.UserId == userId && p.TenantId == tenantId).FirstOrDefault();
            var result = _db.ParentStudents.Where(t => t.TenantId == tenantId && t.ParentId == parentId.Id).Include(s => s.Student).Include(c => c.Student.Course).FirstOrDefault();
            List<Course> courses = new List<Course>();
            if (result != null)
            {
                courseTeacherVM.branchId = result.Student.BranchId;

                var courseObj = new Course()
                {
                    id = result.Student.Course.Id,
                    name = result.Student.Course.Name,
                };
                courses.Add(courseObj);

            }
            courseTeacherVM.courses = courses;
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            var week = _db.Weeks.Where(w => w.StartDate <= today && w.EndDate >= today && !w.IsDeleted).FirstOrDefault();
            courseTeacherVM.weekId = week != null ? week.Id : 0;
            courseTeacherVM.termId = week != null ? week.TermId : 0;
            return courseTeacherVM;
        }


        public ParentWithStudentsResponseVM GetFullParentDetailsByUserId(int userId, int tenantId)
        {
            // Step 1: Get the parent with linked User
            var parent = _db.Parents
                .Include(p => p.User)
                .FirstOrDefault(p => p.UserId == userId && p.TenantId == tenantId && !p.IsDeleted);

            if (parent == null)
                return null;

            // Step 2: Resolve RoleType name (from masters table in SchoolManagementDb)
            string? roleTypeName = null;
            if (parent.User?.RoleTypeId != null)
            {
                roleTypeName = _db.Masters
                    .Where(m => m.Id == parent.User.RoleTypeId && m.TenantId == tenantId && !m.IsDeleted)
                    .Select(m => m.Name)
                    .FirstOrDefault();
            }

            // Step 3: Fetch all student links with navigation props
            var studentLinks = _db.ParentStudents
                .Where(ps => ps.ParentId == parent.Id && ps.TenantId == tenantId && !ps.IsDeleted)
                .Include(ps => ps.Student)
                    .ThenInclude(s => s.Course)
                .Include(ps => ps.Student)
                    .ThenInclude(s => s.Branch)
                .ToList();

            // Step 4: Map to ViewModel
            var response = new ParentWithStudentsResponseVM
            {
                Parent = new ParentVM
                {
                    ParentId = parent.Id,
                    UserId = parent.UserId,
                    ParentName = parent.User?.Username,
                    Email = parent.User?.Email,
                    MobileNumber = parent.User?.MobileNumber,
                    TenantId = parent.TenantId,
                    RoleTypeId = parent.User?.RoleTypeId,
                    RoleTypeName = roleTypeName,

                    // 🔹 New fields from MUser
                    FirstName = parent.User?.FirstName,
                    LastName = parent.User?.LastName,
                    MiddleName = parent.User?.MiddleName,
                    Gender = parent.User?.Gender,
                    UserImageUrl = parent.User?.UserImageUrl,
                    AlternateNumber = parent.User?.AlternateNumber,
                    Address = parent.User?.Address,
                    DateOfBirth = parent.User?.DateOfBirth,
                    FatherName = parent.User?.FatherName,
                    MotherName = parent.User?.MotherName,
                    SpouseName = parent.User?.SpouseName,
                    MaritalStatus = parent.User?.MaritalStatus,
                    WeddingAnniversaryDate = parent.User?.WeddingAnniversaryDate,
                    JoiningDate = parent.User?.JoiningDate,
                    WorkingStartTime = parent.User?.WorkingStartTime,
                    WorkingEndTime = parent.User?.WorkingEndTime
                },
                Students = studentLinks
                    .Where(x => x.Student != null)
                    .Select(x => new StudentVM
                    {
                        StudentId = x.Student.Id,
                        Name = x.Student.Name, // maps to [first_name] column
                        MiddleName = x.Student.MiddleName,
                        LastName = x.Student.LastName,
                        CourseName = x.Student.Course?.Name,
                        CourseId=x.Student.Course?.Id,
                        BranchName = x.Student.Branch?.Name,
                        StudentImageUrl = x.Student.StudentImageUrl,

                        // 🔹 Fixed fields
                        Dob = x.Student.DateOfBirth,
                        Age = x.Student.DateOfBirth.HasValue
                            ? (int?)((DateTime.Today - x.Student.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue)).Days / 365)
                            : null,
                        Gender = x.Student.Gender,
                        BloodGroup = x.Student.BloodGroup,
                        AdmissionNumber = x.Student.RegNumber,   // maps correctly
                        AdmissionGrade = x.Student.AdmissionGrade,
                        DateOfJoining = x.Student.DateOfJoining
                    })
                    .ToList()
            };

            return response;
        }

        public List<ParentWithStudentsResponseVM> GetAllParentsWithStudentsByTenantIdAndBranchIdAndCourseId(int tenantId, int courseId, int branchId)
        {
            var response = _db.ParentStudents
                .Include(s => s.Student)
                .Where(ps => ps.TenantId == tenantId && !ps.IsDeleted &&
                             (ps.Student.CourseId == courseId) &&
                             (ps.Student.BranchId == branchId))
                .Select(ps => new ParentWithStudentsResponseVM
                {
                    Parent = new ParentVM
                    {
                        ParentId = ps.Parent.Id,
                        UserId = ps.Parent.UserId,
                        ParentName = ps.Parent.User.Username,
                        Email = ps.Parent.User.Email,
                        MobileNumber = ps.Parent.User.MobileNumber,
                        TenantId = ps.Parent.TenantId
                    },
                    Students = new List<StudentVM>
                    {
                        new StudentVM
                        {
                            StudentId = ps.Student.Id,
                            Name = ps.Student.Name,
                            MiddleName = ps.Student.MiddleName,
                            LastName = ps.Student.LastName,
                            CourseName = ps.Student.Course.Name,
                            BranchName = ps.Student.Branch.Name,
                            

                        }
                    }
                });
            return response.ToList();
        }

    }
}          