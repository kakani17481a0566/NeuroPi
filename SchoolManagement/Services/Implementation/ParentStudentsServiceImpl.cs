using Microsoft.EntityFrameworkCore;
using SchoolManagement.Data;
using SchoolManagement.Services.Interface;
using SchoolManagement.ViewModel.ParentStudents;

namespace SchoolManagement.Services.Implementation
{
    public class ParentStudentsServiceImpl : SchoolManagement.Services.Interface.IParentStudentsService
    {
        private readonly SchoolManagement.Data.SchoolManagementDb _db;

        public ParentStudentsServiceImpl(SchoolManagement.Data.SchoolManagementDb db)
        {
            _db = db;
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM Create(SchoolManagement.ViewModel.ParentStudents.ParentStudentRequestVM request)
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

        public System.Collections.Generic.List<SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM> GetAll()
        {
            return _db.ParentStudents
                .Where(x => !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public System.Collections.Generic.List<SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM> GetAllByTenantId(int tenantId)
        {
            return _db.ParentStudents
                .Where(x => x.TenantId == tenantId && !x.IsDeleted)
                .Select(MapToResponse)
                .ToList();
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM GetById(int id)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM GetByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            return entity == null ? null : MapToResponse(entity);
        }

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM UpdateByIdAndTenantId(int id, int tenantId, SchoolManagement.ViewModel.ParentStudents.ParentStudentUpdateVM request)
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

        public SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM DeleteByIdAndTenantId(int id, int tenantId)
        {
            var entity = _db.ParentStudents.FirstOrDefault(x => x.Id == id && x.TenantId == tenantId && !x.IsDeleted);
            if (entity == null) return null;

            entity.IsDeleted = true;
            entity.UpdatedOn = System.DateTime.UtcNow;
            _db.SaveChanges();

            return MapToResponse(entity);
        }

        private SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM MapToResponse(SchoolManagement.Model.MParentStudent model)
        {
            return new SchoolManagement.ViewModel.ParentStudents.ParentStudentResponseVM
            {
                Id = model.Id,
                ParentId = model.ParentId,
                StudentId = model.StudentId,
                TenantId = model.TenantId
            };
        }


        public ParentWithStudentsResponseVM GetFullParentDetailsByUserId(int userId, int tenantId)
        {
            // Step 1: Get the parent using UserId + TenantId
            var parent = _db.Parents
                .Include(p => p.User)
                .FirstOrDefault(p => p.UserId == userId && p.TenantId == tenantId && !p.IsDeleted);

            if (parent == null)
                return null;

            // Step 2: Fetch all student links for this parent
            var studentLinks = _db.ParentStudents
                .Where(ps => ps.ParentId == parent.Id && ps.TenantId == tenantId && !ps.IsDeleted)
                .Include(ps => ps.Student)
                    .ThenInclude(s => s.Course)
                .Include(ps => ps.Student)
                    .ThenInclude(s => s.Branch)
                .ToList();

            // Step 3: Map to ViewModel
            var response = new ParentWithStudentsResponseVM
            {
                Parent = new ParentVM
                {
                    ParentId = parent.Id,
                    UserId = parent.UserId,
                    ParentName = parent.User?.Username,
                    Email = parent.User?.Email,
                    MobileNumber = parent.User?.MobileNumber,
                    TenantId = parent.TenantId
                },
                Students = studentLinks
                    .Where(x => x.Student != null)
                    .Select(x => new StudentVM
                    {
                        StudentId = x.Student.Id,
                        Name = x.Student.Name,
                        CourseName = x.Student.Course?.Name,
                        BranchName = x.Student.Branch?.Name,
                        StudentImageUrl = x.Student.StudentImageUrl
                    })
                    .ToList()
            };

            return response;
        }



    }
}
